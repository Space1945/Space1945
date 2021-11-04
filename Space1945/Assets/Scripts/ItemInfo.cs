using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public DB_Manager.ex_stats temp_adds; // ex_hp�� ȸ��, �������� �Ͻ��� ����
    public float add_guage; // ������ ����
    public float effect_time; // ȿ�� ���ӽð�
    public float item_move_speed;
    public int bound_count;

    Rigidbody2D rigid;
    CircleCollider2D col;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

        rigid.velocity = new Vector2(10, 10) * item_move_speed;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionStay2D(Collision2D collision) // ���浹
    {
        GameObject col = collision.gameObject;

        if (col.tag == "game_line")
        {
            bound_count--;
            if (bound_count <= 0)
                gameObject.layer = 0;
        }
        else if (col.tag == "end_line")
            Destroy(gameObject);
        else if (col.tag == "player") // ������ ����
        {
            /*if (!Camera.main.GetComponent<Ingame_manager>().ultimate_use) // �� ������� �ƴҶ��� �� ���� �� ����
            {
                col.GetComponent<AirframeScript>().cur_hp += col.GetComponent<AirframeScript>().max_hp / 100f * temp_adds.ex_hp;
                col.GetComponent<AirframeScript>().atk.GetComponent<AtkInterface>().TemporaryReinforce(
                    effect_time,
                    col.GetComponent<AirframeScript>().atk.GetComponent<AtkInterface>()._fire_rate * (1 + temp_adds.ex_fire_rate / 100f),
                    col.GetComponent<AirframeScript>().atk.GetComponent<AtkInterface>()._fire_cnt_per_shot,
                    temp_adds.ex_crit_chance,
                    temp_adds.ex_crit_dmg
                );
                col.GetComponent<AirframeScript>().TemporaryReinforce(
                    effect_time,
                    col.GetComponent<AirframeScript>().basic_def * (1 + temp_adds.ex_def / 100f),
                    col.GetComponent<AirframeScript>().crash_damage * (1 + temp_adds.ex_crash_dmg / 100f)
                );
                Camera.main.GetComponent<Ingame_manager>().AddUltimateGuage(add_guage);
                Camera.main.GetComponent<Ingame_manager>().AdditionalGoldExp(effect_time, temp_adds.ex_gold, temp_adds.ex_exp);

                Destroy(gameObject);
            }*/
        }
    }
}
