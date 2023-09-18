using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiTextValue;

    private void Start()
    {
        uiTextValue.text = soInt.Value.ToString();
    }

    private void Update()
    {
        uiTextValue.text = soInt.Value.ToString();
    }
}
