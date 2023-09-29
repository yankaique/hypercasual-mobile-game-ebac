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

    [Header("Coin Setup")]
    public GameObject coinCollectorObject;

    [Header("Animation Manager")]
    public AnimatorManager animatorManager;

    [Header("Death Particle")]
    public ParticleSystem VFXDeath;

    [SerializeField] private BounceHelper _bounceHelper;

    #region privates
    private Vector3 _position;
    private bool _canRun;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 7;
    #endregion

    private void Start()
    {
        _startPosition = transform.position;
        animatorManager.PlayAnimation(AnimatorManager.AnimationType.IDLE);
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
        Bounce();
        _canRun = true;
        animatorManager.PlayAnimation(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
        ResetSpeed();
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        animatorManager.PlayAnimation(animationType);
        endScreen.SetActive(true);
    }

    public void Bounce()
    {
        if(_bounceHelper != null)
        {
            _bounceHelper.Bounce();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var isPlayerDead = collision.transform.tag == tagToCheckEnemy;
        var isPlayerFinished = collision.transform.tag == tagToCheckEndLine;

        if (isPlayerDead && !invencible)
        {
            PlayerDeath();
        }else if (isPlayerFinished){
            PlayerFinish();
        }
    }

    private void GoBackOnDeath()
    {
        transform.DOMoveZ(-1f, .3f).SetRelative();
    }

    private void PlayerDeath()
    {
        GoBackOnDeath();
        EndGame(AnimatorManager.AnimationType.DEATH);
        if(VFXDeath != null)
        {
            VFXDeath.Play();
        }
    }

    private void PlayerFinish()
    {
        EndGame();
    }

    #region PowerUps
    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollectorObject.transform.localScale = Vector3.one * amount;
        if(amount == 1) uiTextPowerUp.text = "";
    }
    public void ChangeHeigh(float amount, float duration)
    {
        /*
        var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;
        */

        transform.DOMoveY(_startPosition.y + amount, .1f);//.OnComplete(ResetHeight);
        Bounce();
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
        Bounce();
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
        Bounce();
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
        invencible = false;
        uiTextPowerUp.text = "";
    }
    #endregion
}
