using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float max_range; // ���� �ִ� ����
    public float delta_range; // �����Ӵ� Ŀ���� ũ��

    float boom_damage; //�浹 ������
    float range; // ���� ���� ����
    HashSet<int> collided;

    void Start()
    {
        boom_damage = GetComponent<BulletInfo>().crash_damage;
        range = 0;
        collided = new HashSet<int>();
        transform.parent = null;

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (range < max_range)
        {
            range += delta_range;
            transform.localScale = new Vector2(range, range); //���� ������ ����

            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!collided.Contains(col.GetInstanceID()))
        {
            switch (col.gameObject.tag)
            {
                case "enemy":
                    collided.Add(col.GetInstanceID());
                    if (range <= max_range / 2)
                        col.gameObject.GetComponent<Mob_info>().Attacked(boom_damage);
                    else
                        col.gameObject.GetComponent<Mob_info>().Attacked(boom_damage / 2);
                    break;
            }
        }
    }
}
