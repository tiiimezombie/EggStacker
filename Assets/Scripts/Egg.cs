using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Egg : MonoBehaviour
{
    public static Vector3 OffScreen = new Vector3(100, 0, 0);

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private SpriteRenderer _renderer;
    private bool _held;
    private bool _inCollectionZone;
    private bool _inLoseZone;

    private Color _stackedColor = new Color(0.5f, 0.5f, 0.5f);

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollectionZone")) _inCollectionZone = true;
        if (collision.CompareTag("LoseZone")) _inLoseZone = true;

        if (!_held && _inLoseZone)
        {
            EggLost();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CollectionZone")) _inCollectionZone = false;
        if (collision.CompareTag("LoseZone")) _inLoseZone = false;
    }

    internal void Setup()
    {
        _rb.gravityScale = 0;
        _rb.velocity = Vector2.zero;
        transform.position = new Vector2(Random.Range(-7.75f, 7.75f), 6);
        _renderer.color = Color.white;
        _inCollectionZone = false;
    }

    internal void Drop()
    {
        _rb.gravityScale = .3f;
        _collider.enabled = true;
    }

    internal void Freeze()
    {
        _rb.gravityScale = 0;
        _rb.velocity = Vector2.zero;
    }

    public void Grab()
    {
        _held = true;
        AudioManager.Instance.PlaySFX_EggGrab();
        Freeze();
    }

    public void Release()
    {
        _held = false;

        if (_inCollectionZone)
        {
            EggStackManager.Instance.StackEgg(this);
            return;
        }

        if (_inLoseZone)
        {
            EggLost();
            return;
        }

        _rb.gravityScale = 1;
    }

    public void Stack(Vector3 position)
    {
        _rb.gravityScale = 0;
        _rb.velocity = Vector2.zero;
        _collider.enabled = false;
        _renderer.color = _stackedColor;
    }

    private void EggLost()
    {
        EggStackManager.Instance.BreakEgg(this);
        transform.position = OffScreen;
        Freeze();
    }
}
