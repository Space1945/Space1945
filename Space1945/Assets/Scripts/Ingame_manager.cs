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
    public Image HealthBar; // 플레이어 체력바

    Coroutine addtional_gold_exp;
    float adtl_gold;
    float adtl_exp;

    ObjectPool ops;

    // Start is called before the first frame update
    void Awake()
    {
        ops = GetComponent<ObjectPool>();

        selected_chapter = DB_Manager.Instance.selected_chapter.ToString();
        selected_stage = DB_Manager.Instance.selected_stage.ToString();

        Initiate();

        Reinforce(); // 강화
        ReadStage();

        ops.MakePool();
    }
    void Start()
    {
        DB_Manager.Instance.InitStageDB();

        player = DB_Manager.Instance.using_airframe;
        player_clone = Instantiate(player); // 복제
        
        foreach (GameObject obj in BG) Instantiate(obj);

        StartCoroutine(CreateNormalEnemy());
        StartCoroutine(CheckGameEnd(check_rate));
    }

    void Initiate()
    {
        enemys = new HashSet<GameObject>();

        normals = new List<GameObject>();
        normals_time = new List<int>();
        elites = new List<GameObject>();

        addtional_gold_exp = null;
        Ultimate.enabled = false;
        ultimate_use = false;

        adtl_gold = 1f;
        adtl_exp = 1f;
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
    public void AddUltimateGuage(float ag) // 궁극기 게이지 추가
    {
        if (!ultimate_use)
        {
            var r = player_clone.GetComponent<AirframeScript>().reinforce_add;
            r["cg"] = (r["cg"] + ag) < r["mg"] ? r["cg"] + ag : r["mg"];

            UpdateUltimateBar(r["cg"], r["mg"]);

            if (r["cg"] >= r["mg"])
            {
                //특정 이미지 출력
                Ultimate.enabled = true;
            }
        }
    }
    public void UseUltimate() // 궁극기 사용
    {
        var r = player_clone.GetComponent<AirframeScript>().reinforce_add;
        r["cg"] = 0f;
        StartCoroutine(player_clone.GetComponent<UltimateInterface>().Ultimate());
        UpdateUltimateBar(0f, player_clone.GetComponent<AirframeScript>().max_guage);
        Ultimate.enabled = false;
    }

    public void UpdateUltimateBar(float cg, float mg)
    {
        Ultimate.GetComponent<Image>().fillAmount = cg / mg;
    }

    /*                 스코어, 경험치, 돈                  */
    public void KillEnemy(int score, int exp, int gold)
    {
        var ex_total = Camera.main.GetComponent<Ingame_manager>().ex_total;

        DB_Manager.Instance.score_earned += score;
        DB_Manager.Instance.exp_earned += (int)(exp * ex_total.ex_exp * adtl_exp);
        DB_Manager.Instance.gold_earned += (int)(gold * ex_total.ex_gold * adtl_gold);
        DB_Manager.Instance.enemy_killed_cnt++;

        if (DB_Manager.Instance.enemy_killed_cnt >= elite_emer_cnt) // 일반몹 10킬당 엘리트 한마리씩 출현
        {
            DB_Manager.Instance.enemy_killed_cnt = 0;
            // 엘리트 호출
            int rand_idx = Random.Range(0, elites.Count);
            int rand_point_idx = Random.Range(0, spwan_points.Length);

            StartCoroutine(InstantiateEnemy(elites[rand_idx] , spwan_points[rand_point_idx],1,0f));
        }
    }

    IEnumerator CreateNormalEnemy()
    {
        for (int i = 0; i < normals_time.Count; i++)
        {
            int rand_idx = Random.Range(0, normals.Count);
            int rand_point_idx = Random.Range(0, spwan_points.Length);
            int cnt = normals[rand_idx].GetComponent<Mob_info>().instantiate_count;
            StartCoroutine(InstantiateEnemy(normals[rand_idx], spwan_points[rand_point_idx], cnt , 0.5f));

            yield return new WaitForSeconds(normals_time[i]);
        }
    }

    IEnumerator InstantiateEnemy(GameObject enemy, GameObject point,int cnt, float interval_sec)
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
                ops.Free(); // 오브젝트 풀에 담긴 모든 객체 destroy
                SceneManager.LoadScene("GameEnd");
            }
            else if (player_clone == null) // 사망
            {
                DB_Manager.Instance.stage_clear = false;
                StopAllCoroutines();
                ops.Free(); // 오브젝트 풀에 담긴 모든 객체 destroy
                SceneManager.LoadScene("GameEnd");
            }
        }
    }
    IEnumerator AGE(float duration, float adtl_gold, float adtl_exp)
    {
        float origin_gold = this.adtl_gold;
        float origin_exp = this.adtl_exp;

        this.adtl_gold = adtl_gold;
        this.adtl_exp = adtl_exp;

        yield return new WaitForSeconds(duration);

        this.adtl_gold = origin_gold;
        this.adtl_exp = origin_exp;
    }
    public void AdditionalGoldExp(float duration, float adtl_gold, float adtl_exp)
    {
        addtional_gold_exp = StartCoroutine(AGE(duration, adtl_gold, adtl_exp));
    }

    IEnumerator UHPBar()
    {
        var afs = player_clone.GetComponent<AirframeScript>();

        while (true)
        {
            HealthBar.fillAmount = afs.reinforce_add["chp"] / afs.max_hp;

            yield return null;
        }
    }
    public void UpdatePlayersHPBar()
    {
        StartCoroutine(UHPBar());
    }
}