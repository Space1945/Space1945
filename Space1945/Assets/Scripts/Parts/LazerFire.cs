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
    public float fire_rate;
    public int fire_cnt_per_shot;

    // AtkInterface에 선언된 변수 get만 가능 -----------------------
    public int _gold
    {
        get
        {
            return gold;
        }
    }
    // -----------------------------------------------------------

    Dictionary<string, float> reinforce = new Dictionary<string, float>
    {
        { "fr", 1f },
        { "fcps", 1f }
    };

    List<GameObject> lazers_start;
    List<GameObject> lazers_middle;
    List<GameObject> lazers_end;
    Coroutine atk_coroutine;

    Transform[] butts;
    float adtl_crit_chance_p;
    float adtl_crit_damage_p;

    void Awake()
    {
        butts = transform.parent.GetComponent<AirframeScript>().butts;
        lazers_start = new List<GameObject>();
        lazers_middle = new List<GameObject>();
        lazers_end = new List<GameObject>();
    }

    void Start()
    {
        atk_coroutine = StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        for (int i = 0; i < butts.Length; i++)
        {
            lazers_start.Add(Instantiate(start, butts[i].position, Quaternion.identity, butts[i]));
            for (int j = 0; j < fire_cnt_per_shot * reinforce["fcps"]; j++)
            {
                GameObject mc = Instantiate(middle, butts[i].position, Quaternion.identity, butts[i]);
                mc.GetComponent<Lazer>().Set(fire_rate * reinforce["fr"]);
                lazers_middle.Add(mc);
            }
            lazers_end.Add(Instantiate(end, new Vector2(1000, 1000), Quaternion.identity, butts[i]));
        }
        yield return null;
    }

    IEnumerator TR(float duration, string name, float percentage)
    {
        if (reinforce.ContainsKey(name))
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

            reinforce[name] *= percentage;

            atk_coroutine = StartCoroutine(Attack());

            yield return new WaitForSeconds(duration);

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

            reinforce[name] /= percentage;

            atk_coroutine = StartCoroutine(Attack());
        }
    }

    void AtkInterface.TemporaryReinforce(float duration, string name, float percentage)
    {
        StartCoroutine(TR(duration, name, percentage));
    }
}
