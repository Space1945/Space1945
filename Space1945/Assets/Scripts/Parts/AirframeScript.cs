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
    float fire_rate; // 초 단위
    bool ready = false; // 준비완료

    void Start()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (ready)
        {
            for (int i = 0; i < butts.Length; i++)
                Instantiate(bullet, butts[i].position, Quaternion.identity, butts[i]);
            
            yield return new WaitForSeconds(fire_rate);
        }
    }

    public void SetReady()
    {
        atk = DB_Manager.Instance.using_atk;
        def = DB_Manager.Instance.using_def;
        sub_left = DB_Manager.Instance.using_sub_left;
        sub_right = DB_Manager.Instance.using_sub_right;

        fire_rate = atk.GetComponent<AtkScript>().fire_rate;
        bullet = atk.GetComponent<AtkScript>().bullet;

        ready = true;
    }
}
