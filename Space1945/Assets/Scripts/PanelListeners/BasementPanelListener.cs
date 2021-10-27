using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasementPanelListener : MonoBehaviour
{
    public GameObject repairshop_panel;
    public GameObject laboratory_panel;

    public Button repairshop_button;
    public Button laboratory_button;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);
    }
    void Start()
    {
        repairshop_panel.SetActive(true);
        laboratory_panel.SetActive(false);
    }

    public void ActivateRepairshopPanel()
    {
        repairshop_panel.SetActive(true);
        laboratory_panel.SetActive(false);
    }
    public void ActivateLaboratoryPanel()
    {
        repairshop_panel.SetActive(false);
        laboratory_panel.SetActive(true);
    }
}
