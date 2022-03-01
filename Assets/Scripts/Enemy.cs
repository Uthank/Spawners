using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private Vector2 _target = Vector2.zero;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[1];
    private BoxCollider2D _boxCollider2D;
    private ContactFilter2D _contactFilter;
    private Vector2 _movement = new Vector2(0, 0);

    private const string AnimationHorizontalSpeed = "HorizontalSpeed";
    private const string AnimationIsFalling = "IsFalling";

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _movement = new Vector2(0, 0);
        _movement.x = _target.x - transform.position.x;

        if (Mathf.Abs(_movement.x) < 0.1f)
            _movement.x = 0;

        _movement.Normalize();

        if (_movement.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_movement.x > 0)
        {
            _spriteRenderer.flipX = false;
        }

        _animator.SetFloat(AnimationHorizontalSpeed, Mathf.Abs(_movement.x));
        _rigidbody2D.position += _movement * _speed * Time.deltaTime;
        _animator.SetBool(AnimationIsFalling, IsFalling());

        if (IsFallOutScreen() == true)
            Destroy(gameObject);
    }

    private bool IsFalling()
    {
        _hitBuffer = new RaycastHit2D[1];
        float fallDeltaDistance = 0.01f;
        _rigidbody2D.Cast(new Vector2(0, -1), _contactFilter, _hitBuffer, fallDeltaDistance);
        return !_hitBuffer[0];
    }

    private bool IsFallOutScreen()
    {
        float offscreenDistance = -8;
        return transform.position.y < offscreenDistance;
    }
}
