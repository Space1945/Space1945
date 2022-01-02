using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButtonListener : MonoBehaviour
{
    // Start is called before the first frame update
    public int difficulty { get; set; }
    public int layer { get; set; }

    public void LoadStage()
    {
        SceneManager.LoadScene("Ingame");
    }
}