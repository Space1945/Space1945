using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallInterceptors : MonoBehaviour, UltimateInterface
{
    public GameObject interceptor;
    public int interceptor_cnt;
    public float multiple_damage_rate;
    public float ic_min_speed;
    public float ic_max_speed;
    public float create_interval_time;
    public float scale;

    IEnumerator UltimateInterface.Ultimate()
    {
        for (int i = 0; i < interceptor_cnt; i++)
        {
            Instantiate(interceptor).GetComponent<InterceptorScript>().Set(
                GetComponent<AirframeScript>().crash_damage * multiple_damage_rate,
                Random.Range(ic_min_speed, ic_max_speed),
                scale,
                new Vector2(Random.Range(-3f, 8f), -10)
            );

            yield return new WaitForSeconds(create_interval_time);
        }
    }
}
