using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaboratoryPanelListener : MonoBehaviour
{
    public GameObject explain;
    public GameObject[] skills;
    private void Start()
    {
        explain.SetActive(false);
    }
    public void Load_Explain(string skill_explain)
    {
        explain.SetActive(true);
        explain.GetComponent<Text>().text = skill_explain;
    }
    public void Check_Explain()
    {
        if (explain.activeInHierarchy)
            explain.SetActive(false);
    }
    public bool Skill_Active(int pre_skill, DB_Manager.ex_stats status)
    {
        if (skills[pre_skill].GetComponent<SkillButtonListener>().skill_active)
        {
            DB_Manager.Instance.ex_total.ex_hp += status.ex_hp;
            DB_Manager.Instance.ex_total.ex_def += status.ex_def;
            DB_Manager.Instance.ex_total.ex_crash_dmg += status.ex_crash_dmg;
            DB_Manager.Instance.ex_total.ex_bullet_dmg += status.ex_bullet_dmg;
            DB_Manager.Instance.ex_total.ex_fire_rate += status.ex_fire_rate;
            DB_Manager.Instance.ex_total.ex_crit_chance += status.ex_crit_chance;
            DB_Manager.Instance.ex_total.ex_crit_dmg += status.ex_crit_dmg;
            DB_Manager.Instance.ex_total.ex_gold += status.ex_gold;
            DB_Manager.Instance.ex_total.ex_exp += status.ex_exp;
            DB_Manager.Instance.ex_total.ex_drop += status.ex_drop;
            return true;
        }
        return false;
    }
}