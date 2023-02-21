using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool IsPlaying;
    public static event System.Action<bool> SetPlaying;

    [SerializeField] private UIManager _uiManager;

    private void Awake()
    {
        if (Instance != null) Destroy(this);

        Instance = this;
    }

    private void Start()
    {
        _uiManager.SetUI(UIManager.UIType.Intro);
        StartGame();
    }

    public void StartGame()
    {
        _uiManager.SetUI(UIManager.UIType.Game);

        IsPlaying = true;
        SetPlaying?.Invoke(IsPlaying);
    }

    public void LoseGame()
    {
        _uiManager.SetUI(UIManager.UIType.Retry);

        IsPlaying = false;
        SetPlaying?.Invoke(IsPlaying);
    }
}
