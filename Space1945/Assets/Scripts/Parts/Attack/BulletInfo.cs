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
    bool crit;

    void Awake() // instantiate된 직후 최초실행
    {
        
    }
    public void SetFromPlayer(float shot_angle) // 플레이어가 발사한 총알을 강화 및 다시 세팅
    {
        angle = shot_angle;
        crit_chance_p += Camera.main.GetComponent<Ingame_manager>().ex_total.ex_crit_chance;
        if (crit_chance_p > 100f)
            crit_chance_p = 100f;
        crit_damage_p += Camera.main.GetComponent<Ingame_manager>().ex_total.ex_crit_dmg;

        if (Random.Range(1f, 100f) <= crit_chance_p) // 크리티컬일때
        {
            crit = true;
            crash_damage = crash_damage * (1 + Camera.main.GetComponent<Ingame_manager>().ex_total.ex_bullet_dmg / 100f) * (1 + crit_damage_p / 100f);
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        else
        {
            crit = false;
            crash_damage = crash_damage * (1 + Camera.main.GetComponent<Ingame_manager>().ex_total.ex_bullet_dmg / 100f);
        }
    }
    public void SetFromEnemy(float shot_angle)
    {
        angle = shot_angle;
        if (crit_chance_p > 100f)
            crit_chance_p = 100f;

        if (Random.Range(1f, 100f) <= crit_chance_p) // 크리티컬일때
        {
            crit = true;
            crash_damage = crash_damage * (1 + crit_damage_p / 100f);
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
        else
        {
            crit = false;
        }
    }
    public void SetFromNone()
    {

    }
}
