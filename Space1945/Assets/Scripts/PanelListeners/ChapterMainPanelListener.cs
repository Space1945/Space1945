using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterMainPanelListener : MonoBehaviour
{
    public Button stage_button_ori;
    public Button[][] stage_list = new Button[8][];

    public GameObject content;

    private void Start()
    {
        float width = content.GetComponent<RectTransform>().rect.width;
        float height = content.GetComponent<RectTransform>().rect.height;
        int stage_cnt = 0;
        Debug.Log(width);
        Debug.Log(height);
        for (int i = 0; i < 7; i++)
        {
            if (stage_cnt == 1)
                stage_cnt = Random.Range(2, 4);
            else
                stage_cnt = Random.Range(1, 4);
            stage_list[i] = new Button[stage_cnt];
            for (int j = 0; j < stage_cnt; j++)
            {
                Button stage_btn = Instantiate(stage_button_ori);
                stage_btn.GetComponent<StageButtonListener>().layer = i;
                stage_btn.GetComponent<StageButtonListener>().difficulty = Random.Range(1, 4);
                ColorBlock stage_color = stage_btn.colors;
                if (stage_btn.GetComponent<StageButtonListener>().difficulty == 1)
                {
                    stage_btn.GetComponent<Image>().color = new Color(0, 255, 0, 255);
                }
                if (stage_btn.GetComponent<StageButtonListener>().difficulty == 2)
                {
                    stage_btn.GetComponent<Image>().color = new Color(255, 255, 0, 255);
                }
                if (stage_btn.GetComponent<StageButtonListener>().difficulty == 3)
                {
                    stage_btn.GetComponent<Image>().color = new Color(255, 0, 0, 255);
                }
                stage_btn.transform.SetParent(content.transform);
                stage_btn.transform.localPosition = new Vector2(width / (stage_cnt + 1) * (j + 1), height / 10 * (i + 1));
                stage_list[i][j] = stage_btn;
            }
        }
        Button boss_btn = Instantiate(stage_button_ori);
        boss_btn.GetComponent<StageButtonListener>().layer = 8;
        boss_btn.GetComponent<StageButtonListener>().difficulty = 4;
        boss_btn.transform.SetParent(content.transform);
        boss_btn.transform.localPosition = new Vector2(width / 2, height / 10 * 9);
        boss_btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width / 3);
        boss_btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, width / 3);
        stage_list[7][0] = boss_btn;
        Destroy(stage_button_ori);
    }
}
