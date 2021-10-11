using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsPanelListener : MonoBehaviour
{
    public int button_number { get; set; }
    int button_cnt;

    public Button cancel_button;
    public Button equip_button;

    public Button item_frame;

    // Start is called before the first frame update
    void Start()
    {
        switch (button_number)
        {
            case 0:
                button_cnt = DB_Manager.Instance.unlocked_airframes.Count + 1;
                break;
            case 1:
                button_cnt = DB_Manager.Instance.unlocked_atks.Count + 1;
                break;
            case 2:
                button_cnt = DB_Manager.Instance.unlocked_defs.Count + 1;
                break;
            case 3:
            case 4:
                button_cnt = DB_Manager.Instance.unlocked_subs.Count + 2;
                break;
        }

        for (int i = 0; i < button_cnt; i++)
        {
            Button item = Instantiate(item_frame);

            item.transform.GetComponent<RectTransform>().offsetMin = new Vector2(16 * (i % 4 + 1) + 200 * (i % 4), 1500 - 216 * (i / 4 + 1));
            item.transform.GetComponent<RectTransform>().offsetMax = new Vector2(216 * (i % 4 + 1), 1500 - (16 * (i / 4 + 1) + 200 * (i / 4)));
        }
    }

    public void CancelButtonClicked()
    {
        gameObject.SetActive(false);
    }
    public void EquipButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
