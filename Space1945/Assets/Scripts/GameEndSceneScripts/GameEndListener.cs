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
    public Text clear_or_not_text; // 適軒嬢梅生檎 誓据 庚姥, 公梅生檎 加閤惟馬澗 庚姥 窒径

    // Start is called before the first frame update
    void Start()
    {
        score_text.fontSize = 60;
        exp_text.fontSize = 60;
        gold_text.fontSize = 60;
        clear_or_not_text.fontSize = 60;

        score_text.text = "恥 塙究廃 什坪嬢: " + DB_Manager.Instance.score_earned;
        exp_text.text = "恥 塙究廃 井蝿帖: " + DB_Manager.Instance.exp_earned;
        gold_text.text = "恥 塙究廃 茨球: " + DB_Manager.Instance.gold_earned;
        clear_or_not_text.text = DB_Manager.Instance.stage_clear ? "凧 設馬写柔艦陥!" : "せせせせせせせせせ";

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

        //DB_Manager.Instance.score += DB_Manager.Instance.score_earned; 魚稽 DB拭 置壱 什坪嬢 繊呪 幻級嬢 操醤敗 >> 煽舌 照背亀 雌淫 X
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadCurrentStage() // Replay 獄動 適遣 獣
    {
        SceneManager.LoadScene("Ingame");
    }
}
