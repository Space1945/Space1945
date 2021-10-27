using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ingame_manager : MonoBehaviour
{
    public GameObject[] spwan_points;
    public float check_rate;

    public bool ultimate_use { get; set; }
    public HashSet<GameObject> enemys { get; set; }

    string selected_chapter;
    string selected_stage;
    List<GameObject> normals; // 출현할 일반몹
    List<int> normals_time; // 출현할 일반몹 시간
    List<GameObject> elites; // 출현할 엘리트
    int elite_emer_cnt; // 엘리트 출현 마릿수
    GameObject boss;

    GameObject[] BG;

    GameObject player;
    public GameObject player_clone { get; set; } // 어디서든지 플레이어 객체 접근 가능
    public DB_Manager.ex_stats ex_total { get; set; }

    /*
     ***** player 궁극기 관리 *****
     */
    public Button Ultimate;
    float mUg; // 최대 궁극기 게이지 양
    float cUg; // 현재 궁극기 게이지 양

    public Image HealthBar; // 플레이어 체력바

    // Start is called before the first frame update
    void Awake()
    {
        Reinforce();
    }
    void Start()
    {
        selected_chapter = DB_Manager.Instance.selected_chapter.ToString();
        selected_stage = DB_Manager.Instance.selected_stage.ToString();

        Initiate();
        ReadStage();
        DB_Manager.Instance.InitStageDB();

        player = DB_Manager.Instance.using_airframe;
        player_clone = Instantiate(player); // 복제

        mUg = 100; // 플레이어의 기체에 맞게 수정할것

        foreach (GameObject obj in BG)
            Instantiate(obj);

        Ultimate.enabled = false;

        StartCoroutine(CreateNormalEnemy());
        StartCoroutine(CheckGameEnd(check_rate));
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
        elite_emer_cnt = PlayerPrefs.GetInt("Stage" + selected_chapter + "_" + selected_stage + "ElitesEmerCnt");

        if (PlayerPrefs.HasKey(stage + "Boss"))
        {
            string boss = PlayerPrefs.GetString(stage + "Boss");
            this.boss = Resources.Load<GameObject>("Enemy/Boss/" + stage_type + "/" + boss);
        }

        BG = Resources.LoadAll<GameObject>("Maps/BG/");
    }

    void Reinforce()
    {
        ex_total = new DB_Manager.ex_stats();

        ex_total.ex_hp = PlayerPrefs.GetFloat("ex_hp");
        ex_total.ex_def = PlayerPrefs.GetFloat("ex_def");
        ex_total.ex_crash_dmg = PlayerPrefs.GetFloat("ex_crash_dmg");
        ex_total.ex_bullet_dmg = PlayerPrefs.GetFloat("ex_bullet_dmg");
        ex_total.ex_fire_rate = PlayerPrefs.GetFloat("ex_fire_rate");
        ex_total.ex_crit_chance = PlayerPrefs.GetFloat("ex_crit_chance");
        ex_total.ex_crit_dmg = PlayerPrefs.GetFloat("ex_crit_dmg");
        ex_total.ex_gold = PlayerPrefs.GetFloat("ex_gold");
        ex_total.ex_exp = PlayerPrefs.GetFloat("ex_exp");
        ex_total.ex_drop = PlayerPrefs.GetFloat("ex_drop");
    }

    /*                 궁극기                  */
    public void AddUltimateGuage(float ultimate_guage) // 궁극기 게이지 추가
    {
        if (!ultimate_use)
        {
            cUg = (cUg + ultimate_guage) < mUg ? cUg + ultimate_guage : mUg;

            UpdateUltimateBar();

            if (cUg >= 100)
            {
                //특정 이미지 출력
                Ultimate.enabled = true;
            }
        }
    }
    public void UseUltimate() // 궁극기 사용
    {
        cUg = 0;
        StartCoroutine(player_clone.GetComponent<UltimateInterface>().Ultimate());
        UpdateUltimateBar();
        Ultimate.enabled = false;
    }

    public void UpdateUltimateBar()
    {
        Ultimate.GetComponent<Image>().fillAmount = cUg / mUg;
    }

    /*                 체력바                  */
    public void UpdatePlayersHP() // 플레이어 피격판정 등 플레이어의 체력의 변동 발생
    {
        HealthBar.fillAmount = player_clone.GetComponent<AirframeScript>().cur_hp / player_clone.GetComponent<AirframeScript>().max_hp;
    }

    /*                 스코어, 경험치, 돈                  */
    public void KillEnemy(int score, int exp, int gold)
    {
        DB_Manager.Instance.score_earned += score;
        DB_Manager.Instance.exp_earned += (int)(exp * (1 + Camera.main.GetComponent<Ingame_manager>().ex_total.ex_exp / 100f));
        DB_Manager.Instance.gold_earned += (int)(gold * (1 + Camera.main.GetComponent<Ingame_manager>().ex_total.ex_gold / 100f));
        DB_Manager.Instance.enemy_killed_cnt++;

        if (DB_Manager.Instance.enemy_killed_cnt >= elite_emer_cnt) // 일반몹 10킬당 엘리트 한마리씩 출현
        {
            DB_Manager.Instance.enemy_killed_cnt = 0;
            // 엘리트 호출
            int rand_idx = Random.Range(0, elites.Count);
            int rand_point_idx = Random.Range(0, spwan_points.Length);

            StartCoroutine(InstantiateEnemy(elites[rand_idx], spwan_points[rand_point_idx], 1, 0f));
        }
    }

    IEnumerator CreateNormalEnemy()
    {
        for (int i = 0; i < normals_time.Count; i++)
        {
            int rand_idx = Random.Range(0, normals.Count);
            int rand_point_idx = Random.Range(0, spwan_points.Length);

            GameObject enemy = normals[rand_idx];

            StartCoroutine(InstantiateEnemy(enemy, spwan_points[rand_point_idx], enemy.GetComponent<Mob_info>().instantiate_count, 0.5f));

            yield return new WaitForSeconds(normals_time[i]);
        }
    }

    IEnumerator InstantiateEnemy(GameObject enemy, GameObject point, int cnt, float interval_sec)
    {
        for (int i = 0; i < cnt; i++)
        {
            enemy.GetComponent<Transform>().position = point.GetComponent<Transform>().position;
            enemy.GetComponent<Mob_info>().instantiate_point = point.tag;
            enemy.layer = GV.ENEMY_LAYER;
            Camera.main.GetComponent<Ingame_manager>().enemys.Add(Instantiate(enemy));

            yield return new WaitForSeconds(interval_sec);
        }
    }
    IEnumerator CheckGameEnd(float check_rate) // 인게임의 종료조건을 계속 확인
    {
        while (true)
        {
            yield return new WaitForSeconds(check_rate);

            if (enemys.Count == 0)
            {
                DB_Manager.Instance.stage_clear = true;
                StopAllCoroutines();
                SceneManager.LoadScene("GameEnd");
            }
            else if (player_clone == null) // 사망
            {
                DB_Manager.Instance.stage_clear = false;
                StopAllCoroutines();
                SceneManager.LoadScene("GameEnd");
            }
        }
    }
}