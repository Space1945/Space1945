using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirframeScript : MonoBehaviour
{
    public Transform[] butts;
    public float max_hp;
    public float basic_def;
    public float crash_damage;
    public int gold;

    GameObject atk;
    GameObject def;
    GameObject sub_left;
    GameObject sub_right;

    GameObject bullet;
    public float bullet_damage_p { get; set; }
    public float bullet_damage { get; set; }
    public float fire_rate { get; set; }
    public int fire_cnt_per_shot { get; set; }
    float max_angle { get; set; }
    float min_angle { get; set; }

    Coroutine atk_coroutine;

    void Start()
    {
        max_hp *= 1 + DB_Manager.Instance.ex_total.ex_hp / 100f;
        basic_def *= 1 + DB_Manager.Instance.ex_total.ex_def / 100f;

        atk = DB_Manager.Instance.using_atk;
        def = DB_Manager.Instance.using_def;
        sub_left = DB_Manager.Instance.using_sub_left;
        sub_right = DB_Manager.Instance.using_sub_right;

        bullet = atk.GetComponent<AtkScript>().bullet;
        bullet_damage_p = 1 + DB_Manager.Instance.ex_total.ex_bullet_dmg / 100f;
        bullet_damage = bullet.GetComponent<BulletInfo>().crash_damage * bullet_damage_p;
        fire_rate = atk.GetComponent<AtkScript>().fire_rate * (1 - DB_Manager.Instance.ex_total.ex_fire_rate / 100f);
        fire_cnt_per_shot = atk.GetComponent<AtkScript>().fire_cnt_per_shot;
        max_angle = atk.GetComponent<AtkScript>().max_angle;
        min_angle = atk.GetComponent<AtkScript>().min_angle;

        atk_coroutine = StartCoroutine(Attack(bullet_damage, fire_rate, fire_cnt_per_shot));
    }

    IEnumerator Attack(float bullet_damage, float fire_rate, int fire_cnt_per_shot)
    {
        if (fire_rate < 0.05f)
            fire_rate = 0.05f;

        while (true)
        {
            for (int i = 0; i < butts.Length; i++)
                for (int j = 0; j < fire_cnt_per_shot; j++)
                {
                    GameObject bc = Instantiate(bullet, butts[i].position, Quaternion.identity);
                    bc.GetComponent<BulletInfo>().crash_damage = bullet_damage;
                    bc.GetComponent<BulletInfo>().add_p = bullet_damage_p;
                    bc.GetComponent<BulletInfo>().shot_angle = Random.Range(min_angle, max_angle);
                }

            yield return new WaitForSeconds(fire_rate);
        }
    }

    public void StopAttackCoroutine()
    {
        StopCoroutine(atk_coroutine);
    }
    public void StartAttackCoroutine(float bullet_damage, float fire_rate, int fire_cnt_per_shot)
    {
        atk_coroutine = StartCoroutine(Attack(bullet_damage, fire_rate, fire_cnt_per_shot));
    }
}
