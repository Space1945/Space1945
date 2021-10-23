using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallInterceptors : MonoBehaviour, UltimateInterface
{
    public GameObject interceptor;
    public int interceptor_cnt;
    public float ic_min_speed;
    public float ic_max_speed;
    public float create_interval_time;
    public float scale;

    IEnumerator UltimateInterface.Ultimate()
    {
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = true;
        for (int i = 0; i < interceptor_cnt; i++)
        {
            Instantiate(interceptor).GetComponent<InterceptorScript>().Set(
                Random.Range(ic_min_speed, ic_max_speed),
                scale,
                new Vector2(Random.Range(-3f, 3f), -10)
            );

            yield return new WaitForSeconds(create_interval_time);
        }
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = false;
    }
}
