using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanelListener : MonoBehaviour
{
    public Button[] ch_list;

    public GameObject chapter1_scrollview;
    public GameObject chapter2_scrollview;
    public GameObject chapter3_scrollview;
    public GameObject chapter4_scrollview;

    AudioSource audio_source;
    public AudioClip button_clicked;

    void Start()
    {
        Debug.Log("�����г� Ȱ��ȭ");

        audio_source = GetComponent<AudioSource>();
        audio_source.clip = button_clicked;
        audio_source.playOnAwake = false;

        chapter1_scrollview.SetActive(false);
        chapter2_scrollview.SetActive(false);
        chapter3_scrollview.SetActive(false);
        chapter4_scrollview.SetActive(false);

        for (int i = 0; i < ch_list.Length; i++)
        {
            string chapter = PlayerPrefs.GetString("chapter" + (i + 1).ToString());
            if (chapter[chapter.Length - 1] == '1') // ���� é�͸� Ŭ���� 
            {
                ch_list[i].gameObject.SetActive(true); // ���� é�� ����
                ch_list[i].GetComponent<ChapterButtonListener>().clear = true;
            }
            else // ���� Ŭ���� ����
            {
                if (i == 0 || ch_list[i - 1].GetComponent<ChapterButtonListener>().clear) // �� é�͸� Ŭ���� �߰ų� ù é�Ͱų�
                {
                    ch_list[i].gameObject.SetActive(true);
                    ch_list[i].image.color = new Color(220, 0, 0); // ���� é�� ������
                }
                else
                    ch_list[i].gameObject.SetActive(false);
            }
        }
    }

    public void PlayEffectSound()
    {
        if (PlayerPrefs.GetString("es") == "true")
            audio_source.Play();
    }
}
