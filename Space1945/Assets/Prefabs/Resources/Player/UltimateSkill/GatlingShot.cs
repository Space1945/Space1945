using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingShot : MonoBehaviour, UltimateInterface
{
    public float duration;

    IEnumerator UltimateInterface.Ultimate()
    {
        float bullet_damage = GetComponent<AirframeScript>().bullet_damage;
        float fire_rate = GetComponent<AirframeScript>().fire_rate;
        int fire_cnt_per_shot = GetComponent<AirframeScript>().fire_cnt_per_shot;

        GetComponent<AirframeScript>().StopAttackCoroutine();
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = true;
        GetComponent<AirframeScript>().StartAttackCoroutine(bullet_damage, fire_rate / 5, fire_cnt_per_shot * 2);
        yield return new WaitForSeconds(duration);
        GetComponent<AirframeScript>().StopAttackCoroutine();
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = false;
        GetComponent<AirframeScript>().StartAttackCoroutine(bullet_damage, fire_rate, fire_cnt_per_shot);
    }
}
