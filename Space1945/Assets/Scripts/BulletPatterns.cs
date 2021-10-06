using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatterns : MonoBehaviour
{
    bool init = true;
    int pattern;
    GameObject Target;
    public void Straight_init()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * GetComponent<BulletInfo>().speed * Time.deltaTime;
        init = false;
    }
    public void Spread_init()
    {
        float shoot_angle = Random.Range(-5, 5);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -shoot_angle));
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sin(shoot_angle * Mathf.PI / 180f), Mathf.Cos(shoot_angle * Mathf.PI / 180f)).normalized * GetComponent<BulletInfo>().speed * Time.deltaTime;
        init = false;
    }
    public void Guided_init()
    {
        float min = 999999999;
        Vector2 bullet_p = transform.position;
        foreach (GameObject enemy in Camera.main.GetComponent<Ingame_manager>().Enemy)
            if (enemy != null)
            {
                Vector2 enemy_p = enemy.transform.position;
                float dis = Vector2.Distance(enemy_p, bullet_p);
                if (dis < min)
                {
                    Target = enemy;
                    min = dis;
                }
            }
        init = false;
    }
    public void Guided()
    {
        if (Target != null)
        {
            Vector2 dir = Target.transform.position - transform.position;

            GetComponent<Rigidbody2D>().velocity += dir.normalized;

            if (GetComponent<Rigidbody2D>().velocity.y != 0)
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -Mathf.Atan(GetComponent<Rigidbody2D>().velocity.x / GetComponent<Rigidbody2D>().velocity.y) * 180f / Mathf.PI));
        }
        else // 타겟이 없으면
        {
            if (GetComponent<Rigidbody2D>().velocity.x == 0 && GetComponent<Rigidbody2D>().velocity.y == 0)
                Destroy(this.gameObject);
            GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity, ForceMode2D.Force);
        }
    }
    public void Lazer()
    {
    }
    private void Start()
    {
//        pattern = GetComponent<AtkScript>().pattern;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (init)
        {
            switch (pattern)
            {
                case 0:
                    Straight_init();
                    break;
                case 1:
                    Spread_init();
                    break;
                case 2:
                    Guided_init();
                    break;
            }
        }
        switch (pattern)
        {
            case 2:
                Guided();
                break;
            case 3:
                Lazer();
                break;
        }
    }
}
