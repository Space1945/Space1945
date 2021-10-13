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
    public float invincible_time; // 해당 시간동안 무적
    public int instantiate_count; // 원본을 몇개까지 복제할건지
    public string kind; // 몹이 무슨 종류인지 >> normal, elite, boss로 구분 
    public string instantiate_point; // prefab에서 건들지 말것
    public bool invincible { get; set; }
    public GameObject[] butts;
    public GameObject bullet;
    public float fire_rate;

    Rigidbody2D rigid;
    PolygonCollider2D col;
    ParticleSystem par_die;

    Vector2 dis;

    // Start is called before the first frame update
    void Start()
    {
        Initiate();

        if (butts.Length > 0)
            StartCoroutine(Attack());
    }

    void Initiate()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        par_die = Resources.Load<ParticleSystem>("Particle/Enemy_die_particle");
        invincible = false;
    }

    IEnumerator Invincible()
    {
        yield return new WaitForSeconds(invincible_time);

        invincible = false;
    }
    IEnumerator Attack()
    {
        while (true)
        {
            foreach (GameObject butt in butts)
            {
                bullet.transform.position = new Vector2(butt.transform.position.x, butt.transform.position.y);
                GameObject bullet_clone = Instantiate(bullet);
                dis = butt.transform.position - gameObject.transform.position;
                bullet_clone.GetComponent<Rigidbody2D>().velocity = dis.normalized * bullet_clone.GetComponent<BulletInfo>().speed;
                bullet_clone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 + GV.radian_to_degree(GV.GetRadian(dis.x, dis.y))));
            }
            yield return new WaitForSeconds(fire_rate);
        }
    }

    void FixedUpdate()
    {
        if (hp <= 0) // 사망
        {
            Camera.main.GetComponent<Ingame_manager>().enemys.Remove(gameObject);
            ParticleSystem par = Instantiate(par_die);
            par.transform.position = transform.position; // 적 사망 효과

            Camera.main.GetComponent<Ingame_manager>().AddUltimateGuage(add_guage);
            Camera.main.GetComponent<Ingame_manager>().KillEnemy(score, exp, gold);
            Camera.main.GetComponent<Ingame_manager>().enemys.Remove(gameObject);

            //사운드 출력
            //일정몹이 아이템 뿌리기
            Destroy(gameObject);
        }
    }

    public void Attacked(float crash_damage)
    {
        invincible = true;
        StartCoroutine(Invincible());
        hp -= crash_damage;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "end_line":
                Camera.main.GetComponent<Ingame_manager>().enemys.Remove(gameObject);
                Destroy(gameObject);
                break;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "player":
                col.gameObject.GetComponent<Player>().Attacked(crash_damage);
                break;
        }
    }
}
