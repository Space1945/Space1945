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

    void Start()
    {
        AddListenerOnButtons();

        // 초기 화면
        main_panel.SetActive(true);
        basement_panel.SetActive(false);
        store_panel.SetActive(false);
        configuration_panel.SetActive(false);

        state_panel.SetActive(true); // 항상 보임
        button_panel.SetActive(true); // 항상 보임
    }

    public void AddListenerOnButtons()
    {
        to_main.onClick.AddListener(ActivateMain);
        to_basement.onClick.AddListener(ActivateBasement);
        to_store.onClick.AddListener(ActivateStore);
        to_configuration.onClick.AddListener(ActivateConfiguration);
    }

    public void ActivateMain()
    {
        main_panel.SetActive(true);
        basement_panel.SetActive(false);
        store_panel.SetActive(false);
        configuration_panel.SetActive(false);
    }
    public void ActivateBasement()
    {
        main_panel.SetActive(false);
        basement_panel.SetActive(true);
        store_panel.SetActive(false);
        configuration_panel.SetActive(false);
    }
    public void ActivateStore()
    {
        main_panel.SetActive(false);
        basement_panel.SetActive(false);
        store_panel.SetActive(true);
        configuration_panel.SetActive(false);
    }
    public void ActivateConfiguration()
    {
        main_panel.SetActive(false);
        basement_panel.SetActive(false);
        store_panel.SetActive(false);
        configuration_panel.SetActive(true);
    }
}
