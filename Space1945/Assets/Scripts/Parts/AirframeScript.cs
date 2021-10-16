using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirframeScript : MonoBehaviour
{
    public float max_hp;
    public float crash_damage;
    public int gold;
    public Transform[] butts;

    GameObject atk;
    GameObject def;
    GameObject sub_left;
    GameObject sub_right;

    GameObject bullet;
    int fire_cnt_per_shot;
    float max_angle;
    float min_angle;
    float fire_rate; // 초 단위
    bool ready = false; // 준비완료

    void Start()
    {
        SetReady();

        if (butts.Length > 0)
            StartCoroutine(Attack());
        else
            Debug.Log("AirframeScript/instantiate_cnt Error");
    }

    void SetReady()
    {
        atk = DB_Manager.Instance.using_atk;
        def = DB_Manager.Instance.using_def;
        sub_left = DB_Manager.Instance.using_sub_left;
        sub_right = DB_Manager.Instance.using_sub_right;

        bullet = atk.GetComponent<AtkScript>().bullet;
        fire_cnt_per_shot = atk.GetComponent<AtkScript>().fire_cnt_per_shot;
        max_angle = atk.GetComponent<AtkScript>().max_angle;
        min_angle = atk.GetComponent<AtkScript>().min_angle;
        fire_rate = atk.GetComponent<AtkScript>().fire_rate;

        ready = true;
    }

    IEnumerator Attack()
    {
        while (ready)
        {
            for (int i = 0; i < butts.Length; i++)
                for (int j = 0; j < fire_cnt_per_shot; j++)
                    Instantiate(bullet, butts[i].position, Quaternion.identity, butts[i]).transform.GetComponent<BulletInfo>().shot_angle = Random.Range(min_angle, max_angle);

            yield return new WaitForSeconds(fire_rate);
        }
    }
}
