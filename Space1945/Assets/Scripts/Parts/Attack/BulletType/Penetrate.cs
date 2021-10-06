using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetrate : MonoBehaviour
{
    //관통 탄환
    //충돌해도 계속 직진

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
