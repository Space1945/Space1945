using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chain : MonoBehaviour
{
    public float radius;
    public int chain_cnt; // 튕기는 횟수
    public GameObject middle;
    public GameObject hit;

    float shot_angle;
    float crash_damage;
    float distance; // 목표까지의 거리
    HashSet<GameObject> shocked_enemies;

    void Awake()
    {
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        shocked_enemies = new HashSet<GameObject>();
    }
    void Start()
    {
        StartCoroutine(ShotLightning());
    }

    IEnumerator ShotLightning() // 시작점, 남은 튕김횟수
    {
        GameObject target = gameObject;
        List<GameObject> enemies = new List<GameObject>();
        List<GameObject> middles = new List<GameObject>();

        for (int i = 0; i < chain_cnt; i++)
        {
            // 타겟 찾음
            float min_dis = 9999999;
            GameObject nearest = null;
            if (target == null)
                break;
            Collider2D[] near_enemy = Physics2D.OverlapCircleAll(target.transform.position, radius, LayerMask.GetMask("Enemy")); // radius 범위 내에 있는 모든 적 기체의 collider 수집

            foreach (Collider2D col in near_enemy)
                if (col.gameObject != target && !shocked_enemies.Contains(col.gameObject))
                {
                    float dis = Vector2.Distance(target.transform.position, col.transform.position); // 거리비교
                    if (min_dis > dis)
                    {
                        min_dis = dis;
                        nearest = col.gameObject;
                    }
                }
            if (nearest == null)
                break;
            shocked_enemies.Add(nearest);
            enemies.Add(nearest);

            distance = Vector2.Distance(target.transform.position, nearest.transform.position);
            shot_angle = GV.GetDegree(target.transform.position, nearest.transform.position);

            for (int j = 0; j < distance; j++)
                middles.Add(Instantiate(middle, (Vector2)target.transform.position + GV.GetVector2(shot_angle).normalized * (j + 0.5f), Quaternion.Euler(0, 0, shot_angle), transform));

            yield return new WaitForSeconds(0.05f);

            for (int j = 0; j < middles.Count; j++)
                Destroy(middles[j]);
            middles.Clear();

            target = nearest;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                GameObject hc = Instantiate(hit, enemies[i].transform.position, Quaternion.identity, enemies[i].transform);
                hc.GetComponent<ChainHit>().StartAttackCoroutine(enemies[i], crash_damage);
            }
        }
    }
}
