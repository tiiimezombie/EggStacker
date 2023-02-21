using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _lifeSprites;

    public int CurrentLives
    {
        get => _currentLives;
        set
        {
            _lifeSprites[_currentLives-1].SetActive(false);
            _currentLives = value;
        }
    }
    private int _currentLives;

    private void Awake()
    {
        ResetLives();
    }

    public void ResetLives()
    {
        _currentLives = _lifeSprites.Length;
        foreach (var v in _lifeSprites)
        {
            v.SetActive(true);
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
