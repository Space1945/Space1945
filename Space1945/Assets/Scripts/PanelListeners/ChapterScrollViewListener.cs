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
            if (stages[i] == '1') // Ŭ�����߰ų� Ŭ�����ؾ��ϰų�
            {
                stage_buttons[i].GetComponent<Image>().sprite = unlocked_stage_images[i];
                stage_buttons[i].enabled = true;
            }
            else // Ŭ�����ؾߵǴ� �������� ���Ĵ� ���� �Ұ�
                stage_buttons[i].enabled = false;
        }
    }
}