using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletInfo : MonoBehaviour
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

    void Awake() // instantiate�� ���� ���ʽ���
    {
        if (crit_chance_p > 100f)
            crit_chance_p = 100f;
        crit_damage_p += DB_Manager.Instance.ex_total.ex_crit_dmg;

        if (Random.Range(1f, 100f) <= crit_chance_p) // ũ��Ƽ���϶�
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

    public void Set(float shot_angle) // Awake �������� ����, �� ���� Start ����
    {
        angle = shot_angle;
    }
}
