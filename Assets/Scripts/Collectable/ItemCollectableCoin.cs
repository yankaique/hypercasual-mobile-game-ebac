using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    public Collider collider;
    public float lerp = 5f;
    public float minDistance = 1f;
    private bool _collect = false;

    private void Start()
    {
        CoinsAnimationManager.Instance.RegisterCoin(this);
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        //ItemManager.Instance.AddCoins();
        collider.enabled = false;
        _collect = true;
    }

    protected override void Collect()
    {
        OnCollect();
    }

    private void Update()
    {
        if(_collect)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerp * Time.deltaTime);
            if(Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {
                HideItems();
                Destroy(gameObject);
            }
        }
    }
}
