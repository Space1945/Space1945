using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public float radius;
    public int chain_cnt; // 튕기는 횟수
    public GameObject middle;
    public GameObject hit;

    float shot_angle;
    float crash_damage;
    float distance; // 목표까지의 거리
    Collider2D[] near_enemy; // 주변 적들의 집합

    private void Start()
    {
        crash_damage = GetComponent<BulletInfo>().crash_damage;

        Shot_lightning(gameObject, chain_cnt);

        Invoke("Delete", 0.6f);
    }

    void Delete()
    {
        Destroy(gameObject);
    }
    void Shot_lightning(GameObject start, int chain_cnt) // 시작점, 남은 튕김횟수
    {
        if (chain_cnt == 0) return;

        float min_dis = 9999999;
        GameObject nearest = null;
        near_enemy = Physics2D.OverlapCircleAll(start.transform.position, radius, LayerMask.GetMask("Enemy")); // radius 범위 내에 있는 모든 적 기체의 collider 수집
        foreach (Collider2D col in near_enemy)
        {
            if (col.gameObject != start) // 방금 맞았던 놈 어케 무시? 싯팔 모르겠다
            {
                float dis = Vector2.Distance(start.transform.position, col.transform.position); // 거리비교
                if (min_dis > dis)
                {
                    min_dis = dis;
                    nearest = col.gameObject;
                }
            }
        }
        if (nearest != null)
        {
            distance = Vector2.Distance(start.transform.position, nearest.transform.position);
            shot_angle = GV.GetDegree(start.transform.position, nearest.transform.position);

            for (int i = 0; i < distance; i++)
                Instantiate(middle, (Vector2)start.transform.position + GV.GetVector2(shot_angle).normalized * (i + 0.5f), Quaternion.Euler(0, 0, shot_angle), transform);

            Instantiate(hit, nearest.transform.position, Quaternion.Euler(0, 0, 0), nearest.transform);

            Shot_lightning(nearest, chain_cnt - 1);
        }
    }
}
