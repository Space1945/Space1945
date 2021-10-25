using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DB_Manager
{
    private static DB_Manager DBM = null; // Instance 함수로만 참조 가능

    // 모든 프리팹
    public GameObject[] airframes { get; set; }
    public GameObject[] atks { get; set; }
    public GameObject[] defs { get; set; }
    public GameObject[] subs { get; set; }
    GameObject[] empty { get; set; }

    // 사용 중인 장비
    public GameObject using_airframe;
    public GameObject using_atk;
    public GameObject using_def;
    public GameObject using_sub_left;
    public GameObject using_sub_right;

    // 해금은 됐지만 사용하고 있지 않은 장비
    public List<GameObject> unlocked_airframes;
    public List<GameObject> unlocked_atks;
    public List<GameObject> unlocked_defs;
    public List<GameObject> unlocked_subs;

    // 해금 안된 장비
    public List<GameObject> locked_airframes;
    public List<GameObject> locked_atks;
    public List<GameObject> locked_defs;
    public List<GameObject> locked_subs;

    public int total_prefab_cnt { get; set; }
    public int score_earned { get; set; }
    public int exp_earned { get; set; }
    public int gold_earned { get; set; }
    public int enemy_killed_cnt { get; set; }
    public bool stage_clear { get; set; }

    public int selected_chapter { get; set; } // 현재 선택한 챕터
    public int selected_stage { get; set; }

    [Serializable]
    public struct ex_stats
    {
        public float ex_hp;
        public float ex_def;
        public float ex_crash_dmg;
        public float ex_bullet_dmg;
        public float ex_fire_rate;
        public float ex_crit_chance;
        public float ex_crit_dmg;
        public float ex_gold;
        public float ex_exp;
        public float ex_drop;
    }
    public ex_stats parts_total, research_total;

    public ex_stats InitReinforce()
    {
        ex_stats temp = new ex_stats();
        temp.ex_bullet_dmg = parts_total.ex_bullet_dmg + research_total.ex_bullet_dmg;
        temp.ex_crash_dmg = parts_total.ex_crash_dmg + research_total.ex_crash_dmg;
        temp.ex_crit_chance = parts_total.ex_crit_chance + research_total.ex_crit_chance;
        temp.ex_crit_dmg = parts_total.ex_crit_dmg + research_total.ex_crit_dmg;
        temp.ex_def = parts_total.ex_def + research_total.ex_def;
        temp.ex_drop = parts_total.ex_drop + research_total.ex_drop;
        temp.ex_exp = parts_total.ex_exp + research_total.ex_exp;
        temp.ex_fire_rate = parts_total.ex_fire_rate + research_total.ex_fire_rate;
        temp.ex_gold = parts_total.ex_gold + research_total.ex_gold;
        temp.ex_hp = parts_total.ex_hp + research_total.ex_hp;
        return temp;
    }
    public void parts_status_init()
    {
        if (using_def == null)
            using_def = empty[0];
        if (using_sub_left == null)
            using_sub_left = empty[1];
        if (using_sub_right == null)
            using_sub_right = empty[1];
        parts_total.ex_bullet_dmg = using_sub_left.GetComponent<SubScript>().adds.ex_bullet_dmg + using_sub_right.GetComponent<SubScript>().adds.ex_bullet_dmg;
        parts_total.ex_crash_dmg = using_sub_left.GetComponent<SubScript>().adds.ex_crash_dmg + using_sub_right.GetComponent<SubScript>().adds.ex_crash_dmg + using_def.GetComponent<DefScript>().ex_crash_dmg;
        parts_total.ex_crit_chance = using_sub_left.GetComponent<SubScript>().adds.ex_crit_chance + using_sub_right.GetComponent<SubScript>().adds.ex_crit_chance;
        parts_total.ex_crit_dmg = using_sub_left.GetComponent<SubScript>().adds.ex_crit_dmg + using_sub_right.GetComponent<SubScript>().adds.ex_crit_dmg;
        parts_total.ex_def = using_sub_left.GetComponent<SubScript>().adds.ex_def + using_sub_right.GetComponent<SubScript>().adds.ex_def + using_def.GetComponent<DefScript>().ex_def;
        parts_total.ex_drop = using_sub_left.GetComponent<SubScript>().adds.ex_drop + using_sub_right.GetComponent<SubScript>().adds.ex_drop;
        parts_total.ex_exp = using_sub_left.GetComponent<SubScript>().adds.ex_exp + using_sub_right.GetComponent<SubScript>().adds.ex_exp;
        parts_total.ex_fire_rate = using_sub_left.GetComponent<SubScript>().adds.ex_fire_rate + using_sub_right.GetComponent<SubScript>().adds.ex_fire_rate;
        parts_total.ex_gold = using_sub_left.GetComponent<SubScript>().adds.ex_gold + using_sub_right.GetComponent<SubScript>().adds.ex_gold;
        parts_total.ex_hp = using_sub_left.GetComponent<SubScript>().adds.ex_hp + using_sub_right.GetComponent<SubScript>().adds.ex_hp + using_def.GetComponent<DefScript>().ex_hp;
    }

    public void First_Start()
    {
    }
    DB_Manager()
    {
        LoadAllParts();
        parts_total = new ex_stats();
        research_total = new ex_stats();
    }

    public static DB_Manager Instance
    {
        get
        {
            if (DBM == null)
                DBM = new DB_Manager();
            return DBM;
        }
    }

    public void InitStageDB()
    {
        score_earned = 0;
        exp_earned = 0;
        gold_earned = 0;
        enemy_killed_cnt = 0;
        stage_clear = false;
    }
    private void LoadAllParts()
    {
        airframes = Resources.LoadAll<GameObject>("Player/Airframes/");
        atks = Resources.LoadAll<GameObject>("Player/AtkParts/");
        defs = Resources.LoadAll<GameObject>("Player/DefParts/");
        subs = Resources.LoadAll<GameObject>("Player/SubParts/");
        empty = Resources.LoadAll<GameObject>("Player/EmptyParts/");

        using_airframe = null;
        using_atk = null;
        using_def = null;
        using_sub_left = null;
        using_sub_right = null;

        unlocked_airframes = new List<GameObject>();
        unlocked_atks = new List<GameObject>();
        unlocked_defs = new List<GameObject>();
        unlocked_subs = new List<GameObject>();
        
        locked_airframes = new List<GameObject>();
        locked_atks = new List<GameObject>();
        locked_defs = new List<GameObject>();
        locked_subs = new List<GameObject>();

        for (int i = 0; i < airframes.Length; i++)
        {
            if (!PlayerPrefs.HasKey(airframes[i].name))
                PlayerPrefs.SetString(airframes[i].name, "locked");

            string info = PlayerPrefs.GetString(airframes[i].name);
            if (info == "using")
                using_airframe = airframes[i];
            else if (info == "unlocked")
                unlocked_airframes.Add(airframes[i]);
            else
                locked_airframes.Add(airframes[i]);
        }
        for (int i = 0; i < atks.Length; i++)
        {
            if (!PlayerPrefs.HasKey(atks[i].name))
                PlayerPrefs.SetString(atks[i].name, "locked");

            string info = PlayerPrefs.GetString(atks[i].name);
            if (info == "using")
                using_atk = atks[i];
            else if (info == "unlocked")
                unlocked_atks.Add(atks[i]);
            else
                locked_atks.Add(atks[i]);
        }
        for (int i = 0; i < defs.Length; i++)
        {
            if (!PlayerPrefs.HasKey(defs[i].name))
                PlayerPrefs.SetString(defs[i].name, "locked");

            string info = PlayerPrefs.GetString(defs[i].name);
            if (info == "using")
                using_def = defs[i];
            else if (info == "unlocked")
                unlocked_defs.Add(defs[i]);
            else
                locked_defs.Add(defs[i]);
        }
        for (int i = 0; i < subs.Length; i++)
        {
            if (!PlayerPrefs.HasKey(subs[i].name))
                PlayerPrefs.SetString(subs[i].name, "locked");

            string info = PlayerPrefs.GetString(subs[i].name);
            if (info == "using_left")
                using_sub_left = subs[i];
            else if (info == "using_right")
                using_sub_right = subs[i];
            else if (info == "unlocked")
                unlocked_subs.Add(subs[i]);
            else
                locked_subs.Add(subs[i]);
        }

        //parts_status_init();

        total_prefab_cnt = airframes.Length + atks.Length + defs.Length + subs.Length;
    }
}
