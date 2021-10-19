using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEnergySphere : MonoBehaviour, UltimateInterface
{
    public GameObject es;

    IEnumerator UltimateInterface.Ultimate()
    {
        GameObject clone = Instantiate(es);
        clone.transform.position = new Vector2(0, -10);
        clone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);

        yield return null;
    }
}
