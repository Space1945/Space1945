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
        for (int i = 0; i < 5; i++)     //제일 처음 스킬레벨을 받아와 초기화 해줌 버튼이 아직 5개 밖에 없음
        {
            skills_btn[i].GetComponent<SkillButtonListener>().cur_point = int.Parse(skills[i].ToString());
            if (i > 0 && i < 41)    //첫번재 스킬과 레벨에따라 해금되는 서브스킬 제외
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
        for (int i = 0; i < 5; i++) //스킬 찍은후 변화된 값들 저장 이것도 버튼이 5개 밖에없어서 5로 고정
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
    public void Load_Skill_info(int pre_skill, int idx)    //스킬 정보창을 불러옴
    {
        skill_info_panel.SetActive(true);
        skill_info_panel.GetComponent<Skill_InfoPanelListener>().selected_btn = skills_btn[idx];
        skill_info_panel.GetComponent<Skill_InfoPanelListener>().pre_skill = skills_btn[pre_skill].GetComponent<SkillButtonListener>().skill_active;
        skill_info_panel.GetComponent<Skill_InfoPanelListener>().remain_point = total_point - used_point > 0;
        skill_info_panel.GetComponent<Skill_InfoPanelListener>().Initiate();
    }
}