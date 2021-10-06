using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    GameObject bullet_clone;
    Transform[] butt;
    float rate = 0;

    public GameObject bullet;
    public float fire_rate;

    // Start is called before the first frame update
    void Start()
    {
        butt = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dis;
        rate++;

        if (rate >= fire_rate)
        {
            foreach (Transform b in butt)
            {
                if (b.gameObject.tag != "enemy")
                {
                    bullet.transform.position = new Vector2(b.position.x, b.position.y);
                    bullet_clone = Instantiate(bullet);
                    dis = b.position - gameObject.transform.position;
                    bullet_clone.GetComponent<Rigidbody2D>().velocity = dis.normalized * bullet_clone.GetComponent<BulletInfo>().speed * Time.deltaTime;
                    bullet_clone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 + GV.radian_to_degree(GV.GetRadian(dis.x, dis.y))));
                }
            }
            rate = 0;
        }
    }
}
