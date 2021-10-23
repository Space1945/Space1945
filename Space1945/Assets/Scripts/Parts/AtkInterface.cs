using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AtkInterface
{
    int _gold { get; }
    float _fire_rate { get; }
    int _fire_cnt_per_shot { get; }

    void StartAttackCoroutine();
    void StartAttackCoroutine(float fire_rate, int fire_cnt_per_shot);
    void StopAttackCoroutine();
}
