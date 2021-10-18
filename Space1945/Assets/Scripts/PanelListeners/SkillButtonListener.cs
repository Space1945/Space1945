using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButtonListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject lab_panel;

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
    public string skill_explain;

    private DB_Manager.ex_stats status = new DB_Manager.ex_stats();
    public bool skill_active = false;
    private bool isBtnDown = false;
    int cou = 0;
    private void Start()
    {
        status.ex_hp = ex_hp;
        status.ex_def = ex_def;
        status.ex_crash_dmg = ex_crash_dmg;
        status.ex_bullet_dmg = ex_bullet_dmg;
        status.ex_fire_rate = ex_fire_rate;
        status.ex_crit_chance = ex_crit_chance;
        status.ex_crit_dmg = ex_crit_dmg;
        status.ex_gold = ex_gold;
        status.ex_exp = ex_exp;
        status.ex_drop = ex_drop;
    }
    private void FixedUpdate()
    {
        if (isBtnDown)
        {
            cou++;
        }
        if (cou > 50)
        {
            lab_panel.GetComponent<LaboratoryPanelListener>().Load_Explain(skill_explain);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isBtnDown = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isBtnDown = false;
        if (cou > 50)
            lab_panel.GetComponent<LaboratoryPanelListener>().Check_Explain();
        else if(!skill_active)
            skill_active = lab_panel.GetComponent<LaboratoryPanelListener>().Skill_Active(pre_skill, status);
        cou = 0;
    }
}