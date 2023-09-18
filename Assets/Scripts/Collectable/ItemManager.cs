using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.core.Singleton;
public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public TextMeshProUGUI uiTextCoins;

    private void Start()
    {
        Reset();

    }

    private void Reset()
    {
        coins.Value = 0;
    }

    public void AddCoins(int amount = 1)
    {
        Debug.Log(amount);
        coins.Value += amount;
    }
}
