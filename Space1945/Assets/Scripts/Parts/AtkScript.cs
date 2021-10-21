using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkScript : MonoBehaviour
{
    public int gold;
    public string explanation; // 해당 기체 설명

    public GameObject bullet;
    public float fire_rate; // 초당
    public int fire_cnt_per_shot; // 한번에 발사하는 탄환수
    public float max_angle;
    public float min_angle;

    Coroutine atk_coroutine;

    IEnumerator Attack(Transform[] butts)
    {
        if (fire_rate < 0.05f) // 최대 공속
            fire_rate = 0.05f;

        while (true)
        {
            for (int i = 0; i < butts.Length; i++)
                for (int j = 0; j < fire_cnt_per_shot; j++)
                {
                    GameObject rbc = Instantiate(bullet, butts[i].position, Quaternion.identity, transform);
                    rbc.GetComponent<BulletInfo>().Reinforce(false);
                }

            yield return new WaitForSeconds(fire_rate);
        }
    }
    IEnumerator Attack(Transform[] butts, float fire_rate, float fire_cnt_per_shot)
    {
        if (fire_rate < 0.05f) // 최대 공속
            fire_rate = 0.05f;

        while (true)
        {
            for (int i = 0; i < butts.Length; i++)
                for (int j = 0; j < fire_cnt_per_shot; j++)
                {
                    GameObject rbc = Instantiate(bullet, butts[i].position, Quaternion.identity, transform);
                    rbc.GetComponent<BulletInfo>().Reinforce(false);
                }

            yield return new WaitForSeconds(fire_rate);
        }
    }

    public void StopAttackCoroutine()
    {
        StopCoroutine(atk_coroutine);
    }
    public void StartAttackCoroutine(Transform[] butts)
    {
        atk_coroutine = StartCoroutine(Attack(butts));
    }
    public void StartAttackCoroutine(Transform[] butts, float fire_rate, float fire_cnt_per_shot)
    {
        atk_coroutine = StartCoroutine(Attack(butts, fire_rate, fire_cnt_per_shot));
    }
}