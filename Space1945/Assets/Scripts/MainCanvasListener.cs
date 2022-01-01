using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasListener : MonoBehaviour
{
    public GameObject main_panel;
    public GameObject select_panel;
    public GameObject configuration_panel;
    public GameObject state_panel;

    public void ActivateSelect()
    {
        foreach (Transform child in main_panel.transform)
            child.GetComponent<Button>().interactable = false;
        select_panel.SetActive(true);
        select_panel.GetComponent<SelectPanelListener>().LoadItems();
    }
    public void ActivateConfiguration()
    {
        foreach (Transform child in main_panel.transform)
            child.GetComponent<Button>().interactable = false;
        configuration_panel.SetActive(true);
    }
}
