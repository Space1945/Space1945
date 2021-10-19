using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingShot : MonoBehaviour, UltimateInterface
{
    public float duration;

    IEnumerator UltimateInterface.Ultimate()
    {
        float fire_rate = GetComponent<AirframeScript>().fire_rate;
        int fire_cnt_per_shot = GetComponent<AirframeScript>().fire_cnt_per_shot;

        GetComponent<AirframeScript>().StopCoroutine(GetComponent<AirframeScript>().atk_coroutine);
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = true;
        Coroutine ultimate = StartCoroutine(GetComponent<AirframeScript>().Attack(fire_rate / 5, fire_cnt_per_shot * 2));
        yield return new WaitForSeconds(duration);
        StopCoroutine(ultimate);
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = false;
        StartCoroutine(GetComponent<AirframeScript>().Attack(fire_rate, fire_cnt_per_shot));
    }
}
