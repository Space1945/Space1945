using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ingame_manager : MonoBehaviour
{
    public int elite_appear_score { get; set; } // 일정 점수를 획득하면 엘리트 몹 소환

    public GameObject[] spwan_points;

    public HashSet<GameObject> enemys { get; set; }

    int time = 0;
    int idx = 0;

    string selected_chapter;
    string selected_stage;
    List<GameObject> normals; // 출현할 일반몹
    List<int> normals_time; // 출현할 일반몹 시간 
    List<GameObject> elites; // 출현할 엘리트
    GameObject boss;

    GameObject[] BG;

    GameObject player;
    public GameObject player_clone { get; set; } // 어디서든지 플레이어 객체 접근 가능

    /*
     ***** player 궁극기 관리 *****
     */
    public Button Ultimate;
    float mUg; // 최대 궁극기 게이지 양
    float cUg; // 현재 궁극기 게이지 양

    public Image HealthBar; // 플레이어 체력바
   
    Vector2 player_point;
    
    // Start is called before the first frame update
    void Start() 
    {
        selected_chapter = DB_Manager.Instance.selected_chapter.ToString();
        selected_stage = DB_Manager.Instance.selected_stage.ToString();

        Initiate();
        ReadStage();

        player = DB_Manager.Instance.using_airframe;
        player_clone = Instantiate(player); // 복제
        player_clone.GetComponent<AirframeScript>().SetReady();
        
        mUg = 100; // 플레이어의 기체에 맞게 수정할것

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

    /*                 궁극기                  */
    public void Plus_ultimate_guage(float ultimate_guage) // 궁극기 게이지 추가
    {
        cUg = (cUg + ultimate_guage) < mUg ? cUg + ultimate_guage : mUg;

        Ultimate.GetComponent<Image>().fillAmount = (float)cUg / (float)mUg;

        if (cUg == 100)
        {
            //특정 이미지 출력
            Ultimate.enabled = true;
        }
    }

    void Use_ultimate() // 궁극기 사용
    {
        cUg = 0;
        Ultimate.GetComponent<Image>().fillAmount = 0;
        Ultimate.enabled = false;
    }
    /*                 체력바                  */
    public void Player_attacked() // 플레이어 피격판정 등 플레이어의 체력의 변동 발생
    {
        HealthBar.fillAmount = (float)player_clone.GetComponent<Player>().curHp / player_clone.GetComponent<AirframeScript>().maxHp;
    }
    /*                 스코어, 경험치, 돈                  */
    public void Add_point(int score,int exp,int gold)
    {/*
        DB_Manager.Instance.score_earned += score;
        elite_appear_score += score;
        if (elite_appear_score >= GV.Elite_Appear_Score) // 500점 당 엘리트 한마리씩 출현
        {
            elite_appear_score -= GV.Elite_Appear_Score;
            // 엘리트 호출
            Set_point(elite_mobs[0]);
            Fly f = gameObject.AddComponent<Fly>();
            f.Mobs_fly(start_point, player_point, rand, elite_mobs[0], 0, mob_position);
        }

        DB_Manager.Instance.exp_earned += exp;
        DB_Manager.Instance.gold_earned += gold;*/
    }
    public GameObject GetPlayer() // 포대 회전위해서 만들었는데 static 어케쓰는지 모르겠음 Butt 클래스에서 사용
    {
        return player_clone != null ? player_clone : null;
    }

    private void FixedUpdate() // 인게임의 종료조건을 계속 확인
    {/*
        if (player_clone == null) // 사망
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