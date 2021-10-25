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

    Rigidbody2D rigid;
    PolygonCollider2D col;

    Vector2 touch_began;
    Touch touch;

    ParticleSystem par_die;

    GameObject atk_;
    GameObject def_;
    GameObject sl_;
    GameObject sr_;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();

        par_die = Resources.Load<ParticleSystem>("Particle/Enemy_die_particle");

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
    }
    void Start() // 강화
    {
        max_hp *= 1 + Camera.main.GetComponent<Ingame_manager>().ex_total.ex_hp / 100f;
        basic_def *= 1 + Camera.main.GetComponent<Ingame_manager>().ex_total.ex_def / 100f;
        crash_damage *= 1 + Camera.main.GetComponent<Ingame_manager>().ex_total.ex_crash_dmg / 100f;
        
        cur_hp = max_hp;

        touch_began = new Vector2(0, 0);
    }

    void FixedUpdate()
    {
        if (cur_hp <= 0)
        {
            // 사운드 출력
            ParticleSystem par = Instantiate(par_die);
            par.transform.position = transform.position; // 사망 효과
            StopAllCoroutines();
            Destroy(gameObject);
        }

        Move();
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
