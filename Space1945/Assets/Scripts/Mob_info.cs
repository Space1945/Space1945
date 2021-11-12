using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_info : MonoBehaviour
{
    public float max_hp;
    float hp;
    public float crash_damage; // 충돌 데미지
    public int score;
    public int exp;
    public int gold;
    public float add_guage;
    public float invincible_time; // 해당 시간동안 무적
    public int instantiate_count; // 원본을 몇개까지 복제할건지
    public string kind; // 몹이 무슨 종류인지 >> normal, elite, boss로 구분 
    public string instantiate_point; // prefab에서 건들지 말것
    public Transform[] butts; // 총구있는 오브젝트만 사용
    public GameObject bullet;
    public float fire_rate;
    public int burst_cnt;
    public GameObject item;

    SpriteRenderer sprite;
    Rigidbody2D rigid;
    PolygonCollider2D col;
    ParticleSystem par_die;
    bool invincible;
    Ingame_manager ims;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        ims = Camera.main.GetComponent<Ingame_manager>();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        invincible = false;
        hp = max_hp;
        Debug.Log(bullet.GetInstanceID());
    }

    IEnumerator InvincibleTime()
    {
        invincible = true;
        yield return new WaitForSeconds(invincible_time);
        invincible = false;
    }

    public void BodyAttacked(float crash_damage)
    {
        StartCoroutine(InvincibleTime());
        Attacked(crash_damage);
    }
    public void Attacked(float crash_damage)
    {
        hp -= crash_damage;
    }

    void FixedUpdate()
    {
        if (hp <= 0) // 사망
        {
            ims.AddUltimateGuage(add_guage);
            ims.KillEnemy(score, exp, gold);
            ims.enemys.Remove(gameObject);

            StopAllCoroutines();

            if (Random.Range(0f, 100f) < item.GetComponent<ItemInfo>().drop_rate)
                Instantiate(item, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) // 플레이어 개체와의 충돌만 담당
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                Camera.main.GetComponent<Ingame_manager>().enemys.Remove(gameObject);
                Destroy(gameObject);
                break;
        }
    }
    void OnTriggerStay2D(Collider2D col) // 플레이어 개체와의 충돌만 담당
    {
        switch (col.gameObject.tag)
        {
            case "player":
                if (!invincible)
                {
                    var afs = col.gameObject.GetComponent<AirframeScript>();

                    float new_crash_damage = crash_damage - afs.basic_def;
                    afs.Attacked(new_crash_damage > 0 ? new_crash_damage : 0);
                    BodyAttacked(afs.crash_damage);
                }
                break;
        }
    }
}
