using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class EggStackManager : MonoBehaviour
{
    public static EggStackManager Instance;

    [SerializeField] private TextMeshProUGUI _eggCountText;
    [SerializeField] private GameObject _reticle;
    [SerializeField] private Animator _eggCrackAnimator;

    private int StackCount
    {
        get => _stackCount;
        set
        {
            _stackCount = value;
            _eggCountText.text = _stackCount + " Eggs!";
            //_eggCountText.gameObject.dosca;
        }
    }
    private int _stackCount;
    private int _functionalStackCount;


    private Vector2 _gridDimensions = new Vector2(10, 8);
    private Vector2 _currentPosition = Vector2.zero;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        Instance = this;

        GameManager.SetPlaying += GameManager_SetPlaying;

        _reticle.SetActive(false);
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
            _currentPosition = Vector2.zero;
            _functionalStackCount = 0;
            _reticle.transform.position = NextEggLocation();

            _eggCrackAnimator.transform.position = Egg.OffScreen;
            _eggCrackAnimator.SetTrigger("Playing");
        }
        else
        {

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

        if (_functionalStackCount > 20)
        {
            Debug.Log("many egg");
            _functionalStackCount = 0;
        }
    }

    internal void SetEggGrabbed(bool v)
    {
        _reticle.SetActive(v);
    }

    Vector3 _nextLocation = new Vector3(0, 0, -1);
    int gridsize = 16;
    private Vector3 NextEggLocation()
    {
        Debug.Log(_nextLocation);

        _nextLocation.x = Mathf.FloorToInt(_functionalStackCount / _gridDimensions.y) - 6.5f;
        _nextLocation.y = _functionalStackCount % _gridDimensions.y - 3.25f;

        return _nextLocation;
    }

    internal void BreakEgg(Egg egg)
    {
        _eggCrackAnimator.transform.position = egg.transform.position;
        _eggCrackAnimator.SetTrigger("Playing");

        egg.transform.position = new Vector3(100, 0, 0);        
    }
}
