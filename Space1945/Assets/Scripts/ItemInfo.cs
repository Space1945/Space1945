using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public float hp;
    public float ultimate_guage;
    public float speed;
    public int bound_count;

    Rigidbody2D rigid;
    CircleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        rigid.velocity = new Vector2(10, 10) * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision) // º®Ãæµ¹
    {
        if (collision.gameObject.tag == "game_line")
        {
            bound_count--;
            if (bound_count <= 0)
                this.gameObject.layer = 0;
        }
        else if (collision.gameObject.tag == "end_line")
            Destroy(this.gameObject);
        if (collision.gameObject.tag == "player")
        {
            float cHp = collision.gameObject.GetComponent<Player>().cur_hp;
 //           float mHp = collision.gameObject.GetComponent<Player>().maxHp;
 //           collision.gameObject.GetComponent<Player>().curHp = (cHp + hp) < mHp ? cHp + hp : mHp;
            Camera.main.GetComponent<Ingame_manager>().UpdatePlayersHP();
            Camera.main.GetComponent<Ingame_manager>().AddUltimateGuage(ultimate_guage);

            Destroy(this.gameObject);
        }
    }
}
