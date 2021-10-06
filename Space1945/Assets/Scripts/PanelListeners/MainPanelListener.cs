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

    private void Start()
    {
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

            if (chapter[i] == '1' || i == 0) // ���� ���������� Ŭ���� �߰ų� ù ���������ų�
            {
                ch_list[i].gameObject.SetActive(true); // ���� �������� ����
                if (chapter[i] == '0') // ù ���������� Ŭ���� ��������
                    ch_list[i].image.color = new Color(220, 0, 0);// ������
            }
            else // ���� Ŭ���� ���� 
            {
                ch_list[i].gameObject.SetActive(chapter[i - 1] == '1'); // �� ���������� Ŭ���� ���ο� ���� ��ư Ȱ��ȭ ��Ȱ��ȭ
                if (chapter[i - 1] == '1') // �� ���������� Ŭ���� ���� ���
                    ch_list[i].image.color = new Color(220, 0, 0); // ���� �������� ������
            }
        }
    }

    public void PlayEffectSound()
    {
        if (PlayerPrefs.GetString("es") == "true")
            audio_source.Play();
    }
}
