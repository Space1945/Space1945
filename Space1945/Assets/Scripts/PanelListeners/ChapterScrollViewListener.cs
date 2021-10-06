using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterScrollViewListener : MonoBehaviour
{
    public int chapter_number;
    public Button[] stage_buttons;
    public Sprite[] unlocked_stage_images;

    // Start is called before the first frame update
    void Start()
    {
        string stages = PlayerPrefs.GetString("chapter" + chapter_number);
        for (int i = 0; i < stages.Length; i++)
        {
            if (stages[i] == '1') // 클리어했거나 클리어해야하거나
            {
                stage_buttons[i].GetComponent<Image>().sprite = unlocked_stage_images[i];
                stage_buttons[i].enabled = true;
            }
            else // 클리어해야되는 스테이지 이후는 접근 불가
                stage_buttons[i].enabled = false;
        }
    }
}