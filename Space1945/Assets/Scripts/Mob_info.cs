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

    Rigidbody2D rigid;
    PolygonCollider2D col;
    //ParticleSystem par_die;
    bool invincible;

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
        // par_die = Resources.Load<ParticleSystem>("Particle/Enemy_die_particle");
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
            Camera.main.GetComponent<Ingame_manager>().enemys.Remove(gameObject);
            //ParticleSystem par = Instantiate(par_die);
            //par.transform.position = transform.position; // �� ��� ȿ��

            Camera.main.GetComponent<Ingame_manager>().AddUltimateGuage(add_guage);
            Camera.main.GetComponent<Ingame_manager>().KillEnemy(score, exp, gold);
            Camera.main.GetComponent<Ingame_manager>().enemys.Remove(gameObject);

            StopAllCoroutines();

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
                    float new_crash_damage = crash_damage - col.gameObject.GetComponent<AirframeScript>().basic_def;
                    col.gameObject.GetComponent<AirframeScript>().Attacked(new_crash_damage > 0 ? new_crash_damage : 0);
                    BodyAttacked(col.gameObject.GetComponent<AirframeScript>().crash_damage);
                }
                break;
        }
    }
}
