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
    public bool invincible { get; set; }
    public Transform[] butts; // �ѱ��ִ� ������Ʈ�� ���
    public GameObject bullet;
    public float fire_rate;

    Rigidbody2D rigid;
    PolygonCollider2D col;
    ParticleSystem par_die;
    float[] angles; // �ѱ��ִ� ������Ʈ�� ���

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
        if (hp <= 0) // ���
        {
            Camera.main.GetComponent<Ingame_manager>().enemys.Remove(gameObject);
            ParticleSystem par = Instantiate(par_die);
            par.transform.position = transform.position; // �� ��� ȿ��

            Camera.main.GetComponent<Ingame_manager>().AddUltimateGuage(add_guage);
            Camera.main.GetComponent<Ingame_manager>().KillEnemy(score, exp, gold);
            Camera.main.GetComponent<Ingame_manager>().enemys.Remove(gameObject);

            //���� ���
            //�������� ������ �Ѹ���
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
