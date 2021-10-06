using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ingame_manager : MonoBehaviour
{
    public int elite_appear_score { get; set; } // ���� ������ ȹ���ϸ� ����Ʈ �� ��ȯ

    public GameObject[] spwan_points;

    public HashSet<GameObject> enemys { get; set; }

    int time = 0;
    int idx = 0;

    string selected_chapter;
    string selected_stage;
    List<GameObject> normals; // ������ �Ϲݸ�
    List<int> normals_time; // ������ �Ϲݸ� �ð� 
    List<GameObject> elites; // ������ ����Ʈ
    GameObject boss;

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

        Initiate();
        ReadStage();

        player = DB_Manager.Instance.using_airframe;
        player_clone = Instantiate(player); // ����
        player_clone.GetComponent<AirframeScript>().SetReady();
        
        mUg = 100; // �÷��̾��� ��ü�� �°� �����Ұ�

        foreach (GameObject obj in BG)
            Instantiate(obj);

        Ultimate.onClick.AddListener(Use_ultimate);
        Ultimate.enabled = false;
    }

    void Initiate()
    {
        enemys = new HashSet<GameObject>();

        normals = new List<GameObject>();
        normals_time = new List<int>();
        elites = new List<GameObject>();
    }

    void ReadStage()
    {
        string stage = "Stage" + selected_chapter + "_" + selected_stage;
        string stage_type = PlayerPrefs.GetString(stage + "Type");

        string[] normals = PlayerPrefs.GetString(stage + "Normals").Split(' ');
        for (int i = 0; i < normals.Length; i++)
            this.normals.Add(Resources.Load<GameObject>("Enemy/Normal/" + stage_type + "/" + normals[i]));
        string[] normals_time = PlayerPrefs.GetString(stage + "NormalsTime").Split(' ');
        for (int i = 0; i < normals_time.Length; i++)
            this.normals_time.Add(int.Parse(normals_time[i]));

        string[] elites = PlayerPrefs.GetString(stage + "Elites").Split(' ');
        for (int i = 0; i < elites.Length; i++)
            this.elites.Add(Resources.Load<GameObject>("Enemy/Elite/" + stage_type + "/" + elites[i]));

        if (PlayerPrefs.HasKey(stage + "Boss"))
        {
            string boss = PlayerPrefs.GetString(stage + "Boss");
            this.boss = Resources.Load<GameObject>("Enemy/Boss/" + stage_type + "/" + boss);
        }

        BG = Resources.LoadAll<GameObject>("Maps/BG/");
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

        if (time > normals_time[idx])
        {
            int rand_idx = Random.Range(0, normals.Count);
            int rand_point_idx = Random.Range(0, spwan_points.Length);

            while (spwan_points[rand_point_idx].GetComponent<InstantiateThread>().Creating())
                rand_point_idx = Random.Range(0, spwan_points.Length);
            spwan_points[rand_point_idx].GetComponent<InstantiateThread>().SetObject(normals[rand_idx], 5, 25);
            idx++;
            time = 0;
        }
        time++;
    }
}