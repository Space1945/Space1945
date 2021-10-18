using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float cur_hp { get; set; }
    public float invincible_time; // 해당 시간동안 무적
    
    Rigidbody2D rigid;
    PolygonCollider2D col;
    GameObject airframe;
    float airframe_crash_damage;
    bool invincible;

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

        invincible = false;
    }

    IEnumerator InvincibleTime()
    {
        yield return new WaitForSeconds(invincible_time);

        invincible = false;
    }
    
    public void BodyAttacked(float crash_damage)
    {
        if (!invincible)
        {
            invincible = true;
            StartCoroutine(InvincibleTime());
            Attacked(crash_damage);
        }
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
            Destroy(gameObject);
        }

        Move();
    }

    void OnTriggerEnter2D(Collider2D col) // 적 개체와의 충돌만 담당
    {
        switch (col.gameObject.tag)
        {
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().BodyAttacked(airframe_crash_damage);
                break;
        }
    }
    void OnTriggerStay2D(Collider2D col) // 적 개체와의 충돌만 담당
    {
        switch (col.gameObject.tag)
        {
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().BodyAttacked(airframe_crash_damage);
                break;
        }
    }
}