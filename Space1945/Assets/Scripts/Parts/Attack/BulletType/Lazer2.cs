using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer2 : MonoBehaviour
{
    float shot_angle;
    Vector2 normalized_angle;
    float speed;
    float crash_damage;

    void Start()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 4f);
        shot_angle = GetComponent<BulletInfo>().shot_angle;
        normalized_angle = GV.GetVector2(shot_angle).normalized;
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        transform.rotation = Quaternion.Euler(0, 0, shot_angle - 90);
        transform.parent = null;

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        transform.localScale = new Vector2(0f, 3f);

        yield return new WaitForSeconds(0.3f);

        float max_range = 0.2f;
        float range = 0f;
        while (range < max_range)
        {
            range += 0.02f;
            transform.localScale = new Vector2(range, 4f);

            yield return null;
        }
        yield return new WaitForSeconds(2f);
        while (range > 0)
        {
            range -= 0.02f;
            transform.localScale = new Vector2(range, 4f);

            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                break;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                break;
        }
    }
}
