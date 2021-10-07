using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningListener : MonoBehaviour
{
    public Button main_button;

    public void ClickedAnywhere()
    {
        if (!PlayerPrefs.HasKey("pilot_name"))
        {
            PlayerPrefs.SetString("pilot_name", "user");
            Initiate();
        }
        LoadScene();
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Initiate()
    {
        PlayerPrefs.SetInt("pilot_level", 1);
        PlayerPrefs.SetInt("pilot_exp", 0);
        PlayerPrefs.SetString("pilot_skill", "100000000000000000000000000000000000000000000"); // ±Ê¿Ã 45
        PlayerPrefs.SetInt("gold", 10000);
        PlayerPrefs.SetInt("max_level", 30);
        PlayerPrefs.SetInt("max_exp", 200);

        PlayerPrefs.SetString("chapter1", "10000");
        PlayerPrefs.SetString("chapter2", "00000");
        PlayerPrefs.SetString("bgm", "true");
        PlayerPrefs.SetString("es", "true");
        PlayerPrefs.SetString("refresh", "20200101120000");

        PlayerPrefs.SetString("Stage1_1Type", "Robot");
        PlayerPrefs.SetString("Stage1_1Normals", "Normal1 Normal2");
        PlayerPrefs.SetString("Stage1_1NormalsTime", "100 100 100 100 100");
        PlayerPrefs.SetString("Stage1_1Elites", "Elite1");
        PlayerPrefs.SetInt("Stage1_1ElitesEmerCnt", 10);
        PlayerPrefs.SetInt("Stage1_1Score", 0);
        PlayerPrefs.SetString("Stage1_5Boss", "Boss1");

        PlayerPrefs.SetString("Atk1", "using");
        PlayerPrefs.SetString("Ship1", "using");
    }
}
