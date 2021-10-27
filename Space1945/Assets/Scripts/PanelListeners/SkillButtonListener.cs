using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButtonListener : MonoBehaviour
{
    public int max_point;
    public int cur_point;

    public DB_Manager.ex_stats adds;

    public int pre_skill;
    public string skill_explain;
    public string skill_name;

    public bool skill_active = false;
}