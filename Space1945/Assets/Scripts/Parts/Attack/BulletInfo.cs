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

    float origin_crash_damage;
    float origin_crit_chance_p;
    float origin_crit_damage_p;

    void Awake() // instantiate된 직후 최초실행
    {
        origin_crash_damage = crash_damage;
        origin_crit_chance_p = crit_chance_p;
        origin_crit_damage_p = crit_damage_p;
    }
    public void SetFromPlayer(float shot_angle, float adtl_crashd, float adtl_cc, float adtl_cd) // 플레이어가 발사한 총알을 강화 및 다시 세팅
    {
        angle = shot_angle;
        crit_chance_p += Camera.main.GetComponent<Ingame_manager>().ex_total.ex_crit_chance + adtl_cc;
        if (crit_chance_p > 100f)
            crit_chance_p = 100f;
        crit_damage_p *= Camera.main.GetComponent<Ingame_manager>().ex_total.ex_crit_dmg * adtl_cd;

        if (Random.Range(1f, 100f) <= crit_chance_p) // 크리티컬일때
        {
            crit = true;
            crash_damage *= Camera.main.GetComponent<Ingame_manager>().ex_total.ex_bullet_dmg * crit_damage_p * adtl_crashd;
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        else
        {
            crit = false;
            crash_damage *= Camera.main.GetComponent<Ingame_manager>().ex_total.ex_bullet_dmg * adtl_crashd;
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
            crash_damage *= crit_damage_p;
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

    public void RollBackFromPlayer()
    {
        crash_damage = origin_crash_damage;
        crit_chance_p = origin_crit_chance_p;
        crit_damage_p = origin_crit_damage_p;

        if (crit) // 크리티컬 총알이었다면 다시 원래 색깔로
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }
}
