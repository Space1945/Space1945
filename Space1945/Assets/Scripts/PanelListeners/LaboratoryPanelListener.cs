using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LaboratoryPanelListener : MonoBehaviour
{
    public GameObject skill_info_panel;
    public GameObject remain_text;
    public GameObject[] skills_btn;
    public DB_Manager.ex_stats total_stat;
    public int total_point{ get; set; }
    public int used_point { get; set; }

    string skills;
    void Awake()
    {
        total_stat = new DB_Manager.ex_stats();
        total_point = PlayerPrefs.GetInt("pilot_level");
        skills = PlayerPrefs.GetString("pilot_skill");
        for (int i = 0; i < 5; i++)     //���� ó�� ��ų������ �޾ƿ� �ʱ�ȭ ���� ��ư�� ���� 5�� �ۿ� ����
        {
            skills_btn[i].GetComponent<SkillButtonListener>().cur_point = int.Parse(skills[i].ToString());
            if (i > 0 && i < 41)    //ù���� ��ų�� ���������� �رݵǴ� ���꽺ų ����
                used_point += int.Parse(skills[i].ToString());
            skills_btn[i].GetComponent<SkillButtonListener>().skill_active = skills[i] != '0';
            skills_btn[i].GetComponent<SkillButtonListener>().idx = i;
            skills_btn[i].GetComponent<SkillButtonListener>().Status_Add();
        }
        DB_Manager.Instance.research_total = total_stat;
        Show_remain_point();
        remain_text.transform.SetAsLastSibling();
        skill_info_panel.SetActive(false);
    }
    public void Save_Point()
    {
        for (int i = 0; i < 5; i++) //��ų ������ ��ȭ�� ���� ���� �̰͵� ��ư�� 5�� �ۿ���� 5�� ����
        {
            char change = (char)(skills_btn[i].GetComponent<SkillButtonListener>().cur_point + '0');
            if (skills[i] != change)
            {
                skills = skills.Remove(i, 1);
                skills = skills.Insert(i, change.ToString());
                used_point++;
            }
        }
        PlayerPrefs.SetString("pilot_skill", skills);
    }
    public void Show_remain_point()
    {
        remain_text.GetComponent<Text>().text = (total_point - used_point).ToString();
    }
    public void Load_Skill_info(int pre_skill, int idx)    //��ų ����â�� �ҷ���
    {
        skill_info_panel.SetActive(true);
        skill_info_panel.GetComponent<Skill_InfoPanelListener>().selected_btn = skills_btn[idx];
        skill_info_panel.GetComponent<Skill_InfoPanelListener>().pre_skill = skills_btn[pre_skill].GetComponent<SkillButtonListener>().skill_active;
        skill_info_panel.GetComponent<Skill_InfoPanelListener>().remain_point = total_point - used_point > 0;
        skill_info_panel.GetComponent<Skill_InfoPanelListener>().Initiate();
    }
}