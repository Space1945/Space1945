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
    public Text clear_or_not_text; // 클리어했으면 응원 문구, 못했으면 꼴받게하는 문구 출력

    // Start is called before the first frame update
    void Start()
    {
        score_text.fontSize = 60;
        exp_text.fontSize = 60;
        gold_text.fontSize = 60;
        clear_or_not_text.fontSize = 60;

        score_text.text = "총 획득한 스코어: " + DB_Manager.Instance.score_earned;
        exp_text.text = "총 획득한 경험치: " + DB_Manager.Instance.exp_earned;
        gold_text.text = "총 획득한 골드: " + DB_Manager.Instance.gold_earned;
        clear_or_not_text.text = DB_Manager.Instance.stage_clear ? "참 잘하셨습니다!" : "ㅋㅋㅋㅋㅋㅋㅋㅋㅋ";

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

        //DB_Manager.Instance.score += DB_Manager.Instance.score_earned; 따로 DB에 최고 스코어 점수 만들어 줘야함 >> 저장 안해도 상관 X
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
