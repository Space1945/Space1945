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
    } // get�� ����
    public bool critical
    {
        get
        {
            return crit;
        }
    } // get�� ����

    float angle;
    bool crit = false;

    void Awake()
    {
        
    }
    public void SetFromPlayer(float shot_angle, bool parent_critical, float crit_damage_p) // �÷��̾ �߻��� �Ѿ��� �θ���� ��
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
    public void SetFromEnemy(float shot_angle, bool parent_critical, float crit_damage_p) // ���� �߻��� �Ѿ��� �θ���� ��
    {

    }
    public void SetFromNone(float crash_damage, float speed, float crit_damage_p, float shot_angle, bool critial) // ���ο� ������ �ܼ��� �ʱ�ȭ�� �ϰ� ���� ��
    {
        this.crash_damage = crash_damage;
        this.speed = speed;
        this.crit_damage_p = crit_damage_p;
        angle = shot_angle;
        crit = critial;
    }
}
