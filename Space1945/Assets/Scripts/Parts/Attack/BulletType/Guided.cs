using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guided : MonoBehaviour
{
    public float radius;
    public float speed;
    float crash_damage;
    GameObject Target;
    Collider2D[] near_enemy;
    Rigidbody2D rigid;
    int rate = 0;

    // Start is called before the first frame update
    void Start()
    {
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up;
        Target = Find_Target();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target != null)
        {
            Vector2 dir = Target.transform.position - transform.position;

            rigid.AddForce(dir * (rate++), ForceMode2D.Force);

            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, speed); // �ӵ�����

            if (rigid.velocity.y != 0)
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -Mathf.Atan(rigid.velocity.x / rigid.velocity.y) * 180f / Mathf.PI));
        }
        else // Ÿ���� ������
            Target = Find_Target();

    }
    GameObject Find_Target()
    {
        float min_dis = 9999999;
        GameObject nearest = null;
        near_enemy = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Enemy")); // radius ���� ���� �ִ� ��� �� ��ü�� collider ����
        foreach (Collider2D col in near_enemy)
        {
            float dis = Vector2.Distance(transform.position, col.transform.position); // �Ÿ���
            if (min_dis > dis)
            {
                min_dis = dis;
                nearest = col.gameObject;
            }
        }
        return nearest;
    }

    void OnTriggerEnter2D(Collider2D col) // ���� Mob info�� �ִ� �浹�� �Ѿ� ���� ���� �� bullet info�� �� �浹 ���� �浹ó���� ������
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                Destroy(gameObject);
                break;
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                Destroy(gameObject);
                break;
        }
    }
}
