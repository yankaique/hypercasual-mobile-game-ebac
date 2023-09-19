using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lerp Helper")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Player Props")]
    public float speed = 1f;

    [Header("Enemy Collider Tag")]
    public string tagToCompare = "Enemy";

    #region privates
    private Vector3 _position;
    private bool _canRun;
    #endregion

    private void Start()
    {
        _canRun = true;
    }

    void Update()
    {
        if (!_canRun) return;
        _position = target.position;
        _position.y = transform.position.y;
        _position.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _position, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCompare)
        {
            _canRun = false;
        }
    }
}
