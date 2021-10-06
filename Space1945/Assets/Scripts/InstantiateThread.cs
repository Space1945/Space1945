using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateThread : MonoBehaviour
{
    GameObject obj;
    int cnt;
    int interval;
    bool ready = false;
    bool creating = false;
    int time = 0;
    int made_cnt = 0;

    void FixedUpdate()
    {
        time++;

        if (cnt < made_cnt)
        {
            ready = false;
            made_cnt = 0;
            time = 0;
            creating = false;
        }

        if (ready && time > interval)
        {
            Camera.main.GetComponent<Ingame_manager>().enemys.Add(obj);
            Instantiate(obj);
            made_cnt++;
            time = 0;
            creating = true;
        }
    }

    public void SetObject(GameObject obj, int cnt, int interval)
    {
        this.obj = obj;
        this.cnt = cnt;
        this.interval = interval;
        ready = true;
    }

    public bool Creating()
    {
        return creating;
    }
}
