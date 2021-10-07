using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateThread : MonoBehaviour
{
    GameObject obj;
    int cnt;
    int interval;
    bool ready = false;
    int time = 0;
    int made_cnt = 0;

    void FixedUpdate()
    {
        if (ready)
        {
            time++;
            if (time > interval && made_cnt < cnt)
            {
                time = 0;
                made_cnt++;
                obj.transform.position = transform.position;
                Camera.main.GetComponent<Ingame_manager>().enemys.Add(Instantiate(obj));
            }

            if (made_cnt >= cnt)
            {
                ready = false;
                made_cnt = 0;
            }
        }
    }

    void MobAppear()
    {
        Camera.main.GetComponent<Ingame_manager>().enemys.Add(Instantiate(obj));
    }

    public void SetObject(GameObject obj, int interval)
    {
        this.obj = obj;
        this.obj.GetComponent<Mob_info>().instantiate_point = gameObject.tag;
        this.obj.layer = GV.ENEMY_LAYER;
        cnt = obj.GetComponent<Mob_info>().instantiate_count;
        this.interval = interval;
        ready = true;

        /*for (int i = 0; i < cnt; i++)
            Invoke("MobAppear", i * (interval / 50));*/
    }

    public bool Ready()
    {
        return ready;
    }
}
