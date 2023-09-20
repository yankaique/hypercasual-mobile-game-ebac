using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollectableBase
{
    [Header("Power Up Base")]
    public float powerUpDuration;

    protected override void OnCollect()
    {
        base.OnCollect();
        StartPowerUp();
    }

    protected virtual void StartPowerUp() {
        Invoke("EndPowerUp", powerUpDuration);
    }

    protected virtual void EndPowerUp() {}
}
