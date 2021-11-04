using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_InfoPanelListener : MonoBehaviour
{
    public GameObject image;
    public GameObject skill_name;
    public GameObject skill_explain;
    public GameObject explain;
    public GameObject selected_btn { get; set; }
    public bool pre_skill_active { get; set; }
    public bool remain_point { get; set; }
    
    void Start()
    {
        explain.SetActive(false);
    }

    public void Initiate()
    {
        image.GetComponent<Image>().sprite = selected_btn.GetComponent<Image>().sprite;
        skill_name.GetComponent<Text>().text = selected_btn.GetComponent<SkillButtonListener>().skill_name;
        skill_explain.GetComponent<Text>().text = selected_btn.GetComponent<SkillButtonListener>().skill_explain;
    }

    public void Research_click()
    {
        StartCoroutine(ResearchCoroutine());
    }
    IEnumerator ResearchCoroutine()
    {
        explain.SetActive(true);

        if (remain_point)
        {
            if (pre_skill_active)
            {
                if (selected_btn.GetComponent<SkillButtonListener>().cur_point < selected_btn.GetComponent<SkillButtonListener>().max_point)
                {
                    explain.GetComponent<Text>().text = "연구 완료.";
                    selected_btn.GetComponent<SkillButtonListener>().cur_point++;
                    selected_btn.GetComponent<SkillButtonListener>().skill_active = true;

                    PlayerPrefs.SetFloat("ex_hp", PlayerPrefs.GetFloat("ex_hp") * selected_btn.GetComponent<SkillButtonListener>().adds.ex_hp);
                    PlayerPrefs.SetFloat("ex_def", PlayerPrefs.GetFloat("ex_def") * selected_btn.GetComponent<SkillButtonListener>().adds.ex_def);
                    PlayerPrefs.SetFloat("ex_crash_dmg", PlayerPrefs.GetFloat("ex_crash_dmg") * selected_btn.GetComponent<SkillButtonListener>().adds.ex_crash_dmg);
                    PlayerPrefs.SetFloat("ex_bullet_dmg", PlayerPrefs.GetFloat("ex_bullet_dmg") * selected_btn.GetComponent<SkillButtonListener>().adds.ex_bullet_dmg);
                    PlayerPrefs.SetFloat("ex_fire_rate", PlayerPrefs.GetFloat("ex_fire_rate") * selected_btn.GetComponent<SkillButtonListener>().adds.ex_fire_rate);
                    PlayerPrefs.SetFloat("ex_crit_chance", PlayerPrefs.GetFloat("ex_crit_chance") + selected_btn.GetComponent<SkillButtonListener>().adds.ex_crit_chance);
                    PlayerPrefs.SetFloat("ex_crit_dmg", PlayerPrefs.GetFloat("ex_crit_dmg") * selected_btn.GetComponent<SkillButtonListener>().adds.ex_crit_dmg);
                    PlayerPrefs.SetFloat("ex_gold", PlayerPrefs.GetFloat("ex_gold") * selected_btn.GetComponent<SkillButtonListener>().adds.ex_gold);
                    PlayerPrefs.SetFloat("ex_exp", PlayerPrefs.GetFloat("ex_exp") * selected_btn.GetComponent<SkillButtonListener>().adds.ex_exp);
                    PlayerPrefs.SetFloat("ex_drop", PlayerPrefs.GetFloat("ex_drop") * selected_btn.GetComponent<SkillButtonListener>().adds.ex_drop);
                }
                else
                    explain.GetComponent<Text>().text = "최대 레벨입니다.";
            }
            else
                explain.GetComponent<Text>().text = "선행 스킬을 찍어주세요.";
        }
        else
            explain.GetComponent<Text>().text = "포인트가 부족합니다.";

        yield return new WaitForSeconds(1f);

        explain.SetActive(false);
    }

    public void Cancle_click()
    {
        gameObject.SetActive(false);
    }
}
