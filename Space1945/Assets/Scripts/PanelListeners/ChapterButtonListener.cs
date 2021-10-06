using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterButtonListener : MonoBehaviour
{
    public GameObject chapter_scrollview;
    public int chapter_name;
    public void SaveChapterName()
    {
        DB_Manager.Instance.selected_chapter = chapter_name;
        chapter_scrollview.SetActive(true);
    }
}
