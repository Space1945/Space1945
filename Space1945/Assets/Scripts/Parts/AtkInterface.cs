using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AtkInterface
{
    float fire_rate { get; set; }
    int fire_cnt_per_shot { get; set; }

    void StartAttackCoroutine();
    void StartAttackCoroutine(float fire_rate, int fire_cnt_per_shot);
    void StopAttackCoroutine();
}
