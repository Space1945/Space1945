using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerivedBulletInfo : MonoBehaviour
{
    public float crash_damage;
    public float speed;
    
    float crit_damage_p;
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

    void Awake()
    {
        angle = Random.Range(0f, 360f);
        crit_damage_p = transform.parent.GetComponent<BulletInfo>().crit_damage_p;
        if (transform.parent.GetComponent<BulletInfo>().critical)
        {
            crit = true;
            crash_damage = crash_damage * (1 + DB_Manager.Instance.ex_total.ex_crash_dmg / 100f) * (1 + crit_damage_p / 100f);
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        else
        {
            crit = false;
            crash_damage = crash_damage * (1 + DB_Manager.Instance.ex_total.ex_crash_dmg / 100f);
        }
    }
}
