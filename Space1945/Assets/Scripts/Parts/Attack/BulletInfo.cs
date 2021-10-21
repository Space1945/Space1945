using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    public float crash_damage;
    public float speed;
    public float crit_chance_p;
    public float crit_damage_p;
    
    public float shot_angle
    {
        get
        {
            return angle;
        }
    }
    public bool critical
    {
        get
        {
            return crit;
        }
    }

    float angle;
    bool crit = false;

    public void Reinforce(bool inherit)
    {
        angle = Random.Range(transform.parent.GetComponent<AtkScript>().min_angle, transform.parent.GetComponent<AtkScript>().max_angle);
        if (inherit)
            crit_chance_p = transform.parent.GetComponent<BulletInfo>().crit_chance_p;
        else
            crit_chance_p += DB_Manager.Instance.ex_total.ex_crit_chance;
        if (crit_chance_p > 100f)
            crit_chance_p = 100f;

        if (inherit)
            crit_damage_p = transform.parent.GetComponent<BulletInfo>().crit_damage_p;
        else
            crit_damage_p = DB_Manager.Instance.ex_total.ex_crit_dmg;

        if (Random.Range(1f, 100f) <= crit_chance_p) // 크리티컬일때
        {
            crash_damage = crash_damage * (1 + DB_Manager.Instance.ex_total.ex_crash_dmg / 100f) * (1 + crit_damage_p / 100f);
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            crit = true;
        }
        else
        {
            crash_damage = crash_damage * (1 + DB_Manager.Instance.ex_total.ex_crash_dmg / 100f);
        }
    }
}
