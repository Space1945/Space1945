using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainHit : MonoBehaviour
{
    public float attack_rate;
    public int shocked_cnt;

    IEnumerator Attack(GameObject obj, float crash_damage)
    {
        for (int i = 0; i < shocked_cnt; i++)
        {
            if (obj == null)
                break;

            obj.GetComponent<Mob_info>().Attacked(crash_damage);

            yield return new WaitForSeconds(attack_rate);
        }

        Destroy(gameObject);
    }

    public void StartAttackCoroutine(GameObject obj, float crash_damage)
    {
        StartCoroutine(Attack(obj, crash_damage));
    }
}
