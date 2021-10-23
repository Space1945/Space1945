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
    public float _fire_rate // �ʴ�
    {
        get
        {
            return fire_rate;
        }
    }
    public int _fire_cnt_per_shot // �ѹ��� �߻��ϴ� źȯ��
    {
        get
        {
            return fire_cnt_per_shot;
        }
    }
    // -----------------------------------------------------------

    public float max_angle;
    public float min_angle;

    Coroutine atk_coroutine;

    void Awake() // �ʱ�ȭ
    {

    }
    void Start()
    {
        atk_coroutine = StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        Transform[] butts = transform.parent.GetComponent<AirframeScript>().butts;

        if (fire_rate < 0.05f) // �ִ� ����
            fire_rate = 0.05f;

        while (true)
        {
            for (int i = 0; i < butts.Length; i++)
                for (int j = 0; j < fire_cnt_per_shot; j++)
                    Instantiate(bullet, butts[i].position, Quaternion.identity, transform).GetComponent<BulletInfo>().SetFromPlayer(Random.Range(min_angle, max_angle));

            yield return new WaitForSeconds(fire_rate);
        }
    }
    IEnumerator Attack(float fire_rate, float fire_cnt_per_shot)
    {
        Transform[] butts = transform.parent.GetComponent<AirframeScript>().butts;

        if (fire_rate < 0.05f) // �ִ� ����
            fire_rate = 0.05f;

        while (true)
        {
            for (int i = 0; i < butts.Length; i++)
                for (int j = 0; j < fire_cnt_per_shot; j++)
                    Instantiate(bullet, butts[i].position, Quaternion.identity, transform).GetComponent<BulletInfo>().SetFromPlayer(Random.Range(min_angle, max_angle));

            yield return new WaitForSeconds(fire_rate);
        }
    }

    void AtkInterface.StopAttackCoroutine()
    {
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