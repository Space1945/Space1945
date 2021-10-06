using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_delete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Delete", 0.5f);
    }
    void Delete()
    {
        ParticleSystem par = this.GetComponent<ParticleSystem>();
        Destroy(this.gameObject);
    }
}
