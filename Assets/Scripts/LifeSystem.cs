using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _lifeSprites;
    [SerializeField] private ParticleSystem _heartParticles;
    private Camera _mainCam;
    private Vector3[] _heartWorldPositionArray;
    private bool _positionsSet = false;

    public int CurrentLives
    {
        get => _currentLives;
        set
        {
            _lifeSprites[_currentLives - 1].SetActive(false);
            if (!_positionsSet) dfsd();
            _heartParticles.transform.position = _heartWorldPositionArray[_currentLives - 1];
            _heartParticles.Play();
            AudioManager.Instance.PlaySFX_Oops();
            _currentLives = value;
        }
    }
    private int _currentLives;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    public void ResetLives()
    {
        _currentLives = _lifeSprites.Length;
        foreach (var v in _lifeSprites)
        {
            v.SetActive(true);
        }
    }

    private void dfsd()
    {
        _positionsSet = true;

        _heartWorldPositionArray = new Vector3[_lifeSprites.Length];

        for (int i = 0; i < _lifeSprites.Length; i++)
        {
            _heartWorldPositionArray[_lifeSprites.Length -  1 - i] = _mainCam.ScreenToWorldPoint(_lifeSprites[i].transform.position);
            _heartWorldPositionArray[_lifeSprites.Length - 1 - i].z = 0;
            Debug.Log(_heartWorldPositionArray[i]);
            Debug.Log(_lifeSprites[i].transform.position);
        }
    }

    public void LoseLife()
    {
        CurrentLives--;

        if (CurrentLives <= 0)
        {
            GameManager.Instance.LoseGame();
        }
    }
}
