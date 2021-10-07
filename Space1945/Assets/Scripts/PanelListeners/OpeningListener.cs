using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningListener : MonoBehaviour
{
    public Button main_button;
    public GameObject name_panel;

    public void ClickedAnywhere()
    {
        if (!PlayerPrefs.HasKey("pilot_name"))
        {
            name_panel.SetActive(true);
        }
        else
            LoadScene();
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("Menu");
    }
}