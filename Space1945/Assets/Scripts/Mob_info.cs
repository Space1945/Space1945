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
    public float invincible_time;
    public int instantiate_count; // ������ ����� �����Ұ���
    public string kind; // ���� ���� �������� >> normal, elite, boss�� ���� 

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
        if (hp <= 0) // ���
        {
            Camera.main.GetComponent<Ingame_manager>().enemy_cnt--;
            Debug.Log(Camera.main.GetComponent<Ingame_manager>().enemy_cnt);
            ParticleSystem par = Instantiate(par_die);
            par.transform.position = this.transform.position; // �� ��� ȿ��

            Camera.main.GetComponent<Ingame_manager>().Plus_ultimate_guage(add_guage);
            Camera.main.GetComponent<Ingame_manager>().Add_point(score, exp, gold);
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
        else if (collision.gameObject.tag == "player" && invincible_time > GV.INVINCIBLE_TIME) // 1��
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
