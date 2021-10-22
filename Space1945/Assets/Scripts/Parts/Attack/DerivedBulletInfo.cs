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
        
    }

    public void Set(float shot_angle, bool parent_critical, float crit_damage_p) // Awake 다음으로 실행, 그 이후 Start 실행
    {
        angle = shot_angle;
        this.crit_damage_p = crit_damage_p;
        if (parent_critical)
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
