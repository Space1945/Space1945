using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour, AtkInterface
{
    public int gold;
    public string explanation; // �ش� ��ü ����

    public GameObject bullet;
    public float fire_rate;
    public int fire_cnt_per_shot;

    // AtkInterface�� ����� ���� get�� ���� -----------------------
    public int _gold
    {
        get
        {
            return gold;
        }
    }
    // -----------------------------------------------------------

    public float max_angle;
    public float min_angle;

    Dictionary<string, float> reinforce_mul = new Dictionary<string, float>
    {
        { "fr", 1f },
        { "fcps", 1f },
        { "bd", 1f },
        { "cd", 1f }
    };
    Dictionary<string, float> reinforce_add = new Dictionary<string, float>
    {
        { "cc", 0f },
    };

    Transform[] butts;

    void Awake() // �ʱ�ȭ
    {
        butts = transform.parent.GetComponent<AirframeScript>().butts;
    }
    void Start()
    {
        if (fire_rate < 0.05f) // �ִ� ����
            fire_rate = 0.05f;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            for (int i = 0; i < butts.Length; i++)
                for (int j = 0; j < fire_cnt_per_shot * reinforce_mul["fcps"]; j++)
                    Instantiate(bullet, butts[i].position, Quaternion.identity).GetComponent<BulletInfo>().SetFromPlayer(Random.Range(min_angle, max_angle), reinforce_mul["bd"], reinforce_add["cc"], reinforce_mul["cd"]);

            yield return new WaitForSeconds(fire_rate * reinforce_mul["fr"]);
        }
    }

    IEnumerator TRMul(float duration, string name, float percentage)
    {
        if (reinforce_mul.ContainsKey(name))
        {
            reinforce_mul[name] *= percentage;

            if (fire_rate < 0.05f) // �ִ� ����
                fire_rate = 0.05f;

            yield return new WaitForSeconds(duration);

            reinforce_mul[name] /= percentage;
        }
    }
    void TRAdd(string name, float percentage)
    {
        if (reinforce_add.ContainsKey(name))
        {
            float adtl = reinforce_add["m" + name] * percentage;
            if (reinforce_add["c" + name] + adtl > reinforce_add["m" + name])
                reinforce_add["c" + name] = reinforce_add["m" + name];
            else
                reinforce_add["c" + name] += adtl;
        }
    }
    void AtkInterface.TemporaryReinforce(float duration, string name, float percentage)
    {
        StartCoroutine(TRMul(duration, name, percentage));
        TRAdd(name, percentage);
    }
}