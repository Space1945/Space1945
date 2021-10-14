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
    public Transform[] butts; // 총구있는 오브젝트만 사용
    public GameObject bullet;
    public float fire_rate;

    Rigidbody2D rigid;
    PolygonCollider2D col;
    ParticleSystem par_die;
    float[] angles; // 총구있는 오브젝트만 사용

    // Start is called before the first frame update
    void Start()
    {
        Initiate();

        if (butts.Length > 0)
        {
            CalcAngleOfButts();
            StartCoroutine(Attack());
        }
    }

    void Initiate()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        par_die = Resources.Load<ParticleSystem>("Particle/Enemy_die_particle");
        invincible = false;
    }

    void CalcAngleOfButts()
    {
        angles = new float[butts.Length];
        for (int i = 0; i < butts.Length; i++)
            angles[i] = Vector2.Angle(gameObject.transform.position, butts[i].transform.position);
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
            for (int i = 0; i < butts.Length; i++)
            {
                bullet.transform.position = butts[i].position;
                Instantiate(bullet).GetComponent<NormalEnemy>().SetBullet((butts[i].position - gameObject.transform.position).normalized, angles[i]);
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
