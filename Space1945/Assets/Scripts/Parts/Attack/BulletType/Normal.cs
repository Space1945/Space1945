using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour
{
    // �׳� �ƹ��� ȿ���� ���� źȯ
    // �浹�� ������

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * GetComponent<BulletInfo>().speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) // ���� Mob info�� �ִ� �浹�� �Ѿ� ���� ���� �� bullet info�� �� �浹 ���� �浹ó���� ������
    {
        if (collision.gameObject.tag == "end_line")
            Destroy(gameObject);
    }
}
