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
    public int chapter_num { get; set; } = 3;

    const int max_layer = 13;
    List<int> store_layer = new List<int> { max_layer / 4, max_layer / 4 * 2, max_layer - 1 };
    int upgrade_layer = max_layer / 4 * 3;
    List<int> elite_layer = new List<int>();
    private void CreateStoreStage(GameObject stage_btn, int i)
    {
        stage_btn.GetComponent<StageButtonListener>().type = "store";
        stage_btn.GetComponent<Image>().sprite = store_img;
    }
    private void CreateUpgradeStage(GameObject stage_btn, int i)
    {
        stage_btn.GetComponent<StageButtonListener>().type = "upgrade";
        stage_btn.GetComponent<Image>().sprite = upgrade_img;
    }
    private void CreateEliteStage(GameObject stage_btn, int i)
    {
        stage_btn.GetComponent<StageButtonListener>().difficulty = 3;
        stage_btn.GetComponent<Image>().sprite = elite_img;
        stage_btn.GetComponent<Image>().color = new Color(255, 0, 0, 255);
        stage_btn.GetComponent<StageButtonListener>().type = "elite";
    }
    private void CreateMobStage(GameObject stage_btn, int i)
    {
        int diff = Random.Range(0, 100);
        if (0 <= diff && diff < 8 * i)
        {
            stage_btn.GetComponent<Image>().color = new Color(255, 255, 0, 255);
            stage_btn.GetComponent<StageButtonListener>().difficulty = 2;
        }
        else
        {
            stage_btn.GetComponent<Image>().color = new Color(0, 255, 0, 255);
            stage_btn.GetComponent<StageButtonListener>().difficulty = 1;
        }
        stage_btn.GetComponent<StageButtonListener>().type = "mob";
    }
    private void Start()
    {
        float width = content.GetComponent<RectTransform>().rect.width;
        float height = content.GetComponent<RectTransform>().rect.height;
        float min = width / 5 < height / 15 ? width / 5 : height / 15;

        int stage_cnt = 0;
        int max_elite = 2 + chapter_num;

        for (int i = 0; i < max_elite - 1; i++)
        {
            int appear_layer = Random.Range(2, 11);
            if (elite_layer.Find(x => x == appear_layer) == appear_layer || store_layer.Find(x => x == appear_layer) == appear_layer || upgrade_layer == appear_layer)
            {
                i--;
                continue;
            }
            Debug.Log(appear_layer);
            elite_layer.Add(appear_layer);
        }
        elite_layer.Add(max_layer - 1);

        for (int i = 0; i < max_layer; i++)
        {
            int store_cnt = 1;
            int upgrade_cnt = 1;
            int elite_cnt = 1;
            List<GameObject> layer_list = new List<GameObject>();

            if (i == max_layer - 1)
                stage_cnt = 2;
            else
                stage_cnt = Random.Range(2, 4);

            stage_list[i] = new GameObject[stage_cnt];

            for (int j = 0; j < stage_cnt; j++)
            {
                GameObject stage_btn = Instantiate(stage_button_ori);
                stage_btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, min);
                stage_btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, min);
                stage_btn.GetComponent<StageButtonListener>().layer = i;
                if (store_layer.Find(x => x == i) == i && store_cnt != 0 && i != 0)
                {
                    CreateStoreStage(stage_btn, i);
                    store_cnt--;
                }
                else if ((i == upgrade_layer) && upgrade_cnt != 0)
                {
                    CreateUpgradeStage(stage_btn, i);
                    upgrade_cnt--;
                }
                else if (elite_layer.Find(x => x == i) == i && elite_cnt != 0 && i != 0)
                {
                    CreateEliteStage(stage_btn, i);
                    elite_cnt--;
                }
                else
                    CreateMobStage(stage_btn, i);

                layer_list.Add(stage_btn);
            }
            for (int j = 0; j < stage_cnt; j++)
            {
                int k = Random.Range(0, layer_list.Count);
                stage_list[i][j] = layer_list[k];
                layer_list.RemoveAt(k);
                stage_list[i][j].transform.SetParent(content.transform);
                stage_list[i][j].transform.localPosition = new Vector2(width / (stage_cnt + 1) * (j + 1), height / 15 * (i + 1));
            }
        }
        GameObject boss_btn = Instantiate(stage_button_ori);
        boss_btn.GetComponent<StageButtonListener>().layer = max_layer;
        boss_btn.GetComponent<StageButtonListener>().difficulty = 4;
        boss_btn.transform.SetParent(content.transform);
        boss_btn.transform.localPosition = new Vector2(width / 2, height / 15 * 14);
        boss_btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, min * 1.4f);
        boss_btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, min * 1.4f);
        stage_list[9] = new GameObject[1];
        stage_list[9][0] = boss_btn;
        Destroy(stage_button_ori);
    }
}
