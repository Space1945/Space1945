using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkScript : MonoBehaviour
{
    public int gold;
    public string explanation; // �ش� ��ü ����

    public GameObject bullet;
    public float fire_rate; // �ʴ�
    public int fire_cnt_per_shot; // �ѹ��� �߻��ϴ� źȯ��
    public float max_angle;
    public float min_angle;

    Coroutine atk_coroutine;

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
                    Instantiate(bullet, butts[i].position, Quaternion.identity, transform);

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
                    Instantiate(bullet, butts[i].position, Quaternion.identity, transform);

            yield return new WaitForSeconds(fire_rate);
        }
    }

    public void StopAttackCoroutine()
    {
        StopCoroutine(atk_coroutine);
    }
    public void StartAttackCoroutine()
    {
        atk_coroutine = StartCoroutine(Attack());
    }
    public void StartAttackCoroutine(float fire_rate, float fire_cnt_per_shot)
    {
        atk_coroutine = StartCoroutine(Attack(fire_rate, fire_cnt_per_shot));
    }
}