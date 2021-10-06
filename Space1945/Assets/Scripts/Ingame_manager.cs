using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ingame_manager : MonoBehaviour
{
    public int elite_appear_score { get; set; } // ���� ������ ȹ���ϸ� ����Ʈ �� ��ȯ

    public GameObject[] spwan_points;

    public List<GameObject> Enemy { get; set; } = new List<GameObject>();
    public HashSet<GameObject> e = new HashSet<GameObject>();
    public int enemy_cnt { get; set; } // �� �� �����Ҷ� ++, ������ --
    float time = 0;

    GameObject[] normals;
    GameObject elites;
    GameObject boss; // ���� ��ü�� �ϳ�

    string selected_chapter;
    string selected_stage;

    GameObject[] BG;

    GameObject player;
    public GameObject player_clone { get; set; } // ��𼭵��� �÷��̾� ��ü ���� ����

    /*
     ***** player �ñر� ���� *****
     */
    public Button Ultimate;
    float mUg; // �ִ� �ñر� ������ ��
    float cUg; // ���� �ñر� ������ ��

    public Image HealthBar; // �÷��̾� ü�¹�
   
    Vector2 player_point;
    
    // Start is called before the first frame update
    void Start() 
    {
        selected_chapter = DB_Manager.Instance.selected_chapter.ToString();
        selected_stage = DB_Manager.Instance.selected_stage.ToString();

        string dir = "stage_" + selected_chapter + "_" + selected_stage;
        int mob_cnt = PlayerPrefs.GetString(dir + "_name").Length;

        normals = Resources.LoadAll<GameObject>("Enemy/Normal/" + PlayerPrefs.GetString(dir + "_type") + "/");
        elites = Resources.Load<GameObject>("Enemy/Elite/" + PlayerPrefs.GetString(dir + "_type") + "/Elite" + selected_chapter);
        if (PlayerPrefs.GetString("Chapter" + selected_chapter).Length == DB_Manager.Instance.selected_stage)
            boss = Resources.Load<GameObject>("Enemy/Boss/" + PlayerPrefs.GetString(dir + "_type") + "/Boss" + selected_chapter);

        BG = Resources.LoadAll<GameObject>("Maps/BG/");

        player = DB_Manager.Instance.using_airframe;
        player_clone = Instantiate(player); // ����
        player_clone.GetComponent<AirframeScript>().SetReady();
        
        mUg = 100; // �÷��̾��� ��ü�� �°� �����Ұ�

        foreach (GameObject obj in BG)
            Instantiate(obj);

        /*
        int i;
        for (i = 0; i < mob_cnt; i++)
            Invoke("Mob_count", mob_list[i].mob_time);
        time = mob_list[i - 1].mob_time * 50f;

        Ultimate.onClick.AddListener(Use_ultimate);
        Ultimate.enabled = false;*/
    }
    void Mob_count() // �Ϲ� ��
    {/*
        Set_point(normal_mobs[mob_list[index].mob_num - 1]);
        Fly f = gameObject.AddComponent<Fly>();
        f.Mobs_fly(start_point, player_point, rand, normal_mobs[mob_list[index].mob_num - 1], 0.5f, mob_position);
        index++;*/
    }

    /*                 �ñر�                  */
    public void Plus_ultimate_guage(float ultimate_guage) // �ñر� ������ �߰�
    {
        cUg = (cUg + ultimate_guage) < mUg ? cUg + ultimate_guage : mUg;

        Ultimate.GetComponent<Image>().fillAmount = (float)cUg / (float)mUg;

        if (cUg == 100)
        {
            //Ư�� �̹��� ���
            Ultimate.enabled = true;
        }
    }

    void Use_ultimate() // �ñر� ���
    {
        cUg = 0;
        Ultimate.GetComponent<Image>().fillAmount = 0;
        Ultimate.enabled = false;
    }
    /*                 ü�¹�                  */
    public void Player_attacked() // �÷��̾� �ǰ����� �� �÷��̾��� ü���� ���� �߻�
    {
        HealthBar.fillAmount = (float)player_clone.GetComponent<Player>().curHp / player_clone.GetComponent<AirframeScript>().maxHp;
    }
    /*                 ���ھ�, ����ġ, ��                  */
    public void Add_point(int score,int exp,int gold)
    {/*
        DB_Manager.Instance.score_earned += score;
        elite_appear_score += score;
        if (elite_appear_score >= GV.Elite_Appear_Score) // 500�� �� ����Ʈ �Ѹ����� ����
        {
            elite_appear_score -= GV.Elite_Appear_Score;
            // ����Ʈ ȣ��
            Set_point(elite_mobs[0]);
            Fly f = gameObject.AddComponent<Fly>();
            f.Mobs_fly(start_point, player_point, rand, elite_mobs[0], 0, mob_position);
        }

        DB_Manager.Instance.exp_earned += exp;
        DB_Manager.Instance.gold_earned += gold;*/
    }
    public GameObject GetPlayer() // ���� ȸ�����ؼ� ������µ� static ���ɾ����� �𸣰��� Butt Ŭ�������� ���
    {
        return player_clone != null ? player_clone : null;
    }

    private void FixedUpdate() // �ΰ����� ���������� ��� Ȯ��
    {/*
        if (player_clone == null) // ���
        {
            DB_Manager.Instance.stage_clear = false;
            SceneManager.LoadScene("GameEnd");
        }
        else if (enemy_cnt == 0 && time <= 0)
        {
            DB_Manager.Instance.stage_clear = true;
            SceneManager.LoadScene("GameEnd");
        }
        time--;*/
    }
}
class Fly : MonoBehaviour // �ϳ��� ���� ���� -> �� �����̹Ƿ� ���� ���� ������ �����ؾ���.
{
    Vector2 start;
    Vector2 player_point;
    int area;
    GameObject mob;

    int count = 0; // mob info���� ������
    int pattern_number;
    float mob_position;
    int speed;

    public void Mobs_fly(Vector2 start_point, Vector2 player_point, int rand, GameObject mobs, float time, float mob_position) // ��������, �� �Լ��� function() ȣ��
    {
        start = start_point;
        this.player_point = player_point;
        area = rand - 1;
        mob = Instantiate(mobs);
        mob.transform.position = start;
        this.mob_position = mob_position;

        // ������ ������ �̵� ����, ���� ������ ������ ����
//        int idx = Random.Range(0, mob.GetComponent<Mob_info>().pattern.Length);
//        pattern_number = mob.GetComponent<Mob_info>().pattern[idx];
//        speed = mob.GetComponent<Mob_info>().speed;
        for (int i = 0; i < mob.GetComponent<Mob_info>().instantiate_count; i++)
            Invoke("function", time * i);
    }
    public void function()
    {
        count++;
        GameObject mob_temp = Instantiate(mob); // ����
        switch (mob_temp.GetComponent<Mob_info>().kind)
        {
            case "normal":
                mob_temp.GetComponent<Normal_Patterns>().SetPattern(area, start, player_point, mob_position, speed);
                break;
            case "elite":
//                mob_temp.GetComponent<Elite_Patterns>().SetPattern(area, start, player_point, mob_position, speed);
                break;
            case "boss":
                break;
        }

        mob_temp.layer = GV.ENEMY_LAYER;

        Camera.main.GetComponent<Ingame_manager>().Enemy.Add(mob_temp);
        Camera.main.GetComponent<Ingame_manager>().enemy_cnt++;

        // ������ �ش� ������ŭ ���������� ����
        if (count == mob.GetComponent<Mob_info>().instantiate_count)
            Destroy(mob);
    }
}
