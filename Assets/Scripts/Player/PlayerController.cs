using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.core.Singleton;
using TMPro;
using DG.Tweening;
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
    public bool invencible = false;
    public TextMeshPro uiTextPowerUp;

    #region privates
    private Vector3 _position;
    private bool _canRun;
    private float _currentSpeed;
    private Vector3 _startPosition;
    #endregion

    private void Start()
    {
        _startPosition = transform.position;
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
            if(!invencible) EndGame();
        }
    }

    #region PowerUps
    public void ChangeHeigh(float amount, float duration)
    {
        /*
        var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;
        */

        transform.DOMoveY(_startPosition.y + amount, .1f);//.OnComplete(ResetHeight);
        Invoke("ResetHeight", duration);
    }

    public void ResetHeight()
    {
        /*
        var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p;
        */
        transform.DOMoveY(_startPosition.y, .1f);//.OnComplete(ResetHeight);
    }
    public void SetPowerUpText(string text)
    {
        uiTextPowerUp.text = text;
    }
    public void PowerUpInvencible(bool d = true)
    {
        invencible = d;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
        invencible = false;
        uiTextPowerUp.text = "";
    }
    #endregion
}
