using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectPanelListener : MonoBehaviour
{
    public Button cancel_button;

    public GameObject item_button_frame;
    public GameObject Main_Panel;

    List<GameObject> items;
    int btn_idx;

    void Awake()
    {
        int btn_cnt = DB_Manager.Instance.total_prefab_cnt;
        float parent_width = gameObject.GetComponent<RectTransform>().rect.width;
        float parent_height = gameObject.GetComponent<RectTransform>().rect.height;
        float btn_frame_size = item_button_frame.GetComponent<RectTransform>().rect.width;

        items = new List<GameObject>();

        for (int i = 0; i < btn_cnt; i++)
        {
            GameObject item_btn = Instantiate(item_button_frame);
            item_btn.transform.SetParent(gameObject.transform, false);
            item_btn.transform.localPosition = new Vector2(-parent_width / 2 + 116 + (i % 4) * (btn_frame_size + 16), parent_height / 2 - 116 - (i / 4) * (btn_frame_size + 16));

            item_btn.GetComponent<Button>().enabled = false;
            items.Add(item_btn);
        }

        Destroy(item_button_frame);
    }

    public void LoadItems(int btn_number)
    {
        btn_idx = btn_number;
        int unlocked_cnt = 0;
        int locked_cnt = 0;

        items[0].GetComponent<Button>().enabled = true;
        items[0].GetComponent<ItemButtonsObject>().obj = DB_Manager.Instance.using_airframe;
        items[0].GetComponent<ItemButtonsObject>().idx = 0;
        items[0].transform.Find("ItemImage").GetComponent<Image>().sprite = items[0].GetComponent<ItemButtonsObject>().obj.GetComponent<SpriteRenderer>().sprite;
        unlocked_cnt = DB_Manager.Instance.unlocked_airframes.Count;
        for (int i = 1; i <= unlocked_cnt; i++)
        {
            items[i].GetComponent<Button>().enabled = true;
            items[i].GetComponent<ItemButtonsObject>().obj = DB_Manager.Instance.unlocked_airframes[i - 1];
            items[i].GetComponent<ItemButtonsObject>().idx = i;
            items[i].transform.Find("ItemImage").GetComponent<Image>().sprite = items[i].GetComponent<ItemButtonsObject>().obj.GetComponent<SpriteRenderer>().sprite;
        }
        locked_cnt = DB_Manager.Instance.locked_airframes.Count;
        for (int i = unlocked_cnt + 1; i <= locked_cnt; i++)
        {
            items[i].GetComponent<Button>().enabled = true;
            items[i].GetComponent<ItemButtonsObject>().obj = DB_Manager.Instance.locked_airframes[i - unlocked_cnt - 1];
            items[i].GetComponent<ItemButtonsObject>().idx = i;
            items[i].transform.Find("ItemImage").GetComponent<Image>().sprite = items[i].GetComponent<ItemButtonsObject>().obj.GetComponent<SpriteRenderer>().sprite;
        }
    }
    public void CancelButtonClicked()
    {
        DeleteAllItemsImage();
        foreach (Transform child in Main_Panel.transform)
            child.GetComponent<Button>().interactable = true;
        gameObject.SetActive(false);
    }
    public void EquipButtonClicked()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject clicked_obj = null;

        clicked_obj = button.GetComponent<ItemButtonsObject>().obj;
        DB_Manager.Instance.unlocked_airframes.Add(DB_Manager.Instance.using_airframe);
        PlayerPrefs.SetString(DB_Manager.Instance.using_airframe.name, "unlocked");
        DB_Manager.Instance.using_airframe = clicked_obj;
        DB_Manager.Instance.unlocked_airframes.Remove(clicked_obj);
        PlayerPrefs.SetString(clicked_obj.name, "using");
        transform.parent.GetComponent<RepairshopPanelListener>().UpdatePartsButton(0);
        DeleteAllItemsImage();
        gameObject.SetActive(false);
    }

    void DeleteAllItemsImage()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.Find("ItemImage").GetComponent<Image>().sprite = null;
            items[i].GetComponent<Button>().enabled = false;
        }
    }
}
