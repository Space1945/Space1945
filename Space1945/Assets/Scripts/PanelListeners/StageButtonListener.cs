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
        DB_Manager.Instance.selected_stage = this.gameObject;
        if (type == "store")
            main_canvas.GetComponent<ChapterCanvasListener>().store_panel.SetActive(true);
        else if (type == "upgrade")
            main_canvas.GetComponent<ChapterCanvasListener>().upgrade_panel.SetActive(true);
        else
            SceneManager.LoadScene("Ingame");
    }
}