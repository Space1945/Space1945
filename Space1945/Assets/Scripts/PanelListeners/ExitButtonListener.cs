using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonListener : MonoBehaviour
{
    public void Exit()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
