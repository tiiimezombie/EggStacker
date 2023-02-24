using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class EggStackManager : MonoBehaviour
{
    public static EggStackManager Instance;

    [SerializeField] private LifeSystem _lifeSystem;
    [SerializeField] private TextMeshProUGUI _eggCountText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _reticle;
    [SerializeField] private ParticleSystem _crackParticles;

    private int StackCount
    {
        get => _stackCount;
        set
        {
            _stackCount = value;
            _eggCountText.text = _stackCount + " Eggs!";
            _eggCountText.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1), 0.4f, 1);
        }
    }
    private int _stackCount;
    private int _functionalStackCount;


    private Vector2 _gridDimensions = new Vector2(10, 8);
    private int _maxEggsPerGroup;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        Instance = this;

        GameManager.SetPlaying += GameManager_SetPlaying;

        _reticle.SetActive(false);
        _crackParticles.transform.position = Egg.OffScreen;
        _maxEggsPerGroup = Mathf.RoundToInt(_gridDimensions.x * _gridDimensions.y);
    }

    private void OnDestroy()
    {
        GameManager.SetPlaying -= GameManager_SetPlaying;
    }

    private void GameManager_SetPlaying(bool obj)
    {
        _reticle.SetActive(false);
        if (obj)
        {
            StackCount = 0;
            _functionalStackCount = 0;
            _reticle.transform.position = NextEggLocation();

            _crackParticles.Stop();
            _crackParticles.transform.position = Egg.OffScreen;

            _lifeSystem.ResetLives();
        }
        else
        {
            _scoreText.text = "Final Score: " + _stackCount;
        }
    }

    // -7.5 to 7.5
    // -3.25 to 3.5

    public void StackEgg(Egg egg)
    {
        StackCount++;

        egg.Stack(NextEggLocation());
        _functionalStackCount++;
        _reticle.transform.position = NextEggLocation();

        if (_functionalStackCount > _maxEggsPerGroup)
            _functionalStackCount = 0;

        AudioManager.Instance.PlaySFX_EggPlace();
    }

    internal void SetEggGrabbed(bool v)
    {
        _reticle.SetActive(v);
    }

    Vector3 _nextLocation = new Vector3(0, 0, -1);
    int gridsize = 16;
    private Vector3 NextEggLocation()
    {
        _nextLocation.x = Mathf.FloorToInt(_functionalStackCount / _gridDimensions.y) - 6.5f;
        _nextLocation.y = _functionalStackCount % _gridDimensions.y - 3.25f;

        return _nextLocation;
    }

    internal void BreakEgg(Egg egg)
    {
        _crackParticles.transform.position = egg.transform.position;
        _crackParticles.Play();

        _lifeSystem.LoseLife();
    }
}
