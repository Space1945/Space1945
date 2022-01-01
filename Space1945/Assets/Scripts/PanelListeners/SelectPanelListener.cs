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
        gameObject.SetActive(false);
    }
    public void LoadItems()
    {
        int unlocked_cnt = DB_Manager.Instance.unlocked_airframes.Count;
        int locked_cnt = DB_Manager.Instance.locked_airframes.Count;

        items[0].GetComponent<Button>().enabled = true;
        items[0].GetComponent<ItemButtonsObject>().obj = DB_Manager.Instance.using_airframe;
        items[0].GetComponent<ItemButtonsObject>().idx = 0;
        items[0].transform.Find("ItemImage").GetComponent<Image>().sprite = items[0].GetComponent<ItemButtonsObject>().obj.GetComponent<SpriteRenderer>().sprite;

        for (int i = 1; i <= unlocked_cnt; i++)
        {
            items[i].GetComponent<Button>().enabled = true;
            items[i].GetComponent<ItemButtonsObject>().obj = DB_Manager.Instance.unlocked_airframes[i - 1];
            items[i].GetComponent<ItemButtonsObject>().idx = i;
            items[i].transform.Find("ItemImage").GetComponent<Image>().sprite = items[i].GetComponent<ItemButtonsObject>().obj.GetComponent<SpriteRenderer>().sprite;
        }

        for (int i = unlocked_cnt + 1; i <= unlocked_cnt + locked_cnt; i++)
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
    public void AirframeClicked()
    {
        Debug.Log("µø¿€");
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject clicked_obj = button.GetComponent<ItemButtonsObject>().obj;

        if (PlayerPrefs.GetString(clicked_obj.name) == "locked")
        {
            Main_Panel.GetComponent<MainPanelListener>().Update_Airframe(clicked_obj, false);
        }
        else
        {
            DB_Manager.Instance.unlocked_airframes.Add(DB_Manager.Instance.using_airframe);
            PlayerPrefs.SetString(DB_Manager.Instance.using_airframe.name, "unlocked");
            DB_Manager.Instance.using_airframe = clicked_obj;
            DB_Manager.Instance.unlocked_airframes.Remove(clicked_obj);
            PlayerPrefs.SetString(clicked_obj.name, "using");
            Main_Panel.GetComponent<MainPanelListener>().Update_Airframe(clicked_obj, true);
        }

        DeleteAllItemsImage();
        foreach (Transform child in Main_Panel.transform)
            child.GetComponent<Button>().interactable = true;
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
