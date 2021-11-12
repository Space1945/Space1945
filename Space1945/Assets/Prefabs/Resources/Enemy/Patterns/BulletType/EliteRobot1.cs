using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteRobot1 : MonoBehaviour
{
    Mob_info mis;
    ObjectPool ops;

    void Awake()
    {
        ops = Camera.main.GetComponent<ObjectPool>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mis = GetComponent<Mob_info>();
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        while (true)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                AttackSpread();
            }
            else if (rand == 1)
            {
                StartCoroutine(AttackRush());
            }

            yield return new WaitForSeconds(mis.fire_rate);
        }
    }

    public void AttackSpread()
    {
        for (int i = 0; i < mis.butts.Length; i++)
            for (int j = 0; j < 30; j++)
            {
                GameObject bullet_e = ops.GetBullet(mis.bullet);
                bullet_e.GetComponent<BulletInfo>().SetFromEnemy(j * 12);
                bullet_e.transform.position = mis.butts[i].position;
                bullet_e.GetComponent<NormalEnemy>().bullet_key = GetComponent<Mob_info>().bullet;
                bullet_e.SetActive(true);
            }
    }
    public IEnumerator AttackRush()
    {
        Ingame_manager ims = Camera.main.GetComponent<Ingame_manager>();

        Vector2 pos = ims.player_clone.transform.position;
        for (int j = 0; j < mis.burst_cnt; j++)
        {
            for (int i = 0; i < mis.butts.Length; i++)
            {
                GameObject bullet_e = ops.GetBullet(mis.bullet);
                bullet_e.GetComponent<BulletInfo>().SetFromEnemy(GV.GetDegree(mis.butts[i].position, pos));
                bullet_e.transform.position = mis.butts[i].position;
                bullet_e.GetComponent<NormalEnemy>().bullet_key = GetComponent<Mob_info>().bullet;
                bullet_e.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
