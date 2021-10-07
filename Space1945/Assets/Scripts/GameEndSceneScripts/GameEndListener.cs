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
    public Text clear_or_not_text; // 클리어했으면 응원 문구, 못했으면 꼴받게하는 문구 출력

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
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + DB_Manager.Instance.gold_earned);

        int max_exp = PlayerPrefs.GetInt("max_exp");
        int level = PlayerPrefs.GetInt("pilot_level");
        if (level < PlayerPrefs.GetInt("max_level"))
        {
            int exp = PlayerPrefs.GetInt("pilot_exp");
            bool level_up = false;
            while (exp + DB_Manager.Instance.exp_earned >= max_exp)
            {
                exp = exp + DB_Manager.Instance.exp_earned - max_exp;
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
        }
        else
            PlayerPrefs.SetInt("pilot_exp", max_exp);

        score_text.text = "총 획득한 스코어: " + DB_Manager.Instance.score_earned;
        best_score_text.text = "최고 점수: " + PlayerPrefs.GetInt(stage_score);
        exp_text.text = "총 획득한 경험치: " + DB_Manager.Instance.exp_earned;
        gold_text.text = "총 획득한 골드: " + DB_Manager.Instance.gold_earned;
        clear_or_not_text.text = DB_Manager.Instance.stage_clear ? "참 잘하셨습니다!" : "ㅋㅋㅋㅋㅋㅋㅋㅋㅋ";
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadCurrentStage() // Replay 버튼 클릭 시
    {
        SceneManager.LoadScene("Ingame");
    }
}
