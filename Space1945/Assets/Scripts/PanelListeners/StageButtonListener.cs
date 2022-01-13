using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButtonListener : MonoBehaviour
{
    public GameObject main_canvas;

    public int difficulty { get; set; }
    public int layer { get; set; }
    public string type{ get; set; }

    public void LoadStage()
    {
        SceneManager.LoadScene("Ingame");
    }
}