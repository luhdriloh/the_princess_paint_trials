using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float _speed;
    public float _speedDelta;
    public int _health;
    public OnDeath _onDeath;

    private static Transform _playerPosition;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private AttackType _attackType;
    private bool _dead;

    private void Start()
    {
        if (_playerPosition == null)
        {
            _playerPosition = GameObject.FindWithTag("Player").transform;
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // set up enemy type
        int enemyTypeIndex = GetEnemyAttackType();
        _attackType = TrialOneGameController._instance._currentLevel._typeAllowed[enemyTypeIndex];
        _spriteRenderer.color = TrialOneGameController._instance._colors[enemyTypeIndex];

        Vector3 direction = _playerPosition.position - transform.position;
        _spriteRenderer.flipX = direction.x <= 0;
        _rigidbody.velocity = direction.normalized * (_speed + Random.Range(0, _speed));
        _dead = false;
    }

    public void TakeDamage(int damageAmount, Vector2 bulletDirection, AttackType type)
    {
        if (type != _attackType)
        {
            return;
        }

        _health -= damageAmount;
        _rigidbody.MovePosition(transform.position + (Vector3)(bulletDirection.normalized * .2f));
        Vector3 position = transform.position + (Vector3)(bulletDirection.normalized * 1f) + Vector3.back;
        position.z = -1;
        BloodSplatterEffect._bloodSplatterEffect.PlaceBloodSplatter(position, _spriteRenderer.color);

        if (_health <= 0 && _dead == false)
        {
            _dead = true;
            _onDeath();
            Destroy(gameObject);
        }
    }

    private int GetEnemyAttackType()
    {
        int randomEnemyType = Random.Range(0, TrialOneGameController._instance._currentLevel._typeAllowed.Count);
        return randomEnemyType;
    }
}
