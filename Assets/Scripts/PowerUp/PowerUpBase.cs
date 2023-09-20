using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollectableBase
{
    public float powerUpDuration;

    protected override void OnCollect()
    {
        base.OnCollect();
        StartPowerUp();
    }

    protected virtual void StartPowerUp() {
        Debug.Log("Start Power Up");
        Invoke("EndPowerUp", powerUpDuration);
    }

    protected virtual void EndPowerUp() { 
        Debug.Log("End Power Up");

    }
}
