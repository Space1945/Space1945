using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScript : MonoBehaviour
{
    public int gold;
    public DB_Manager.ex_stats adds;

    void Awake()
    {
        DB_Manager.Instance.ex_total.ex_hp += adds.ex_hp;
        DB_Manager.Instance.ex_total.ex_def += adds.ex_def;
        DB_Manager.Instance.ex_total.ex_crash_dmg += adds.ex_crash_dmg;
        DB_Manager.Instance.ex_total.ex_bullet_dmg += adds.ex_bullet_dmg;
        DB_Manager.Instance.ex_total.ex_fire_rate += adds.ex_fire_rate;
        DB_Manager.Instance.ex_total.ex_crit_chance += adds.ex_crit_chance;
        DB_Manager.Instance.ex_total.ex_crit_dmg += adds.ex_crit_dmg;
        DB_Manager.Instance.ex_total.ex_gold += adds.ex_gold;
        DB_Manager.Instance.ex_total.ex_exp += adds.ex_exp;
        DB_Manager.Instance.ex_total.ex_drop += adds.ex_drop;
    }
}