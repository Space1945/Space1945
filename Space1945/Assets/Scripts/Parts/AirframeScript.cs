using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirframeScript : MonoBehaviour
{
    public Transform[] butts;
    public float max_hp;
    public float basic_def;
    public float crash_damage;
    public int gold;
    public float cur_hp { get; set; }

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

    Dictionary<string, float> reinforce = new Dictionary<string, float>
    {
        { "def", 1f },
        { "crashd", 1f }
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

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();

        //par_die = Resources.Load<ParticleSystem>("Particle/Enemy_die_particle");

        // ���� Ȱ��ȭ
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
    }
    void Start() // ��ȭ
    {
        max_hp *= Camera.main.GetComponent<Ingame_manager>().ex_total.ex_hp;
        basic_def *= Camera.main.GetComponent<Ingame_manager>().ex_total.ex_def;
        crash_damage *= Camera.main.GetComponent<Ingame_manager>().ex_total.ex_crash_dmg;
        
        cur_hp = max_hp;

        touch_began = new Vector2(0, 0);

        bd = basic_def;
        cd = crash_damage;
    }

    void FixedUpdate()
    {
        basic_def = bd * reinforce["def"];
        crash_damage = cd * reinforce["crashd"];

        if (cur_hp <= 0)
        {
            // ���� ���
            //ParticleSystem par = Instantiate(par_die);
            //par.transform.position = transform.position; // ��� ȿ��
            StopAllCoroutines();
            Destroy(gameObject);
        }

        Move();
    }

    IEnumerator TR(float duration, string name, float percentage)
    {
        if (reinforce.ContainsKey(name))
        {
            reinforce[name] *= percentage;

            yield return new WaitForSeconds(duration);

            reinforce[name] /= percentage;
        }
    }
    public void TemporaryReinforce(float duration, string name, float percentage)
    {
        StartCoroutine(TR(duration, name, percentage));
        atk.GetComponent<AtkInterface>().TemporaryReinforce(duration, name, percentage);
    }
    public void Attacked(float crash_damage)
    {
        cur_hp -= crash_damage;
        Camera.main.GetComponent<Ingame_manager>().UpdatePlayersHP();
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
