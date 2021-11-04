using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingShot : MonoBehaviour, UltimateInterface
{
    public float duration;

    IEnumerator UltimateInterface.Ultimate()
    {
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = true;
        GetComponent<AirframeScript>().atk.GetComponent<AtkInterface>().TemporaryReinforce(duration, "fr", 0.3f);
        GetComponent<AirframeScript>().atk.GetComponent<AtkInterface>().TemporaryReinforce(duration, "fcps", 2f);
        yield return new WaitForSeconds(duration);
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = false;
    }
}
