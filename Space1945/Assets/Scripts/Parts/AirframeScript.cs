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
    float bullet_damage;
    public float fire_rate { get; set; }
    public int fire_cnt_per_shot { get; set; }
    float max_angle;
    float min_angle;

    public Coroutine atk_coroutine { get; set; }

    void Start()
    {
        atk = DB_Manager.Instance.using_atk;
        def = DB_Manager.Instance.using_def;
        sub_left = DB_Manager.Instance.using_sub_left;
        sub_right = DB_Manager.Instance.using_sub_right;

        bullet = atk.GetComponent<AtkScript>().bullet;
        bullet_damage = bullet.GetComponent<BulletInfo>().crash_damage;
        fire_rate = atk.GetComponent<AtkScript>().fire_rate;
        fire_cnt_per_shot = atk.GetComponent<AtkScript>().fire_cnt_per_shot;
        max_angle = atk.GetComponent<AtkScript>().max_angle;
        min_angle = atk.GetComponent<AtkScript>().min_angle;

        atk_coroutine = StartCoroutine(Attack(fire_rate, fire_cnt_per_shot));
    }

    public IEnumerator Attack(float fire_rate, int fire_cnt_per_shot)
    {
        while (true)
        {
            for (int i = 0; i < butts.Length; i++)
                for (int j = 0; j < fire_cnt_per_shot; j++)
                {
                    GameObject bc = Instantiate(bullet, butts[i].position, Quaternion.identity);
                    bc.GetComponent<BulletInfo>().shot_angle = Random.Range(min_angle, max_angle);
                }

            yield return new WaitForSeconds(fire_rate);
        }
    }
}
