using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    public List<Transform> position;
    public float duration = 1.0f;
    private int _index = 0;

    private void Start()
    {
        transform.position = position[0].transform.position;
        NextIndex();
        StartCoroutine(StartMoviment());
    }

    public void NextIndex()
    {
        _index++;
        if (_index >= position.Count) _index = 0;
    }

    IEnumerator StartMoviment()
    {
        float time = 0;

        while (true)
        {
            var currentPosition  = transform.position;
            while (time < duration)
            {
                transform.position = Vector3.Lerp(currentPosition, position[_index].transform.position, (time/duration));
                time += Time.deltaTime;
                yield return null;
            }

            NextIndex();
            time = 0;
            yield return null;
        }
    }
}
