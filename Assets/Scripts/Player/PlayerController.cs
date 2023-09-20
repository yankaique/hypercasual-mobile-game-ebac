using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.core.Singleton;
public class PlayerController : Singleton<PlayerController>
{
    [Header("Lerp Helper")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Player Props")]
    public float speed = 1f;

    [Header("Enemy Collider Tag")]
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;

    #region privates
    private Vector3 _position;
    private bool _canRun;
    private float _currentSpeed;
    #endregion

    private void Start()
    {
        ResetSpeed();
    }

    void Update()
    {
        if (!_canRun) return;
        _position = target.position;
        _position.y = transform.position.y;
        _position.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _position, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    public void StartToRun()
    {
        _canRun = true;
        ResetSpeed();
    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy || collision.transform.tag == tagToCheckEndLine)
        {
            EndGame();
        }
    }

    #region PowerUps
    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }
    #endregion
}
