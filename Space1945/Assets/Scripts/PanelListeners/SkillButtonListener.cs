using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButtonListener : MonoBehaviour
{
    public GameObject lab_panel;

    public int max_point;
    public int cur_point;

    public float ex_hp;
    public float ex_def;
    public float ex_crash_dmg;
    public float ex_bullet_dmg;
    public float ex_fire_rate;
    public float ex_crit_chance;
    public float ex_crit_dmg;
    public float ex_gold;
    public float ex_exp;
    public float ex_drop;
    public int pre_skill;
    public int idx;
    public string skill_explain;
    public string skill_name;

    public bool skill_active = false;
    public void Call_Load_Skill_info()
    {
        lab_panel.GetComponent<LaboratoryPanelListener>().Load_Skill_info(pre_skill, idx);
    }
    public void Status_Add()    //게임 시작시 스킬렙에 맞게 total 초기화
    {
        lab_panel.GetComponent<LaboratoryPanelListener>().total_stat.ex_hp += ex_hp * cur_point;
        lab_panel.GetComponent<LaboratoryPanelListener>().total_stat.ex_def += ex_def * cur_point;
        lab_panel.GetComponent<LaboratoryPanelListener>().total_stat.ex_crash_dmg += ex_crash_dmg * cur_point;
        lab_panel.GetComponent<LaboratoryPanelListener>().total_stat.ex_bullet_dmg += ex_bullet_dmg * cur_point;
        lab_panel.GetComponent<LaboratoryPanelListener>().total_stat.ex_fire_rate += ex_fire_rate * cur_point;
        lab_panel.GetComponent<LaboratoryPanelListener>().total_stat.ex_crit_chance += ex_crit_chance * cur_point;
        lab_panel.GetComponent<LaboratoryPanelListener>().total_stat.ex_crit_dmg += ex_crit_dmg * cur_point;
        lab_panel.GetComponent<LaboratoryPanelListener>().total_stat.ex_gold += ex_gold * cur_point;
        lab_panel.GetComponent<LaboratoryPanelListener>().total_stat.ex_exp += ex_exp * cur_point;
        lab_panel.GetComponent<LaboratoryPanelListener>().total_stat.ex_drop += ex_drop * cur_point;
    }
    public bool Ex_Status_Add() //스킬 찍을때 최대렙이 아니면 스탯증가
    {
        if (cur_point < max_point)
        {
            cur_point++;
            skill_active = true;
            DB_Manager.Instance.research_total.ex_hp += ex_hp;
            DB_Manager.Instance.research_total.ex_def += ex_def;
            DB_Manager.Instance.research_total.ex_crash_dmg += ex_crash_dmg;
            DB_Manager.Instance.research_total.ex_bullet_dmg += ex_bullet_dmg;
            DB_Manager.Instance.research_total.ex_fire_rate += ex_fire_rate;
            DB_Manager.Instance.research_total.ex_crit_chance += ex_crit_chance;
            DB_Manager.Instance.research_total.ex_crit_dmg += ex_crit_dmg;
            DB_Manager.Instance.research_total.ex_gold += ex_gold;
            DB_Manager.Instance.research_total.ex_exp += ex_exp;
            DB_Manager.Instance.research_total.ex_drop += ex_drop;
            lab_panel.GetComponent<LaboratoryPanelListener>().Save_Point();
            lab_panel.GetComponent<LaboratoryPanelListener>().Show_remain_point();
            return true;
        }
        return false;
    }
}