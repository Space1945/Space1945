using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterMainPanelListener : MonoBehaviour
{
    public GameObject stage_button_ori;
    public GameObject[][] stage_list = new GameObject[15][];

    public Sprite store_img;
    public Sprite upgrade_img;
    public Sprite elite_img;

    public GameObject content;

    string[] type = { "store", "upgrade", "mob", "elite" };
    int[] type_cnt = { 2, 1, 17, 3 };

    private int sum_stage()
    {
        int sum = 0;
        for (int i = 0; i < type_cnt.Length; i++)
            sum += type_cnt[i];
        return sum;
    }
    private void Start()
    {
        float width = content.GetComponent<RectTransform>().rect.width;
        float height = content.GetComponent<RectTransform>().rect.height;
        float min = width / 5 < height / 20 ? width / 5 : height / 20;

        int stage_cnt = 0;
        

        for (int i = 0; i < 9; i++)
        {
            if (stage_cnt == 1)
                stage_cnt = 3;
            else
                stage_cnt = Random.Range(1, 4);
            stage_list[i] = new GameObject[stage_cnt];
            for (int j = 0; j < stage_cnt && sum_stage() > 0; j++)
            {
                int stage_type = Random.Range(0, type.Length);
                GameObject stage_btn = Instantiate(stage_button_ori);
                stage_btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, min);
                stage_btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, min);
                stage_btn.GetComponent<StageButtonListener>().layer = i;
                stage_btn.GetComponent<StageButtonListener>().difficulty = Random.Range(1, 4);
                if (type_cnt[stage_type] == 0)
                    j--;
                else
                {
                    if (type[stage_type] == "store")
                    {
                        stage_btn.GetComponent<StageButtonListener>().type = "store";
                        stage_btn.GetComponent<Image>().sprite = store_img;
                        type_cnt[stage_type]--;
                    }
                    else if (type[stage_type] == "upgrade")
                    {
                        stage_btn.GetComponent<StageButtonListener>().type = "upgrade";
                        stage_btn.GetComponent<Image>().sprite = upgrade_img;
                        type_cnt[stage_type]--;
                    }
                    else if (type[stage_type] == "mob")
                    {
                        stage_btn.GetComponent<StageButtonListener>().type = "mob";
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
                        type_cnt[stage_type]--;
                    }
                    else
                    {
                        stage_btn.GetComponent<Image>().sprite = elite_img;
                        stage_btn.GetComponent<Image>().color = new Color(255, 0, 0, 255);
                        stage_btn.GetComponent<StageButtonListener>().type = "elite";
                        type_cnt[stage_type]--;
                    }
                    stage_btn.transform.SetParent(content.transform);
                    stage_btn.transform.localPosition = new Vector2(width / (stage_cnt + 1) * (j + 1), height / 11 * (i + 1));
                    stage_list[i][j] = stage_btn;
                }
            }
        }
        GameObject boss_btn = Instantiate(stage_button_ori);
        boss_btn.GetComponent<StageButtonListener>().layer = 9;
        boss_btn.GetComponent<StageButtonListener>().difficulty = 4;
        boss_btn.transform.SetParent(content.transform);
        boss_btn.transform.localPosition = new Vector2(width / 2, height / 10 * 9);
        boss_btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, min * 1.4f);
        boss_btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, min * 1.4f);
        stage_list[9] = new GameObject[1];
        stage_list[9][0] = boss_btn;
        Destroy(stage_button_ori);
    }
}
