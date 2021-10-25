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
    } // get만 가능
    public bool critical
    {
        get
        {
            return crit;
        }
    } // get만 가능

    float angle;
    bool crit = false;

    void Awake()
    {
        
    }
    public void SetFromPlayer(float shot_angle, bool parent_critical, float crit_damage_p) // 플레이어가 발사한 총알을 부모로할 때
    {
        angle = shot_angle;
        this.crit_damage_p = crit_damage_p;
        if (parent_critical)
        {
            crit = true;
            crash_damage = crash_damage * (1 + Camera.main.GetComponent<Ingame_manager>().ex_total.ex_crash_dmg / 100f) * (1 + crit_damage_p / 100f);
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        else
        {
            crit = false;
            crash_damage = crash_damage * (1 + Camera.main.GetComponent<Ingame_manager>().ex_total.ex_crash_dmg / 100f);
        }
    }
    public void SetFromEnemy(float shot_angle, bool parent_critical, float crit_damage_p) // 적이 발사한 총알을 부모로할 때
    {

    }
    public void SetFromNone(float crash_damage, float speed, float crit_damage_p, float shot_angle, bool critial) // 새로운 값으로 단순히 초기화만 하고 싶을 때
    {
        this.crash_damage = crash_damage;
        this.speed = speed;
        this.crit_damage_p = crit_damage_p;
        angle = shot_angle;
        crit = critial;
    }
}
