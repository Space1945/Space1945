using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemsPanelListener : MonoBehaviour
{
    public Button cancel_button;
    public Button equip_button;
    public Button unequip_button;

    public GameObject item_button_frame;

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

        switch (btn_number)
        {
            case 0:
                unlocked_cnt = DB_Manager.Instance.unlocked_airframes.Count;
                for (int i = 0; i < unlocked_cnt; i++)
                {
                    items[i].GetComponent<Button>().enabled = true;
                    items[i].GetComponent<ItemButtonsObject>().obj = DB_Manager.Instance.unlocked_airframes[i];
                    items[i].GetComponent<ItemButtonsObject>().idx = i;
                    items[i].transform.Find("ItemImage").GetComponent<Image>().sprite = items[i].GetComponent<ItemButtonsObject>().obj.GetComponent<SpriteRenderer>().sprite;
                }
                break;
            case 1:
                unlocked_cnt = DB_Manager.Instance.unlocked_atks.Count;
                for (int i = 0; i < unlocked_cnt; i++)
                {
                    items[i].GetComponent<Button>().enabled = true;
                    items[i].GetComponent<ItemButtonsObject>().obj = DB_Manager.Instance.unlocked_atks[i];
                    items[i].GetComponent<ItemButtonsObject>().idx = i;
                    items[i].transform.Find("ItemImage").GetComponent<Image>().sprite = items[i].GetComponent<ItemButtonsObject>().obj.GetComponent<SpriteRenderer>().sprite;
                }
                break;
            case 2:
                unlocked_cnt = DB_Manager.Instance.unlocked_defs.Count;
                for (int i = 0; i < unlocked_cnt; i++)
                {
                    items[i].GetComponent<Button>().enabled = true;
                    items[i].GetComponent<ItemButtonsObject>().obj = DB_Manager.Instance.unlocked_defs[i];
                    items[i].GetComponent<ItemButtonsObject>().idx = i;
                    items[i].transform.Find("ItemImage").GetComponent<Image>().sprite = items[i].GetComponent<ItemButtonsObject>().obj.GetComponent<SpriteRenderer>().sprite;
                }
                break;
            case 3:
            case 4:
                unlocked_cnt = DB_Manager.Instance.unlocked_subs.Count;
                for (int i = 0; i < unlocked_cnt; i++)
                {
                    items[i].GetComponent<Button>().enabled = true;
                    items[i].GetComponent<ItemButtonsObject>().obj = DB_Manager.Instance.unlocked_subs[i];
                    items[i].GetComponent<ItemButtonsObject>().idx = i;
                    items[i].transform.Find("ItemImage").GetComponent<Image>().sprite = items[i].GetComponent<ItemButtonsObject>().obj.GetComponent<SpriteRenderer>().sprite;
                }
                break;
        }
    }
    public void CancelButtonClicked()
    {
        DeleteAllItemsImage();
        gameObject.SetActive(false);
    }
    public void EquipButtonClicked()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject clicked_obj = null;

        switch (btn_idx)
        {
            case 0: // 기체
                clicked_obj = button.GetComponent<ItemButtonsObject>().obj;
                DB_Manager.Instance.unlocked_airframes.Add(DB_Manager.Instance.using_airframe);
                PlayerPrefs.SetString(DB_Manager.Instance.using_airframe.name, "unlocked");
                DB_Manager.Instance.using_airframe = clicked_obj;
                DB_Manager.Instance.unlocked_airframes.Remove(clicked_obj);
                PlayerPrefs.SetString(clicked_obj.name, "using");
                transform.parent.GetComponent<RepairshopPanelListener>().UpdatePartsButton(0);
                DeleteAllItemsImage();
                gameObject.SetActive(false);
                break;
            case 1: // 공격
                clicked_obj = button.GetComponent<ItemButtonsObject>().obj;
                DB_Manager.Instance.unlocked_atks.Add(DB_Manager.Instance.using_atk);
                PlayerPrefs.SetString(DB_Manager.Instance.using_atk.name, "unlocked");
                DB_Manager.Instance.using_atk = clicked_obj;
                DB_Manager.Instance.unlocked_atks.Remove(clicked_obj);
                PlayerPrefs.SetString(clicked_obj.name, "using");
                transform.parent.GetComponent<RepairshopPanelListener>().UpdatePartsButton(1);
                DeleteAllItemsImage();
                gameObject.SetActive(false);
                break;
            case 2: // 방어
                clicked_obj = button.GetComponent<ItemButtonsObject>().obj;
                if (DB_Manager.Instance.using_def != null)
                {
                    DB_Manager.Instance.unlocked_defs.Add(DB_Manager.Instance.using_def);
                    PlayerPrefs.SetString(DB_Manager.Instance.using_def.name, "unlocked");
                }
                DB_Manager.Instance.UpdateDefExStats(DB_Manager.Instance.using_def, clicked_obj); // 증가량 적용해준뒤 템 바꿈
                DB_Manager.Instance.using_def = clicked_obj;
                DB_Manager.Instance.unlocked_defs.Remove(clicked_obj);
                PlayerPrefs.SetString(clicked_obj.name, "using");
                transform.parent.GetComponent<RepairshopPanelListener>().UpdatePartsButton(2);
                DeleteAllItemsImage();
                gameObject.SetActive(false);
                break;
            case 3: // 서브 왼쪽
                clicked_obj = button.GetComponent<ItemButtonsObject>().obj;
                if (DB_Manager.Instance.using_sub_left != null)
                {
                    DB_Manager.Instance.unlocked_subs.Add(DB_Manager.Instance.using_sub_left);
                    PlayerPrefs.SetString(DB_Manager.Instance.using_sub_left.name, "unlocked");
                }
                DB_Manager.Instance.UpdateSubExStats(DB_Manager.Instance.using_sub_left, clicked_obj); // 증가량 적용해준뒤 템 바꿈
                DB_Manager.Instance.using_sub_left = clicked_obj;
                DB_Manager.Instance.unlocked_subs.Remove(clicked_obj);
                PlayerPrefs.SetString(clicked_obj.name, "using_left");
                transform.parent.GetComponent<RepairshopPanelListener>().UpdatePartsButton(3);
                DeleteAllItemsImage();
                gameObject.SetActive(false);
                break;
            case 4: // 서브 오른쪽
                clicked_obj = button.GetComponent<ItemButtonsObject>().obj;
                if (DB_Manager.Instance.using_sub_right != null)
                {
                    DB_Manager.Instance.unlocked_subs.Add(DB_Manager.Instance.using_sub_right);
                    PlayerPrefs.SetString(DB_Manager.Instance.using_sub_right.name, "unlocked");
                }
                DB_Manager.Instance.UpdateSubExStats(DB_Manager.Instance.using_sub_right, clicked_obj); // 증가량 적용해준뒤 템 바꿈
                DB_Manager.Instance.using_sub_right = clicked_obj;
                DB_Manager.Instance.unlocked_subs.Remove(clicked_obj);
                PlayerPrefs.SetString(clicked_obj.name, "using_right");
                transform.parent.GetComponent<RepairshopPanelListener>().UpdatePartsButton(4);
                DeleteAllItemsImage();
                gameObject.SetActive(false);
                break;
        }
    }
    public void UnequipButtonClicked()
    {
        switch (btn_idx)
        {
            case 0:
                // 비행기는 해제 불가
                break;
            case 1:
                // 무기도 해제 불가
                break;
            case 2:
                if (DB_Manager.Instance.using_def == null)
                    break;
                DB_Manager.Instance.UpdateDefExStats(DB_Manager.Instance.using_def, null);
                DB_Manager.Instance.unlocked_defs.Add(DB_Manager.Instance.using_def);
                DB_Manager.Instance.using_def = null;
                LoadItems(2);
                transform.parent.GetComponent<RepairshopPanelListener>().UpdatePartsButton(2);
                break;
            case 3:
                if (DB_Manager.Instance.using_sub_left == null)
                    break;
                DB_Manager.Instance.UpdateSubExStats(DB_Manager.Instance.using_sub_left, null);
                DB_Manager.Instance.unlocked_subs.Add(DB_Manager.Instance.using_sub_left);
                DB_Manager.Instance.using_sub_left = null;
                LoadItems(3);
                transform.parent.GetComponent<RepairshopPanelListener>().UpdatePartsButton(3);
                break;
            case 4:
                if (DB_Manager.Instance.using_sub_right == null)
                    break;
                DB_Manager.Instance.UpdateSubExStats(DB_Manager.Instance.using_sub_right, null);
                DB_Manager.Instance.unlocked_subs.Add(DB_Manager.Instance.using_sub_right);
                DB_Manager.Instance.using_sub_right = null;
                LoadItems(4);
                transform.parent.GetComponent<RepairshopPanelListener>().UpdatePartsButton(4);
                break;
        }
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
