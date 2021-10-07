using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ingame_manager : MonoBehaviour
{
    public GameObject[] spwan_points;

    public HashSet<GameObject> enemys { get; set; }

    int time = 0;
    int idx = 0;
    public int count { get; set; }

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

    // Start is called before the first frame update
    void Start() 
    {
        selected_chapter = DB_Manager.Instance.selected_chapter.ToString();
        selected_stage = DB_Manager.Instance.selected_stage.ToString();

        Allocate();
        ReadStage();
        DB_Manager.Instance.InitStageDB();

        player = DB_Manager.Instance.using_airframe;
        player_clone = Instantiate(player); // ����
        player_clone.GetComponent<AirframeScript>().SetReady();
        
        mUg = 100; // �÷��̾��� ��ü�� �°� �����Ұ�

        foreach (GameObject obj in BG)
            Instantiate(obj);

        Ultimate.enabled = false;
    }

    void Allocate()
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
    public void AddUltimateGuage(float ultimate_guage) // �ñر� ������ �߰�
    {
        cUg = (cUg + ultimate_guage) < mUg ? cUg + ultimate_guage : mUg;

        DisplayUltimateBar();

        if (cUg >= 100)
        {
            //Ư�� �̹��� ���
            Ultimate.enabled = true;
        }
    }
    public void UseUltimate() // �ñر� ���
    {
        cUg = 0;
        DisplayUltimateBar();
        Ultimate.enabled = false;
    }
    private void DisplayUltimateBar()
    {
        Ultimate.GetComponent<Image>().fillAmount = cUg / mUg;
    }

    /*                 ü�¹�                  */
    public void DisplayPlayersHP() // �÷��̾� �ǰ����� �� �÷��̾��� ü���� ���� �߻�
    {
        HealthBar.fillAmount = (float)player_clone.GetComponent<Player>().curHp / player_clone.GetComponent<AirframeScript>().maxHp;
    }

    /*                 ���ھ�, ����ġ, ��                  */
    public void KillEnemy(int score, int exp, int gold)
    {
        DB_Manager.Instance.score_earned += score;
        DB_Manager.Instance.exp_earned += exp;
        DB_Manager.Instance.gold_earned += gold;
        DB_Manager.Instance.enemy_killed_cnt++;

        if (DB_Manager.Instance.enemy_killed_cnt >= PlayerPrefs.GetInt("Stage1_1ElitesEmerCnt")) // �Ϲݸ� 10ų�� ����Ʈ �Ѹ����� ����
        {
            DB_Manager.Instance.enemy_killed_cnt = 0;
            // ����Ʈ ȣ��
            int rand_idx = Random.Range(0, elites.Count);
            int rand_point_idx = Random.Range(0, spwan_points.Length);

            while (spwan_points[rand_point_idx].GetComponent<InstantiateThread>().Ready())
                rand_point_idx = Random.Range(0, spwan_points.Length);
            spwan_points[rand_point_idx].GetComponent<InstantiateThread>().SetObject(elites[rand_idx], 50);
        }
    }

    public GameObject GetPlayer() // ���� ȸ�����ؼ� ������µ� static ���ɾ����� �𸣰��� Butt Ŭ�������� ���
    {
        return player_clone != null ? player_clone : null;
    }

    private void FixedUpdate() // �ΰ����� ���������� ��� Ȯ��
    {
        if (player_clone == null) // ���
        {
            DB_Manager.Instance.stage_clear = false;
            SceneManager.LoadScene("GameEnd");
        }
        else if (enemys.Count == 0 && time >= 500)
        {
            DB_Manager.Instance.stage_clear = true;
            SceneManager.LoadScene("GameEnd");
        }

        if (idx < normals_time.Count && time > normals_time[idx])
        {
            time = 0;

            int rand_idx = Random.Range(0, normals.Count);
            int rand_point_idx = Random.Range(0, spwan_points.Length);

            while (spwan_points[rand_point_idx].GetComponent<InstantiateThread>().Ready())
                rand_point_idx = Random.Range(0, spwan_points.Length);
            spwan_points[rand_point_idx].GetComponent<InstantiateThread>().SetObject(normals[rand_idx], 50);

            idx++;
        }
        time++;
    }
}