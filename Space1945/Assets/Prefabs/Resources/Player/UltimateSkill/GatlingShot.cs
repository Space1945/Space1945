using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingShot : MonoBehaviour, UltimateInterface
{
    public float duration;

    IEnumerator UltimateInterface.Ultimate()
    {
        float fire_rate = GetComponent<AirframeScript>().atk.GetComponent<AtkInterface>()._fire_rate;
        int fire_cnt_per_shot = GetComponent<AirframeScript>().atk.GetComponent<AtkInterface>()._fire_cnt_per_shot;

        Camera.main.GetComponent<Ingame_manager>().ultimate_use = true;
        GetComponent<AirframeScript>().atk.GetComponent<AtkInterface>().TemporaryReinforce(duration, fire_rate / 5, fire_cnt_per_shot * 2);
        yield return new WaitForSeconds(duration);
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = false;
    }
}
