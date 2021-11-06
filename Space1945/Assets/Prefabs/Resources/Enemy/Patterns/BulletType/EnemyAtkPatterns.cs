using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkPatterns : MonoBehaviour
{
    public IEnumerator Attack(GameObject obj)
    {
        Mob_info mis = obj.GetComponent<Mob_info>();

        while (true)
        {
            for (int i = 0; i < mis.butts.Length; i++)
                Instantiate(mis.bullet, mis.butts[i].position, Quaternion.identity).GetComponent<BulletInfo>().SetFromEnemy(GV.GetDegree(transform.position, mis.butts[i].position));

            yield return new WaitForSeconds(mis.fire_rate);
        }
    }
    public IEnumerator AttackSpread(GameObject obj)
    {
        Mob_info mis = obj.GetComponent<Mob_info>();

        while (true)
        {
            for (int i = 0; i < mis.butts.Length; i++)
                for (int j = 0; j < 30; j++)
                    Instantiate(mis.bullet, mis.butts[i].position, Quaternion.identity).GetComponent<BulletInfo>().SetFromEnemy(j * 12); // spread

            yield return new WaitForSeconds(mis.fire_rate);
        }
    }
    public IEnumerator AttackRush(GameObject obj)
    {
        Mob_info mis = obj.GetComponent<Mob_info>();
        Ingame_manager ims = Camera.main.GetComponent<Ingame_manager>();

        while (true)
        {
            Vector2 pos = ims.player_clone.transform.position;
            for (int j = 0; j < mis.burst_cnt; j++)
            {
                for (int i = 0; i < mis.butts.Length; i++)
                    Instantiate(mis.bullet, mis.butts[i].position, Quaternion.identity).GetComponent<BulletInfo>().SetFromEnemy(GV.GetDegree(mis.butts[i].position, pos));
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(mis.fire_rate);
        }
    }
   public  IEnumerator AttackWave(GameObject obj)
    {
        Mob_info mis = obj.GetComponent<Mob_info>();
        //225 ~ 315
        while (true)
        {
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < mis.butts.Length; k++)
                        Instantiate(mis.bullet, mis.butts[k].position, Quaternion.identity).GetComponent<BulletInfo>().SetFromEnemy(225 + j * 10);
                    yield return new WaitForSeconds(0.1f);
                }
                for (int j = 9; j >= 0; j--)
                {
                    for (int k = 0; k < mis.butts.Length; k++)
                        Instantiate(mis.bullet, mis.butts[k].position, Quaternion.identity).GetComponent<BulletInfo>().SetFromEnemy(315 - j * 10);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(mis.fire_rate);
        }
    }
}
