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

    Dictionary<string, float> reinforce = new Dictionary<string, float>
    {
        { "fr", 1f },
        { "fcps", 1f }
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
                for (int j = 0; j < fire_cnt_per_shot * reinforce["fcps"]; j++)
                    Instantiate(bullet, butts[i].position, Quaternion.identity, transform).GetComponent<BulletInfo>().SetFromPlayer(Random.Range(min_angle, max_angle));

            yield return new WaitForSeconds(fire_rate * reinforce["fr"]);
        }
    }

    IEnumerator TR(float duration, string name, float percentage)
    {
        if (reinforce.ContainsKey(name))
        {
            reinforce[name] *= percentage;

            if (fire_rate < 0.05f) // �ִ� ����
                fire_rate = 0.05f;

            yield return new WaitForSeconds(duration);

            reinforce[name] /= percentage;
        }
    }
    void AtkInterface.TemporaryReinforce(float duration, string name, float percentage)
    {
        StartCoroutine(TR(duration, name, percentage));
    }
}