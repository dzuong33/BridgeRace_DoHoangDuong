using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickState : MonoBehaviour
{

    void Update()
    {
        if(gameObject.activeSelf == false)
        {
            Invoke(nameof(TurnOnBrickPrefabs), 10f);
        }
    }
    public void TurnOnBrickPrefabs()
    {
        gameObject.SetActive(true);
    }
}
