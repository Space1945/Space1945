using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chain : MonoBehaviour
{
    public float radius;
    public int chain_cnt; // ƨ��� Ƚ��
    public GameObject middle;
    public GameObject hit;

    float shot_angle;
    float crash_damage;
    float distance; // ��ǥ������ �Ÿ�
    HashSet<GameObject> shocked_enemies;

    ObjectPool ops;
    void Awake()
    {
        ops = Camera.main.GetComponent<ObjectPool>();
    }
    void Start()
    {
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        shocked_enemies = new HashSet<GameObject>();

        StartCoroutine(ShotLightning());
    }

    IEnumerator ShotLightning() // ������, ���� ƨ��Ƚ��
    {
        GameObject target = gameObject;
        List<GameObject> enemies = new List<GameObject>();
        List<GameObject> middles = new List<GameObject>();

        for (int i = 0; i < chain_cnt; i++)
        {
            // Ÿ�� ã��
            float min_dis = 9999999;
            GameObject nearest = null;
            if (target == null)
                break;
            Collider2D[] near_enemy = Physics2D.OverlapCircleAll(target.transform.position, radius, LayerMask.GetMask("Enemy")); // radius ���� ���� �ִ� ��� �� ��ü�� collider ����

            foreach (Collider2D col in near_enemy)
                if (col.gameObject != target && !shocked_enemies.Contains(col.gameObject))
                {
                    float dis = Vector2.Distance(target.transform.position, col.transform.position); // �Ÿ���
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
            {
                GameObject middle_clone = ops.GetBullet(middle);
                middle_clone.transform.position = (Vector2)target.transform.position + GV.GetVector2(shot_angle).normalized * (j + 0.5f);
                middle_clone.transform.rotation = Quaternion.Euler(0, 0, shot_angle);
                middle_clone.transform.parent = transform;
                middles.Add(middle_clone);
                middle_clone.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);

            for (int j = 0; j < middles.Count; j++)
                ops.ReturnBullet(middle, middles[j]);
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
