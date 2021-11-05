using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_info : MonoBehaviour
{
    public float hp;
    public float crash_damage; // �浹 ������
    public int score;
    public int exp;
    public int gold;
    public float add_guage;
    public float invincible_time; // �ش� �ð����� ����
    public int instantiate_count; // ������ ����� �����Ұ���
    public string kind; // ���� ���� �������� >> normal, elite, boss�� ���� 
    public string instantiate_point; // prefab���� �ǵ��� ����
    public Transform[] butts; // �ѱ��ִ� ������Ʈ�� ���
    public GameObject bullet;
    public float fire_rate;
    public GameObject item;

    SpriteRenderer sprite;
    Rigidbody2D rigid;
    PolygonCollider2D col;
    ParticleSystem par_die;
    bool invincible;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        par_die = Resources.Load<ParticleSystem>("Particle/TinyExplosion");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (butts.Length > 0)
            StartCoroutine(Attack());

        invincible = false;
    }

    IEnumerator InvincibleTime()
    {
        invincible = true;
        yield return new WaitForSeconds(invincible_time);
        invincible = false;
    }
    IEnumerator Attack()
    {
        while (true)
        {
            for (int i = 0; i < butts.Length; i++)
                Instantiate(bullet, butts[i].position, Quaternion.identity).GetComponent<BulletInfo>().SetFromEnemy(GV.GetDegree(transform.position, butts[i].position));

            yield return new WaitForSeconds(fire_rate);
        }
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
        if (hp <= 0) // ���
        {
            var im = Camera.main.GetComponent<Ingame_manager>();

            im.enemys.Remove(gameObject);
            ParticleSystem par = Instantiate(par_die);
            par.transform.position = transform.position; // �� ��� ȿ��

            im.AddUltimateGuage(add_guage);
            im.KillEnemy(score, exp, gold);
            im.enemys.Remove(gameObject);

            StopAllCoroutines();

            if (Random.Range(0f, 100f) < item.GetComponent<ItemInfo>().drop_rate)
                Instantiate(item, transform.position, Quaternion.identity);

            //���� ���
            //�������� ������ �Ѹ���
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) // �÷��̾� ��ü���� �浹�� ���
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                Camera.main.GetComponent<Ingame_manager>().enemys.Remove(gameObject);
                Destroy(gameObject);
                break;
        }
    }
    void OnTriggerStay2D(Collider2D col) // �÷��̾� ��ü���� �浹�� ���
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
