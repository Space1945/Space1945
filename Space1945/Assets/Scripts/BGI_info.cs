using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGI_info : MonoBehaviour
{
    
    public float speed;
    public Sprite[] sprite;

    void Start()
    {
        Call_BG();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

    void Call_BG()
    {
        GetComponent<SpriteRenderer>().sprite = sprite[0];
    }


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BG_E_line")
        {
            transform.position = new Vector2(0, 22.4f);
        }
    }
}
