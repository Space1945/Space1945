using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEnergySphere : MonoBehaviour, UltimateInterface
{
    public GameObject es;
    public float during_time;

    IEnumerator UltimateInterface.Ultimate()
    {
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = true;
        GameObject esc = Instantiate(es);

        yield return new WaitForSeconds(during_time);

        Destroy(esc);
        Camera.main.GetComponent<Ingame_manager>().ultimate_use = false;
    }
}
