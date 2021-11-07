using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    List<List<GameObject>> enemy_normal;
    List<List<GameObject>> enemy_elite;

    GameObject[] bulletenemy1;
    //GameObject[] bulletenemy2;
    //GameObject[] bulletenemy3;
    //GameObject[] bulletenemy4; // 후에 추가

    GameObject[] bulletplayer;
    GameObject[] bulletsub1; // 서브파츠용
    GameObject[] bulletsub2; // 서브파츠용

    GameObject[] enemydead;

    Ingame_manager ims;

    void Start()
    {
        ims = GetComponent<Ingame_manager>();

        bulletenemy1 = new GameObject[200];

        bulletplayer = new GameObject[500];
        bulletsub1 = new GameObject[100];
        bulletsub2 = new GameObject[100];

        enemydead = new GameObject[60];
    }
    // 몹
    public void Make_enemy_list(int normal_enemy_kind, int elite_enemy_kind) // 몹종류 수
    {
        enemy_normal = new List<List<GameObject>>();
        enemy_elite = new List<List<GameObject>>();

        for (int i =0;i< normal_enemy_kind; i++)
            enemy_normal.Add(new List<GameObject>());

        for (int i = 0; i < elite_enemy_kind; i++)
            enemy_elite.Add(new List<GameObject>());
    }
    public void Make_enemy_normal(ref List<GameObject> normal)
    {
        enemy_normal = new List<List<GameObject>>();
        for (int i = 0; i < normal.Count; i++)
        {
            enemy_normal.Add(new List<GameObject>());
            for (int j = 0; j < 50; i++)
            {
                enemy_normal[i][j] = Instantiate(normal[i]);
                enemy_normal[i][j].SetActive(false);
            }
        }
    }
    public void Make_enemy_elite(ref List<GameObject> elite)
    {
        enemy_elite = new List<List<GameObject>>();

        for (int i = 0; i < elite.Count; i++)
        {
            enemy_elite.Add(new List<GameObject>());
            for (int j = 0; j < elite.Count; i++)
            {
                enemy_elite[i][j] = Instantiate(elite[i]);
                enemy_elite[i][j].SetActive(false);
            }
        }
    }
    public void Make_bulletenemy1()
    {
        //총알
        for (int i = 0; i < bulletenemy1.Length; i++)
        {
            //bulletenemy1[i] = Instantiate(해당 맵에 해당되는 적이 발사하는 총알);
            bulletenemy1[i].SetActive(false);
        }
    }
    public void Make_bulletplayer(GameObject bullet)
    {
        for (int i = 0; i < bulletplayer.Length; i++)
        {
            bulletplayer[i] = Instantiate(bullet,new Vector2(-100,-100), Quaternion.identity);
            bulletplayer[i].SetActive(false);
        }
    }
    public void Make_bulletsub1()
    {
        for (int i = 0; i < bulletsub1.Length; i++)
        {
            //bulletsub1[i] = Instantiate(플레이어가 장착한 서브 파츠1의 총알);
            bulletsub1[i].SetActive(false);
        }
    }
    public void Make_bulletsub2()
    {
        for (int i = 0; i < bulletsub2.Length; i++)
        {
            //bulletsub2[i] = Instantiate(플레이어가 장착한 서브 파츠2의 총알);
            bulletsub2[i].SetActive(false);
        }
    }
    public void Make_enemydead()
    {
        for (int i = 0; i < enemydead.Length; i++)
        {
            //enemydead[i] = Instantiate(적 사망시 파티클);
            enemydead[i].SetActive(false);
        }
    }
    public GameObject GetPlayerBullet()
    {
        for (int i = 0; i < bulletplayer.Length; i++)
            if (!bulletplayer[i].activeSelf)
            {
                bulletplayer[i].SetActive(true);
                return bulletplayer[i];
            }
        return null;
    }
}
