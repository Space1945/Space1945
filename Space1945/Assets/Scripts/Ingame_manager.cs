using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ingame_manager : MonoBehaviour
{
    public int elite_appear_score { get; set; } // 일정 점수를 획득하면 엘리트 몹 소환

    public GameObject[] spwan_points;

    public List<GameObject> Enemy { get; set; } = new List<GameObject>();
    public HashSet<GameObject> e = new HashSet<GameObject>();
    public int enemy_cnt { get; set; } // 적 수 생성할때 ++, 죽을때 --
    float time = 0;

    GameObject[] normals;
    GameObject elites;
    GameObject boss; // 보스 개체는 하나

    string selected_chapter;
    string selected_stage;

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

        string dir = "stage_" + selected_chapter + "_" + selected_stage;
        int mob_cnt = PlayerPrefs.GetString(dir + "_name").Length;

        normals = Resources.LoadAll<GameObject>("Enemy/Normal/" + PlayerPrefs.GetString(dir + "_type") + "/");
        elites = Resources.Load<GameObject>("Enemy/Elite/" + PlayerPrefs.GetString(dir + "_type") + "/Elite" + selected_chapter);
        if (PlayerPrefs.GetString("Chapter" + selected_chapter).Length == DB_Manager.Instance.selected_stage)
            boss = Resources.Load<GameObject>("Enemy/Boss/" + PlayerPrefs.GetString(dir + "_type") + "/Boss" + selected_chapter);

        BG = Resources.LoadAll<GameObject>("Maps/BG/");

        player = DB_Manager.Instance.using_airframe;
        player_clone = Instantiate(player); // 복제
        player_clone.GetComponent<AirframeScript>().SetReady();
        
        mUg = 100; // 플레이어의 기체에 맞게 수정할것

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
    void Mob_count() // 일반 몹
    {/*
        Set_point(normal_mobs[mob_list[index].mob_num - 1]);
        Fly f = gameObject.AddComponent<Fly>();
        f.Mobs_fly(start_point, player_point, rand, normal_mobs[mob_list[index].mob_num - 1], 0.5f, mob_position);
        index++;*/
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
    }
}
class Fly : MonoBehaviour // 하나의 군집 비행 -> 한 군집이므로 전부 비행 패턴이 동일해야함.
{
    Vector2 start;
    Vector2 player_point;
    int area;
    GameObject mob;

    int count = 0; // mob info에서 정해줌
    int pattern_number;
    float mob_position;
    int speed;

    public void Mobs_fly(Vector2 start_point, Vector2 player_point, int rand, GameObject mobs, float time, float mob_position) // 군집비행, 이 함수는 function() 호출
    {
        start = start_point;
        this.player_point = player_point;
        area = rand - 1;
        mob = Instantiate(mobs);
        mob.transform.position = start;
        this.mob_position = mob_position;

        // 몹마다 고유한 이동 패턴, 공격 패턴을 가지고 있음
//        int idx = Random.Range(0, mob.GetComponent<Mob_info>().pattern.Length);
//        pattern_number = mob.GetComponent<Mob_info>().pattern[idx];
//        speed = mob.GetComponent<Mob_info>().speed;
        for (int i = 0; i < mob.GetComponent<Mob_info>().instantiate_count; i++)
            Invoke("function", time * i);
    }
    public void function()
    {
        count++;
        GameObject mob_temp = Instantiate(mob); // 복제
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

        // 원본을 해당 개수만큼 복제했으면 삭제
        if (count == mob.GetComponent<Mob_info>().instantiate_count)
            Destroy(mob);
    }
}
