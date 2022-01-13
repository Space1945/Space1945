using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterCanvasListener : MonoBehaviour
{
    public GameObject state_panel;
    public GameObject main_panel;
    public GameObject repairshop_panel;
    public GameObject store_panel;
    public GameObject upgrade_panel;

    public Button repairshop_button;

    private void Start()
    {
        upgrade_panel.SetActive(false);
    }
    public void Active_Repairshop()
    {
        repairshop_panel.SetActive(true);
    }
}
