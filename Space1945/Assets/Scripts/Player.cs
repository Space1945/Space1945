using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float curHp { get; set; }
    
    Rigidbody2D rigid;
    PolygonCollider2D col;

    GameObject airframe;

    Vector2 touch_began;
    Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();

        airframe = DB_Manager.Instance.using_airframe;

        curHp = airframe.GetComponent<AirframeScript>().maxHp;
        touch_began = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (curHp <= 0)
        {
            // 사운드 출력
            Destroy(gameObject);
        }
        Move();
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "enemy_bullet")
        {
            curHp -= coll.GetComponent<BulletInfo>().crash_damage;
            Camera.main.GetComponent<Ingame_manager>().Player_attacked();
            Destroy(coll.gameObject);
        }
    }
}