using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanelListener : MonoBehaviour
{
    private GameObject parts;
    private int button_number;

    public Image parts_image;

    public void LoadObject(GameObject obj, int button_number)
    {
        parts_image.GetComponent<Image>().sprite = obj.GetComponent<SpriteRenderer>().sprite;
        parts = obj;
        this.button_number = button_number;
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
    public void Buy()
    {
        transform.parent.GetComponent<StorePanelListener>().BuyClicked(parts, button_number);
        gameObject.SetActive(false);
    }
}
