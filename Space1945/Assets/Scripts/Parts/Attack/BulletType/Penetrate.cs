using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetrate : MonoBehaviour
{
    //���� źȯ
    //�浹�ص� ��� ����

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * GetComponent<BulletInfo>().speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "end_line")
            Destroy(gameObject);
    }
}
