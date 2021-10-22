using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour, AtkInterface
{
    public int gold;
    public string explanation; // �ش� ��ü ����

    public GameObject bullet;
    public float _fire_rate;
    public int _fire_cnt_per_shot;
    public float fire_rate { get; set; }// �ʴ�
    public int fire_cnt_per_shot { get; set; } // �ѹ��� �߻��ϴ� źȯ��
    public float max_angle;
    public float min_angle;

    Coroutine atk_coroutine;

    void Start()
    {
        fire_rate = _fire_rate;
        fire_cnt_per_shot = _fire_cnt_per_shot;
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
                    Instantiate(bullet, butts[i].position, Quaternion.identity, transform).GetComponent<BulletInfo>().Set(Random.Range(min_angle, max_angle));

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
                    Instantiate(bullet, butts[i].position, Quaternion.identity, transform).GetComponent<BulletInfo>().Set(Random.Range(min_angle, max_angle));

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