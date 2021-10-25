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
    public bool pre_skill { get; set; }
    public bool remain_point { get; set; }
    private void Start()
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
        if (remain_point)
        {
            if (pre_skill)
            {
                if (!selected_btn.GetComponent<SkillButtonListener>().ExStatusAdd())
                {
                    explain.SetActive(true);
                    explain.GetComponent<Text>().text = "최대 레벨입니다.";
                }
                else
                {
                    explain.SetActive(true);
                    explain.GetComponent<Text>().text = "연구 완료.";
                }
            }
            else
            {
                explain.SetActive(true);
                explain.GetComponent<Text>().text = "선행 스킬을 찍어주세요.";
            }
        }
        else
        {
            explain.SetActive(true);
            explain.GetComponent<Text>().text = "포인트가 부족합니다.";
        }
        yield return new WaitForSeconds(1f);
        explain.SetActive(false);
    }
    public void Cancle_click()
    {
        gameObject.SetActive(false);
    }
}
