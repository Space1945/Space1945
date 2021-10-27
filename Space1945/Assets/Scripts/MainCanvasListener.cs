using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasListener : MonoBehaviour
{
    public GameObject main_panel;
    public GameObject basement_panel;
    public GameObject store_panel;
    public GameObject configuration_panel;
    public GameObject state_panel;
    public GameObject button_panel;

    public Button to_main;
    public Button to_basement;
    public Button to_store;
    public Button to_configuration;

    GameObject opening_panel;

    void Awake()
    {
        // �ʱ� ȭ��
        opening_panel = main_panel;
    }

    public void ActivateMain()
    {
        opening_panel.SetActive(false);
        main_panel.SetActive(true);
        opening_panel = main_panel;
    }
    public void ActivateBasement()
    {
        opening_panel.SetActive(false);
        basement_panel.SetActive(true);
        opening_panel = basement_panel;
    }
    public void ActivateStore()
    {
        opening_panel.SetActive(false);
        store_panel.SetActive(true);
        opening_panel = store_panel;
    }
    public void ActivateConfiguration()
    {
        opening_panel.SetActive(false);
        configuration_panel.SetActive(true);
        opening_panel = configuration_panel;
    }
}
