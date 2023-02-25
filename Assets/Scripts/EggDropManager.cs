using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggDropManager : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefab;
    [SerializeField] private Image _timerImage;

    private List<Egg> _spawnedEggs = new List<Egg>();
    private int _eggListIndex;

    private bool _canDrop;
    private float _dropTimer;
    private float _dropDelayMax;
    private bool _canSpeedUp;

    private System.Diagnostics.Stopwatch _speedUpStopwatch = new System.Diagnostics.Stopwatch();
    private float _speedUpDelay = 1000 * 10;

    private void Awake()
    {
        GameManager.SetPlaying += SetDropTimer;

    }

    private void OnDestroy()
    {
        GameManager.SetPlaying -= SetDropTimer;
    }

    private void SetDropTimer(bool isPlaying)
    {
        if (isPlaying)
        {
            _canSpeedUp = true;
            _dropDelayMax = 2f; // 2
            _dropTimer = _dropDelayMax;
            _eggListIndex = 0;

            foreach (var v in _spawnedEggs)
            {
                v.transform.position = Egg.OffScreen;
            }

            _speedUpStopwatch.Restart();
        }
        else
        {
            _speedUpStopwatch.Stop();
            foreach (var v in _spawnedEggs)
            {
                v.Freeze();
            }
        }
        _canDrop = isPlaying;
        _timerImage.fillAmount = 0;
    }

    private void Update()
    {
        if (!_canDrop) return;

        _dropTimer -= Time.deltaTime;
        _timerImage.fillAmount = _dropTimer / _dropDelayMax;

        if (_dropTimer <= 0)
        {
            // Add egg
            var egg = GetEgg();

            egg.Setup();
            egg.Drop();

            _dropTimer = _dropDelayMax + Random.Range(-0.2f, 0.4f);
            Debug.Log(_dropTimer);
        }

        if (_canSpeedUp && _speedUpStopwatch.ElapsedMilliseconds > _speedUpDelay)
        {
            if (_dropDelayMax < 0.5f)
            {
                _speedUpStopwatch.Stop();
                _canSpeedUp = false;
            }
            else
            {
                _dropDelayMax -= 0.2f;
                _speedUpStopwatch.Restart();
                Debug.Log("speed up");
            }
        }
    }

    private Egg GetEgg()
    {
        if (_spawnedEggs.Count <= _eggListIndex)
        {
            _spawnedEggs.Add(Instantiate(EggPrefab, transform).GetComponent<Egg>());
        }

        return _spawnedEggs[_eggListIndex++];
    }
}
