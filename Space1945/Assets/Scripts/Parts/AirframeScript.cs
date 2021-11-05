using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirframeScript : MonoBehaviour
{
    public Transform[] butts;
    public float max_hp;
    public float max_guage;
    public float basic_def;
    public float crash_damage;
    public int gold;

    public GameObject atk
    {
        get
        {
            return atk_;
        }
    }
    public GameObject def
    {
        get
        {
            return def_;
        }
    }
    public GameObject sub_left
    {
        get
        {
            return sl_;
        }
    }
    public GameObject sub_right
    {
        get
        {
            return sr_;
        }
    }

    public Dictionary<string, float> reinforce_mul = new Dictionary<string, float>
    {
        { "def", 1f },
        { "crashd", 1f },
    };
    public Dictionary<string, float> reinforce_add = new Dictionary<string, float>
    {
        { "hp", -1f },
        { "g", -1f }
    };

    Rigidbody2D rigid;
    PolygonCollider2D col;

    Vector2 touch_began;
    Touch touch;

    ParticleSystem par_die;

    GameObject atk_;
    GameObject def_;
    GameObject sl_;
    GameObject sr_;

    float bd;
    float cd;

    Ingame_manager im;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();

        //par_die = Resources.Load<ParticleSystem>("Particle/Enemy_die_particle");

        // 파츠 활성화
        atk_ = Instantiate(DB_Manager.Instance.using_atk, transform);
        atk_.GetComponent<SpriteRenderer>().sprite = null;
        if (DB_Manager.Instance.using_def != null)
        {
            def_ = Instantiate(DB_Manager.Instance.using_def, transform);
            def_.GetComponent<SpriteRenderer>().sprite = null;
        }
        if (DB_Manager.Instance.using_sub_left != null)
        {
            sl_ = Instantiate(DB_Manager.Instance.using_sub_left, transform);
            sl_.GetComponent<SpriteRenderer>().sprite = null;
        }
        if (DB_Manager.Instance.using_sub_right != null)
        {
            sr_ = Instantiate(DB_Manager.Instance.using_sub_right, transform);
            sr_.GetComponent<SpriteRenderer>().sprite = null;
        }

        im = Camera.main.GetComponent<Ingame_manager>();
    }
    void Start() // 강화
    {
        max_hp *= im.ex_total.ex_hp;
        basic_def *= im.ex_total.ex_def;
        crash_damage *= im.ex_total.ex_crash_dmg;
        
        reinforce_add.Add("mhp", max_hp);
        reinforce_add.Add("chp", max_hp);
        reinforce_add.Add("mg", max_guage);
        reinforce_add.Add("cg", 0f);

        touch_began = new Vector2(0, 0);

        bd = basic_def;
        cd = crash_damage;

        im.UpdatePlayersHPBar();
    }

    void FixedUpdate()
    {
        basic_def = bd * reinforce_mul["def"];
        crash_damage = cd * reinforce_mul["crashd"];

        if (reinforce_add["chp"] <= 0)
        {
            // 사운드 출력
            //ParticleSystem par = Instantiate(par_die);
            //par.transform.position = transform.position; // 사망 효과
            StopAllCoroutines();
            Destroy(gameObject);
        }

        Move();
    }

    IEnumerator TRMul(float duration, string name, float percentage)
    {
        if (reinforce_mul.ContainsKey(name))
        {
            reinforce_mul[name] *= percentage;

            yield return new WaitForSeconds(duration);

            reinforce_mul[name] /= percentage;
        }
    }
    void TRAdd(string name, float percentage)
    {
        if (reinforce_add.ContainsKey(name))
        {
            float adtl = reinforce_add["m" + name] * percentage;
            if (reinforce_add["c" + name] + adtl > reinforce_add["m" + name])
                reinforce_add["c" + name] = reinforce_add["m" + name];
            else
                reinforce_add["c" + name] += adtl;
        }
    }
    public void TemporaryReinforce(float duration, string name, float percentage)
    {
        StartCoroutine(TRMul(duration, name, percentage));
        TRAdd(name, percentage);
        atk.GetComponent<AtkInterface>().TemporaryReinforce(duration, name, percentage);
    }
    public void Attacked(float crash_damage)
    {
        reinforce_add["chp"] -= crash_damage;
    }
    void Move()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            Vector2 nor = (touch.position - touch_began).normalized;
            if (touch.phase == TouchPhase.Began)
                touch_began = touch.position;
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 v = nor * 500;
                v = 500 < Vector2.Distance(touch.position, touch_began) ? v : (touch.position - touch_began);
                rigid.velocity = v * Time.deltaTime * 15;
            }
            touch_began = touch.position;
        }
        else
            rigid.velocity = new Vector2(0, 0);
    }
}
