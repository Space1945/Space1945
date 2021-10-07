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
    public int invincible_time; int it = 0;
    public int instantiate_count; // ������ ����� �����Ұ���
    public string kind; // ���� ���� �������� >> normal, elite, boss�� ���� 

    public string instantiate_point { get; set; }

    Rigidbody2D rigid;
    PolygonCollider2D col;
    ParticleSystem par_die;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        par_die = Resources.Load<ParticleSystem>("Particle/Enemy_die_particle");
    }

    void FixedUpdate()
    {
        it++;
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player_bullet") // �Ѿ��̸�
        {
            hp -= collision.gameObject.GetComponent<BulletInfo>().crash_damage;
            if (collision.gameObject.GetComponent<Penetrate>() == null) // �ش� źȯ�� ������ �ƴϸ�
                Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "player" && it > invincible_time) // 1��
        {
            it = 0;
            hp -= collision.gameObject.GetComponent<AirframeScript>().crash_damage;
            collision.gameObject.GetComponent<Player>().curHp -= crash_damage;
            Camera.main.GetComponent<Ingame_manager>().DisplayPlayersHP();
        }
        else if (collision.gameObject.tag == "end_line")
        {
            Camera.main.GetComponent<Ingame_manager>().enemys.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
