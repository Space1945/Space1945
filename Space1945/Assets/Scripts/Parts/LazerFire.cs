using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerFire : MonoBehaviour, AtkInterface
{
    public int gold;
    public string explanation; // 해당 기체 설명

    public GameObject start;
    public GameObject middle;
    public GameObject end;
    public float _fire_rate;
    public int _fire_cnt_per_shot;
    public float fire_rate { get; set; }
    public int fire_cnt_per_shot { get; set; }

    List<GameObject> lazers_start;
    List<GameObject> lazers_middle;
    List<GameObject> lazers_end;
    Coroutine atk_coroutine;

    Transform[] butts;
    void Start()
    {
        fire_rate = _fire_rate;
        fire_cnt_per_shot = _fire_cnt_per_shot;
        butts = transform.parent.GetComponent<AirframeScript>().butts;
        lazers_start = new List<GameObject>();
        lazers_middle = new List<GameObject>();
        lazers_end = new List<GameObject>();

        atk_coroutine = StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        for (int i = 0; i < butts.Length; i++)
        {
            lazers_start.Add(Instantiate(start, butts[i].position, Quaternion.identity, butts[i]));
            for (int j = 0; j < fire_cnt_per_shot; j++)
            {
                GameObject mc = Instantiate(middle, butts[i].position, Quaternion.identity, butts[i]);
                mc.GetComponent<Lazer>().Set(fire_rate);
                lazers_middle.Add(mc);
            }
            lazers_end.Add(Instantiate(end, new Vector2(1000, 1000), Quaternion.identity, butts[i]));
        }
        yield return null;
    }
    IEnumerator Attack(float fire_rate, int fire_cnt_per_shot)
    {
        for (int i = 0; i < butts.Length; i++)
        {
            lazers_start.Add(Instantiate(start, butts[i].position, Quaternion.identity, butts[i]));
            for (int j = 0; j < fire_cnt_per_shot; j++)
            {
                GameObject mc = Instantiate(middle, butts[i].position, Quaternion.identity, butts[i]);
                mc.GetComponent<Lazer>().Set(fire_rate);
                lazers_middle.Add(mc);
            }
            lazers_end.Add(Instantiate(end, new Vector2(1000, 1000), Quaternion.identity, butts[i]));
        }
        yield return null;
    }

    void AtkInterface.StopAttackCoroutine()
    {
        for (int i = 0; i < butts.Length; i++)
        {
            Destroy(lazers_start[i]);
            Destroy(lazers_end[i]);
        }
        foreach (GameObject obj in lazers_middle)
            Destroy(obj);
        lazers_start.Clear();
        lazers_middle.Clear();
        lazers_end.Clear();
        StopCoroutine(atk_coroutine);
    }
    void AtkInterface.StartAttackCoroutine()
    {
        atk_coroutine = StartCoroutine(Attack());
    }
    void AtkInterface.StartAttackCoroutine(float fire_rate, int fire_cnt_per_shot)
    {
        atk_coroutine = StartCoroutine(Attack(fire_rate, fire_cnt_per_shot));
    }
}
