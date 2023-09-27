using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.core.Singleton;
using DG.Tweening;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<ItemCollectableCoin> itens;

    [Header("Animation")]
    public float scaleDuration = .1f;
    public float scaleTimeBetweenSlices = .1f;
    public float delayToStart = .5f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        itens = new List<ItemCollectableCoin>();
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if(!itens.Contains(i)) {  
            itens.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    }

    public void StartAnimations()
    {
        StartCoroutine(ScaleSliceByTime());
    }

    public void Sort()
    {
        itens = itens.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }

    IEnumerator ScaleSliceByTime()
    {
        foreach (var i in itens)
        {
            i.transform.localScale = Vector3.zero;
        }

        yield return null;

        for (int i = 0; i < itens.Count; i++)
        {
            itens[i].transform.DOScale(1, scaleDuration).SetEase(ease).SetDelay(delayToStart);
            yield return new WaitForSeconds(scaleTimeBetweenSlices);
        }
    }

}
