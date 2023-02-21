using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public enum UIType
    {
        Intro,
        Retry,
        Game
    }

    [SerializeField] private GameObject _mainMenuGO;
    [SerializeField] private GameObject _replayMenuGO;
    [SerializeField] private GameObject _gameUIGO;

    [SerializeField] private Transform _titleTextGO;

    void Start()
    {
        _titleTextGO.DOScale(1.2f, 1).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetUI(UIType ui)
    {
        _mainMenuGO.SetActive(false);
        _replayMenuGO.SetActive(false);
        _gameUIGO.SetActive(false);

        switch (ui)
        {
            case UIType.Intro:
                _mainMenuGO.SetActive(true);
                break;
            case UIType.Retry:
                _replayMenuGO.SetActive(true);
                break;
            case UIType.Game:
                _gameUIGO.SetActive(true);
                break;
        }
    }
}
