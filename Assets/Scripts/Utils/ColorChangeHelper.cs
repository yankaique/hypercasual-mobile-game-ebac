using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChangeHelper : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Color startColor = Color.white;

    public float duration = .2f;
    public float delay = .5f;

    private Color _startColor;

    private void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _startColor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color", startColor);
        meshRenderer.materials[0].DOColor(_startColor, duration).SetDelay(delay);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            LerpColor();
        }
    }
}
