using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirframeScript : MonoBehaviour
{
    public Transform[] butts;
    public float max_hp;
    public float basic_def;
    public float crash_damage;
    public int gold;

    public GameObject atk
    {
        get
        {
            return atk_;
        }
    }
    public GameObject def
    {
        get
        {
            return def_;
        }
    }
    public GameObject sub_left
    {
        get
        {
            return sl_;
        }
    }
    public GameObject sub_right
    {
        get
        {
            return sr_;
        }
    }
    
    GameObject atk_;
    GameObject def_;
    GameObject sl_;
    GameObject sr_;

    void Awake()
    {
        atk_ = Instantiate(DB_Manager.Instance.using_atk, transform);
        atk_.GetComponent<SpriteRenderer>().sprite = null;
        def_ = Instantiate(DB_Manager.Instance.using_def, transform);
        def_.GetComponent<SpriteRenderer>().sprite = null;
        sl_ = Instantiate(DB_Manager.Instance.using_sub_left, transform);
        sl_.GetComponent<SpriteRenderer>().sprite = null;
        sr_ = Instantiate(DB_Manager.Instance.using_sub_right, transform);
        sr_.GetComponent<SpriteRenderer>().sprite = null;
    }
    void Start() // °­È­
    {
        max_hp *= 1 + DB_Manager.Instance.ex_total.ex_hp / 100f;
        basic_def *= 1 + DB_Manager.Instance.ex_total.ex_def / 100f;
        crash_damage *= 1 + DB_Manager.Instance.ex_total.ex_crash_dmg / 100f;
    }
}
