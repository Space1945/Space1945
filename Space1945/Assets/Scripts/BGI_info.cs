using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGI_info : MonoBehaviour
{
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, -3);
    }


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BG_E_line")
            transform.position = new Vector3(0, transform.position.y + 60, 300);
    }
}
