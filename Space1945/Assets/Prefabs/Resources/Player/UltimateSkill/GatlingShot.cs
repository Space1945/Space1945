using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingShot : MonoBehaviour, UltimateInterface
{
    public float duration;

    Transform[] butts;

    void Start()
    {
        butts = GetComponent<AirframeScript>().butts;
    }

    IEnumerator UltimateInterface.Ultimate()
    {
        float fire_rate = GetComponent<AirframeScript>().atk.GetComponent<AtkScript>().fire_rate;
        int fire_cnt_per_shot = GetComponent<AirframeScript>().atk.GetComponent<AtkScript>().fire_cnt_per_shot;

        GetComponent<AirframeScript>().atk.GetComponent<AtkScript>().StopAttackCoroutine();
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = true;
        GetComponent<AirframeScript>().atk.GetComponent<AtkScript>().StartAttackCoroutine(butts, fire_rate / 5, fire_cnt_per_shot * 2);
        yield return new WaitForSeconds(duration);
        GetComponent<AirframeScript>().atk.GetComponent<AtkScript>().StopAttackCoroutine();
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = false;
        GetComponent<AirframeScript>().atk.GetComponent<AtkScript>().StartAttackCoroutine(butts, fire_rate, fire_cnt_per_shot);
    }
}
