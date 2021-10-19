using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float cur_hp { get; set; }
    
    Rigidbody2D rigid;
    PolygonCollider2D col;
    GameObject airframe;
    float airframe_crash_damage;

    Vector2 touch_began;
    Touch touch;

    ParticleSystem par_die;

    // Start is called before the first frame update
    void Start()
    {
        Initiate();
    }

    void Initiate()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();

        par_die = Resources.Load<ParticleSystem>("Particle/Enemy_die_particle");

        airframe = DB_Manager.Instance.using_airframe;
        airframe_crash_damage = airframe.GetComponent<AirframeScript>().crash_damage;

        cur_hp = airframe.GetComponent<AirframeScript>().max_hp;
        touch_began = new Vector2(0, 0);
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

    // Update is called once per frame
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
}