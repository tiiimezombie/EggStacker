using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private EggStackManager _stackManager;
    [SerializeField] private LayerMask _hitMask;

    private Camera _mainCam;
    private Collider2D[] _hits;
    private Egg _grabbedEgg;
    private Vector3 _grabbedEggPos;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        //if (!GameManager.IsPlaying) return;

        if (Input.GetMouseButtonDown(0))
        {
            _hits = Physics2D.OverlapPointAll(_mainCam.ScreenToWorldPoint(Input.mousePosition), _hitMask);
            if (_hits.Length > 0)
            {
                var e = _hits[0].GetComponent<Egg>();
                if (e)
                {
                    _grabbedEgg = e;
                    _grabbedEgg.Grab();
                    _stackManager.SetEggGrabbed(true);
                }
            }
        }

        if (_grabbedEgg == null) return;

        if (Input.GetMouseButtonUp(0))
        {
            if (_grabbedEgg != null)
            {
                _grabbedEgg.Release();
                _stackManager.SetEggGrabbed(false);
                _grabbedEgg = null;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            _grabbedEggPos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            _grabbedEggPos.z = 0;
            _grabbedEgg.transform.position = _grabbedEggPos;
        }
    }
}
