using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingShot : MonoBehaviour, UltimateInterface
{
    public float duration;

    IEnumerator UltimateInterface.Ultimate()
    {
        var atk_if = GetComponent<AirframeScript>().atk.GetComponent<AtkInterface>();
        var camera_im = Camera.main.GetComponent<Ingame_manager>();

        camera_im.ultimate_use = true;
        atk_if.TemporaryReinforce(duration, "fr", 0.3f);
        atk_if.TemporaryReinforce(duration, "fcps", 2f);
        yield return new WaitForSeconds(duration);
        camera_im.ultimate_use = false;
    }
}
