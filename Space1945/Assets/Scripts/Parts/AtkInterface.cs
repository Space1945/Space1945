using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AtkInterface
{
    int _gold { get; }

    void TemporaryReinforce(float duration, string name, float percentage);
}
