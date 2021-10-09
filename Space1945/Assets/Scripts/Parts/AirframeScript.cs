using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirframeScript : MonoBehaviour
{
    public float maxHp;
    public int crash_damage;
    public int gold;

    GameObject atk;
    GameObject def;
    GameObject sub_left;
    GameObject sub_right;

    Transform[] butts;
    GameObject bullet;
    float fire_rate; // 초 단위
    bool ready = false; // 준비완료

    private void Start()
    {
        butts = GetComponentsInChildren<Transform>();

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (ready)
        {
            foreach (Transform butt in butts)
                if (butt.gameObject.tag != "player")
                {
                    bullet.transform.position = new Vector2(butt.position.x, butt.position.y);
                    Instantiate(bullet);
                }
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
