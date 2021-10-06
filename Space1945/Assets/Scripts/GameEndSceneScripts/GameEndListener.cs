using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndListener : MonoBehaviour
{
    public Button back_button;
    public Button replay_button;

    public Text score_text;
    public Text exp_text;
    public Text gold_text;
    public Text clear_or_not_text; // Ŭ���������� ���� ����, �������� �ùް��ϴ� ���� ���

    // Start is called before the first frame update
    void Start()
    {
        score_text.fontSize = 60;
        exp_text.fontSize = 60;
        gold_text.fontSize = 60;
        clear_or_not_text.fontSize = 60;

        score_text.text = "�� ȹ���� ���ھ�: " + DB_Manager.Instance.score_earned;
        exp_text.text = "�� ȹ���� ����ġ: " + DB_Manager.Instance.exp_earned;
        gold_text.text = "�� ȹ���� ���: " + DB_Manager.Instance.gold_earned;
        clear_or_not_text.text = DB_Manager.Instance.stage_clear ? "�� ���ϼ̽��ϴ�!" : "������������������";

        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + DB_Manager.Instance.gold_earned);
        int exp = PlayerPrefs.GetInt("pilot_exp");
        int max_exp = PlayerPrefs.GetInt("max_exp");
        int level = PlayerPrefs.GetInt("pilot_level");
        while (exp + DB_Manager.Instance.exp_earned >= PlayerPrefs.GetInt("max_exp"))
        {
            exp = exp + DB_Manager.Instance.exp_earned - max_exp;
            max_exp = (int)(max_exp * 1.2f);
            level++;
        }
        PlayerPrefs.SetInt("pilot_exp", exp);
        PlayerPrefs.SetInt("max_exp", max_exp);
        PlayerPrefs.SetInt("pilot_level", level);

        //DB_Manager.Instance.score += DB_Manager.Instance.score_earned; ���� DB�� �ְ� ���ھ� ���� ����� ����� >> ���� ���ص� ��� X
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadCurrentStage() // Replay ��ư Ŭ�� ��
    {
        SceneManager.LoadScene("Ingame");
    }
}
