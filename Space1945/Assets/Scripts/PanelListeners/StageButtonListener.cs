using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButtonListener : MonoBehaviour
{
    // Start is called before the first frame update
    public int stage_number;

    public void LoadStage()
    {
        DB_Manager.Instance.selected_stage = stage_number;
        SceneManager.LoadScene("Ingame");
    }
}