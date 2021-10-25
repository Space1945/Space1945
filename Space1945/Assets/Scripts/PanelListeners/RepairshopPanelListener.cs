using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairshopPanelListener : MonoBehaviour
{
    public Button airframe_button;
    public Button atk_button;
    public Button def_button;
    public Button sub1_button;
    public Button sub2_button;

    public Sprite none;

    public GameObject items_panel;

    void Awake()
    {
        UpdateAllPartsButton();

        DB_Manager.Instance.parts_status_init();

        items_panel.SetActive(false);
    }

    public void AirframeButtonClicked()
    {
        items_panel.SetActive(true);
        items_panel.GetComponent<ItemsPanelListener>().LoadItems(0);
    }
    public void AtkButtonClicked()
    {
        items_panel.SetActive(true);
        items_panel.GetComponent<ItemsPanelListener>().LoadItems(1);
    }
    public void DefButtonClicked()
    {
        items_panel.SetActive(true);
        items_panel.GetComponent<ItemsPanelListener>().LoadItems(2);
    }
    public void Sub1ButtonClicked()
    {
        items_panel.SetActive(true);
        items_panel.GetComponent<ItemsPanelListener>().LoadItems(3);
    }
    public void Sub2ButtonClicked()
    {
        items_panel.SetActive(true);
        items_panel.GetComponent<ItemsPanelListener>().LoadItems(4);
    }

    public void UpdatePartsButton(int idx)
    {
        switch (idx)
        {
            case 0:
                if (DB_Manager.Instance.using_airframe != null)
                    airframe_button.transform.Find("AirframeImage").GetComponent<Image>().sprite = DB_Manager.Instance.using_airframe.GetComponent<SpriteRenderer>().sprite;
                else
                    airframe_button.transform.Find("AirframeImage").GetComponent<Image>().sprite = none;
                break;
            case 1:
                if (DB_Manager.Instance.using_atk != null)
                    atk_button.transform.Find("AtkImage").GetComponent<Image>().sprite = DB_Manager.Instance.using_atk.GetComponent<SpriteRenderer>().sprite;
                else
                    atk_button.transform.Find("AtkImage").GetComponent<Image>().sprite = none;
                break;
            case 2:
                if (DB_Manager.Instance.using_def != null)
                    def_button.transform.Find("DefImage").GetComponent<Image>().sprite = DB_Manager.Instance.using_def.GetComponent<SpriteRenderer>().sprite;
                else
                    def_button.transform.Find("DefImage").GetComponent<Image>().sprite = none;
                break;
            case 3:
                if (DB_Manager.Instance.using_sub_left != null)
                    sub1_button.transform.Find("Sub1Image").GetComponent<Image>().sprite = DB_Manager.Instance.using_sub_left.GetComponent<SpriteRenderer>().sprite;
                else
                    sub1_button.transform.Find("Sub1Image").GetComponent<Image>().sprite = none;
                break;
            case 4:
                if (DB_Manager.Instance.using_sub_right != null)
                    sub2_button.transform.Find("Sub2Image").GetComponent<Image>().sprite = DB_Manager.Instance.using_sub_right.GetComponent<SpriteRenderer>().sprite;
                else
                    sub2_button.transform.Find("Sub2Image").GetComponent<Image>().sprite = none;
                break;
        }
    }
    public void UpdateAllPartsButton()
    {
        if (DB_Manager.Instance.using_airframe != null)
            airframe_button.transform.Find("AirframeImage").GetComponent<Image>().sprite = DB_Manager.Instance.using_airframe.GetComponent<SpriteRenderer>().sprite;
        else
            airframe_button.transform.Find("AirframeImage").GetComponent<Image>().sprite = none;

        if (DB_Manager.Instance.using_atk != null)
            atk_button.transform.Find("AtkImage").GetComponent<Image>().sprite = DB_Manager.Instance.using_atk.GetComponent<SpriteRenderer>().sprite;
        else
            atk_button.transform.Find("AtkImage").GetComponent<Image>().sprite = none;

        if (DB_Manager.Instance.using_def != null)
            def_button.transform.Find("DefImage").GetComponent<Image>().sprite = DB_Manager.Instance.using_def.GetComponent<SpriteRenderer>().sprite;
        else
            def_button.transform.Find("DefImage").GetComponent<Image>().sprite = none;

        if (DB_Manager.Instance.using_sub_left != null)
            sub1_button.transform.Find("Sub1Image").GetComponent<Image>().sprite = DB_Manager.Instance.using_sub_left.GetComponent<SpriteRenderer>().sprite;
        else
            sub1_button.transform.Find("Sub1Image").GetComponent<Image>().sprite = none;

        if (DB_Manager.Instance.using_sub_right != null)
            sub2_button.transform.Find("Sub2Image").GetComponent<Image>().sprite = DB_Manager.Instance.using_sub_right.GetComponent<SpriteRenderer>().sprite;
        else
            sub2_button.transform.Find("Sub2Image").GetComponent<Image>().sprite = none;
    }
}
