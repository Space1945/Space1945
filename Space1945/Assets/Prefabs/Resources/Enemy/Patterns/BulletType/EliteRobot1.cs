using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteRobot1 : MonoBehaviour
{
    EnemyAtkPatterns eaps;

    void Awake()
    {
        eaps = Camera.main.GetComponent<EnemyAtkPatterns>();
    }
    // Start is called before the first frame update
    void Start()
    {
        eaps.StartCoroutine(eaps.AttackSpread(gameObject));
    }
}
