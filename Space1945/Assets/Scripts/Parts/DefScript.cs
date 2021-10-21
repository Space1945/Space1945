using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefScript : MonoBehaviour
{
    public int gold;
    public float add_hp;
    public float add_def;

    void Start()
    {
        DB_Manager.Instance.ex_total.ex_hp += add_hp;
        DB_Manager.Instance.ex_total.ex_def += add_def;
    }
}
