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
    public Text best_score_text;
    public Text exp_text;
    public Text gold_text;
    public Text clear_or_not_text; // Ŭ���������� ���� ����, �������� �ùް��ϴ� ���� ���

    // Start is called before the first frame update
    void Start()
    {
        score_text.fontSize = 60;
        best_score_text.fontSize = 60;
        exp_text.fontSize = 60;
        gold_text.fontSize = 60;
        clear_or_not_text.fontSize = 60;

        string stage_score = "Stage" + DB_Manager.Instance.selected_chapter + "_" + DB_Manager.Instance.selected_stage + "Score";
        int score = PlayerPrefs.GetInt(stage_score);
        if (score < DB_Manager.Instance.score_earned)
            PlayerPrefs.SetInt(stage_score, DB_Manager.Instance.score_earned);

        int gold_earned = DB_Manager.Instance.gold_earned;
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + gold_earned);

        int max_exp = PlayerPrefs.GetInt("max_exp");
        int max_level = PlayerPrefs.GetInt("max_level");
        int level = PlayerPrefs.GetInt("pilot_level");
        int exp_earned = DB_Manager.Instance.exp_earned;
        if (level < max_level)
        {
            int exp = PlayerPrefs.GetInt("pilot_exp") + exp_earned;
            bool level_up = false;
            while (exp >= max_exp)
            {
                exp -= max_exp;
                max_exp = (int)(max_exp * 1.2f);
                level++;
                level_up = true;
            }

            if (level_up)
            {
                PlayerPrefs.SetInt("pilot_exp", exp);
                PlayerPrefs.SetInt("max_exp", max_exp);
                PlayerPrefs.SetInt("pilot_level", level);
            }
            else
                PlayerPrefs.SetInt("pilot_exp", exp + DB_Manager.Instance.exp_earned);

            if (level >= max_level)
                PlayerPrefs.SetInt("pilot_exp", max_exp);
        }

        score_text.text = "�� ȹ���� ���ھ�: " + DB_Manager.Instance.score_earned;
        best_score_text.text = "�ְ� ����: " + PlayerPrefs.GetInt(stage_score);
        exp_text.text = "�� ȹ���� ����ġ: " + exp_earned;
        gold_text.text = "�� ȹ���� ���: " + gold_earned;
        clear_or_not_text.text = DB_Manager.Instance.stage_clear ? "�� ���ϼ̽��ϴ�!" : "������������������";
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
