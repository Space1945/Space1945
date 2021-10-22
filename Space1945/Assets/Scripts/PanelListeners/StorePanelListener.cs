using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StorePanelListener : MonoBehaviour
{
    AudioSource audio_source;
    public AudioClip button_clicked1;
    public AudioClip button_clicked2;

    public Image[] face;

    public Button[] item_button;
    private GameObject[] item_objects; // 버튼에 올라간 gameobject

    public Button refresh_button;
    public Text remaining_time_text;

    public Text[] item_cost_text;
    public Text refresh_cost_text;

    public GameObject item_info_panel;

    public Sprite none;

    // Start is called before the first frame update
    void Start()
    {
        face_disable();
        face[0].enabled = true;
        audio_source = GetComponent<AudioSource>();
        audio_source.playOnAwake = false; // 설정해주어야 클릭시에만 소리남

        remaining_time_text.fontSize = 50;
        remaining_time_text.color = new Color(255, 255, 255);

        item_objects = new GameObject[6];

        UpdateAllItems();

        item_info_panel.SetActive(false);
    }
    private void face_disable()
    {
        face[0].enabled = false;
        face[1].enabled = false;
        face[2].enabled = false;
    }
    public void PlayEffectSound1()
    {
        audio_source.clip = button_clicked1;
        if (PlayerPrefs.GetString("es") == "true")
            audio_source.Play();
    }
    public void PlayEffectSound2()
    {
        audio_source.clip = button_clicked2;
        if (PlayerPrefs.GetString("es") == "true")
            audio_source.Play();
    }

    // refresh 버튼을 눌렀을 때 작동
    public void RefreshClicked()
    {
        int gold = PlayerPrefs.GetInt("gold");
        int refresh_gold = int.Parse(refresh_cost_text.text);

        if (gold - refresh_gold >= 0)
        {
            PlayerPrefs.SetInt("gold", gold - refresh_gold);

            // 템 리스트 변경
            UpdateAllItems();
        }
    }
    public void GotchaClicked()
    {
        int gold = PlayerPrefs.GetInt("gold");
        int gotcha_gold = int.Parse(item_cost_text[5].text);

        if (gold - gotcha_gold >= 0) // 10000 골드 소비
        {
            int rand = UnityEngine.Random.Range(0, 10001); // 0 ~ 10000

            if (rand > 9990)
            {
                gold += 1000000;
                face_disable();
                face[1].enabled = true;
            }
            else if (rand > 9900)
            {
                gold += 100000;
                face_disable();
                face[0].enabled = true;
            }
            else if (rand > 9700)
            {
                gold += 50000;
                face_disable();
                face[0].enabled = true;
            }
            else if (rand > 7500)
            {
                gold += 10000;
                face_disable();
                face[1].enabled = true;
            }
            else if (rand > 5000)
            {
                gold += 5000;
                face_disable();
                face[0].enabled = true;
            }
            else if (rand > 2500)
            {
                gold += 2500;
                face_disable();
                face[1].enabled = true;
            }
            else
            {
                face_disable();
                face[2].enabled = true;
            }
        }

        PlayerPrefs.SetInt("gold", gold - gotcha_gold);
    }

    public void UpdateAllItems()
    {
        UpdateAirframeItems();
        UpdateAtkItems();
        UpdateDefItems();
        UpdateSubItems();
    }
    public void UpdateAirframeItems()
    {
        int locked_afs_cnt = DB_Manager.Instance.locked_airframes.Count;
        int idx;

        if (locked_afs_cnt > 0)
        {
            item_button[0].enabled = true;
            idx = UnityEngine.Random.Range(0, locked_afs_cnt);
            item_objects[0] = DB_Manager.Instance.locked_airframes[idx];
            item_button[0].transform.Find("ItemImage").GetComponent<Image>().sprite = item_objects[0].GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            item_button[0].enabled = false;
            item_button[0].transform.Find("ItemImage").GetComponent<Image>().sprite = none;
        }
    }
    public void UpdateAtkItems()
    {
        int locked_atks_cnt = DB_Manager.Instance.locked_atks.Count;
        int idx;

        if (locked_atks_cnt > 0)
        {
            item_button[1].enabled = true;
            idx = UnityEngine.Random.Range(0, locked_atks_cnt);
            item_objects[1] = DB_Manager.Instance.locked_atks[idx];
            item_button[1].transform.Find("ItemImage").GetComponent<Image>().sprite = item_objects[1].GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            item_button[1].enabled = false;
            item_button[1].transform.Find("ItemImage").GetComponent<Image>().sprite = none;
        }
    }
    public void UpdateDefItems()
    {
        int locked_defs_cnt = DB_Manager.Instance.locked_defs.Count;
        int idx;

        if (locked_defs_cnt > 0)
        {
            item_button[2].enabled = true;
            idx = UnityEngine.Random.Range(0, DB_Manager.Instance.locked_defs.Count);
            item_objects[2] = DB_Manager.Instance.locked_defs[idx];
            item_button[2].transform.Find("ItemImage").GetComponent<Image>().sprite = item_objects[2].GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            item_button[2].enabled = false;
            item_button[2].transform.Find("ItemImage").GetComponent<Image>().sprite = none;
        }
    }
    public void UpdateSubItems()
    {
        int locked_subs_cnt = DB_Manager.Instance.locked_subs.Count;
        int idx1, idx2;

        if (locked_subs_cnt > 1)
        {
            item_button[3].enabled = true;
            item_button[4].enabled = true;
            idx1 = UnityEngine.Random.Range(0, DB_Manager.Instance.locked_subs.Count);
            idx2 = idx1;
            while (idx1 == idx2)
                idx2 = UnityEngine.Random.Range(0, DB_Manager.Instance.locked_subs.Count);
            item_objects[3] = DB_Manager.Instance.locked_subs[idx1];
            item_objects[4] = DB_Manager.Instance.locked_subs[idx2];
            item_button[3].transform.Find("ItemImage").GetComponent<Image>().sprite = item_objects[3].GetComponent<SpriteRenderer>().sprite;
            item_button[4].transform.Find("ItemImage").GetComponent<Image>().sprite = item_objects[4].GetComponent<SpriteRenderer>().sprite;
        }
        else if (locked_subs_cnt == 1)
        {
            item_button[3].enabled = true;
            item_button[4].enabled = false;
            idx1 = UnityEngine.Random.Range(0, DB_Manager.Instance.locked_subs.Count);
            item_objects[3] = DB_Manager.Instance.locked_subs[idx1];
            item_objects[4] = null;
            item_button[3].transform.Find("ItemImage").GetComponent<Image>().sprite = item_objects[3].GetComponent<SpriteRenderer>().sprite;
            item_button[4].transform.Find("ItemImage").GetComponent<Image>().sprite = none;
        }
        else
        {
            item_button[3].enabled = false;
            item_button[4].enabled = false;
            item_objects[3] = null;
            item_objects[4] = null;
            item_button[3].transform.Find("ItemImage").GetComponent<Image>().sprite = none;
            item_button[4].transform.Find("ItemImage").GetComponent<Image>().sprite = none;
        }
    }

    public void ActivateSpecificButton(int idx) // 특정 버튼 활성화
    {
        try
        {
            item_button[idx].enabled = true;
        }
        catch
        {
            Debug.Log("StorePanelListener/ActivateSpecificButton Error");
        }
    }
    public void DeactivateSpecificButton(int idx) // 특정 버튼 비활성화
    {
        try
        {
            item_button[idx].enabled = false;
        }
        catch
        {
            Debug.Log("StorePanelListener/DeactivateSpecificButton Error");
        }
    }

    public void ActivateItemInfoPanel(int idx)
    {
        try
        {
            item_info_panel.SetActive(true);
            item_info_panel.transform.SetAsLastSibling();
            item_info_panel.GetComponent<ItemInfoPanelListener>().LoadObject(item_objects[idx], idx);
        }
        catch
        {
            Debug.Log("out of index: StorePanelListener/ActivateItemInfoPanel");
        }
    }

    public void BuyClicked(GameObject obj, int button_number)
    {
        int gold = PlayerPrefs.GetInt("gold");
        int parts_gold;

        try
        {
            switch (button_number)
            {
                case 0:
                    parts_gold = obj.GetComponent<AirframeScript>().gold;
                    if (gold >= parts_gold)
                    {
                        gold -= parts_gold;
                        DB_Manager.Instance.unlocked_airframes.Add(obj);
                        DB_Manager.Instance.locked_airframes.Remove(obj);
                        PlayerPrefs.SetString(obj.name, "unlocked");
                        PlayerPrefs.SetInt("gold", gold);
                        item_button[button_number].transform.Find("ItemImage").GetComponent<Image>().sprite = none;
                        DeactivateSpecificButton(button_number);
                    }
                    break;
                case 1:
                    parts_gold = obj.GetComponent<BulletFire>().gold;
                    if (gold >= parts_gold)
                    {
                        gold -= parts_gold;
                        DB_Manager.Instance.unlocked_atks.Add(obj);
                        DB_Manager.Instance.locked_atks.Remove(obj);
                        PlayerPrefs.SetString(obj.name, "unlocked");
                        PlayerPrefs.SetInt("gold", gold);
                        item_button[button_number].transform.Find("ItemImage").GetComponent<Image>().sprite = none;
                        DeactivateSpecificButton(button_number);
                    }
                    break;
                case 2:
                    parts_gold = obj.GetComponent<DefScript>().gold;
                    if (gold >= parts_gold)
                    {
                        gold -= parts_gold;
                        DB_Manager.Instance.unlocked_defs.Add(obj);
                        DB_Manager.Instance.locked_defs.Remove(obj);
                        PlayerPrefs.SetString(obj.name, "unlocked");
                        PlayerPrefs.SetInt("gold", gold);
                        item_button[button_number].transform.Find("ItemImage").GetComponent<Image>().sprite = none;
                        DeactivateSpecificButton(button_number);
                    }
                    break;
                case 3:
                case 4:
                    parts_gold = obj.GetComponent<SubScript>().gold;
                    if (gold >= parts_gold)
                    {
                        gold -= parts_gold;
                        DB_Manager.Instance.unlocked_subs.Add(obj);
                        DB_Manager.Instance.locked_subs.Remove(obj);
                        PlayerPrefs.SetString(obj.name, "unlocked");
                        PlayerPrefs.SetInt("gold", gold);
                        item_button[button_number].transform.Find("ItemImage").GetComponent<Image>().sprite = none;
                        DeactivateSpecificButton(button_number);
                    }
                    break;
            }
        }
        catch
        {
            Debug.Log("StorePanelListener/BuyClicked Error");
        }
    }

    void Update()
    {
        DateTime refresh = DateTime.ParseExact(PlayerPrefs.GetString("refresh"), "yyyyMMddHHmmss", null);
        TimeSpan remaining_time;

        if (refresh <= DateTime.Now) // 남은 시간이 없는 경우
        {
            PlayerPrefs.SetString("refresh", DateTime.Now.AddDays(0.5).ToString("yyyyMMddHHmmss", null)); // 12시간 뒤에 버튼 활성화

            // 템 리스트 변경
            UpdateAllItems();
        }
        else
        {
            remaining_time = refresh - DateTime.Now;
            remaining_time_text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", remaining_time.Hours, remaining_time.Minutes, remaining_time.Seconds);
        }
    }
}
