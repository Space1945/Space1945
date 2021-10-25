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
                    explain.GetComponent<Text>().text = "�ִ� �����Դϴ�.";
                }
                else
                {
                    explain.SetActive(true);
                    explain.GetComponent<Text>().text = "���� �Ϸ�.";
                }
            }
            else
            {
                explain.SetActive(true);
                explain.GetComponent<Text>().text = "���� ��ų�� ����ּ���.";
            }
        }
        else
        {
            explain.SetActive(true);
            explain.GetComponent<Text>().text = "����Ʈ�� �����մϴ�.";
        }
        yield return new WaitForSeconds(1f);
        explain.SetActive(false);
    }
    public void Cancle_click()
    {
        gameObject.SetActive(false);
    }
}
