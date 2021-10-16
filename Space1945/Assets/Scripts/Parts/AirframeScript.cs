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
    float[] angles;
    float fire_rate; // 초 단위
    bool ready = false; // 준비완료

    void Start()
    {
        SetReady();

        if (angles.Length > 0)
            StartCoroutine(Attack());
        else
            Debug.Log("AirframeScript/instantiate_cnt Error");
    }

    IEnumerator Attack()
    {
        while (ready)
        {
            for (int i = 0; i < butts.Length; i++)
                for (int j = 0; j < angles.Length; j++)
                {
                    butts[i].GetComponent<ButtInfo>().angle = angles[i];
                    Instantiate(bullet, butts[i].position, Quaternion.identity, butts[i]);
                }

            yield return new WaitForSeconds(fire_rate);
        }
    }

    void SetReady()
    {
        atk = DB_Manager.Instance.using_atk;
        def = DB_Manager.Instance.using_def;
        sub_left = DB_Manager.Instance.using_sub_left;
        sub_right = DB_Manager.Instance.using_sub_right;

        bullet = atk.GetComponent<AtkScript>().bullet;
        angles = atk.GetComponent<AtkScript>().angles;
        fire_rate = atk.GetComponent<AtkScript>().fire_rate;

        ready = true;
    }
}
