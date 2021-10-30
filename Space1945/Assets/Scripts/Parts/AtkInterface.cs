using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AtkInterface
{
    int _gold { get; }
    float _fire_rate { get; }
    int _fire_cnt_per_shot { get; }

    void TemporaryReinforce(float during_time, float new_fire_rate, int new_fire_cnt_per_shot, float adtl_crit_chance_p = 0f, float adtl_crit_damage_p = 0f);
}
