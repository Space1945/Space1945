using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_info : MonoBehaviour
{
    public float hp;
    public float crash_damage; // 충돌 데미지
    public int score;
    public int exp;
    public int gold;
    public float add_guage;
    public float invincible_time;
    public int instantiate_count; // 원본을 몇개까지 복제할건지
    public string kind; // 몹이 무슨 종류인지 >> normal, elite, boss로 구분 

    Rigidbody2D rigid;
    PolygonCollider2D col;
    ParticleSystem par_die;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        invincible_time = GV.INVINCIBLE_TIME;
        par_die = Resources.Load<ParticleSystem>("Particle/Enemy_die_particle");
    }

    void FixedUpdate()
    {
        invincible_time++;
        if (hp <= 0) // 사망
        {
            Camera.main.GetComponent<Ingame_manager>().enemy_cnt--;
            Debug.Log(Camera.main.GetComponent<Ingame_manager>().enemy_cnt);
            ParticleSystem par = Instantiate(par_die);
            par.transform.position = this.transform.position; // 적 사망 효과

            Camera.main.GetComponent<Ingame_manager>().Plus_ultimate_guage(add_guage);
            Camera.main.GetComponent<Ingame_manager>().Add_point(score, exp, gold);
            //사운드 출력
            //일정몹이 아이템 뿌리기
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player_bullet") // 총알이면
        {
            hp -= collision.gameObject.GetComponent<BulletInfo>().crash_damage;
            if (collision.gameObject.GetComponent<Penetrate>() == null) // 해당 탄환이 관통이 아니면
                Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "player" && invincible_time > GV.INVINCIBLE_TIME) // 1초
        {
            invincible_time = 0;
            hp -= collision.gameObject.GetComponent<AirframeScript>().crash_damage;
            collision.gameObject.GetComponent<Player>().curHp -= crash_damage;
            Camera.main.GetComponent<Ingame_manager>().Player_attacked();
        }
        else if (collision.gameObject.tag == "end_line")
        {
            Camera.main.GetComponent<Ingame_manager>().enemy_cnt--;
            Debug.Log(Camera.main.GetComponent<Ingame_manager>().enemy_cnt);
            Destroy(this.gameObject);
        }
    }
}
