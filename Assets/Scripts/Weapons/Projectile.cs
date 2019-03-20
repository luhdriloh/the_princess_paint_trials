using System;
using System.Collections.Generic;

using UnityEngine;


[Flags]
public enum AttackType
{ 
    NONE = 0,
    LEFT = 1,
    RIGHT = 2,

    LEFTANDRRIGHT = LEFT | RIGHT
}


public class Projectile : MonoBehaviour
{
    public static List<Color> _gameColors = new List<Color>();

    public AttackType _projectileType;
    public float _speed;
    public float _speedDelta;
    public float _acceleration;
    public int _damage;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private AttackType _attackType;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void FireProjectile(float fireAngle, AttackType attackType)
    {
        _attackType = attackType;

        if ((attackType & (AttackType.LEFT | AttackType.RIGHT)) == (AttackType.LEFT | AttackType.RIGHT))
        {
            _spriteRenderer.color = _gameColors[2];
        }
        else if ((attackType & AttackType.LEFT) == AttackType.LEFT)
        {
            _spriteRenderer.color = _gameColors[0];
        }
        else if ((attackType & AttackType.RIGHT) == AttackType.RIGHT)
        {
            _spriteRenderer.color = _gameColors[1];
        }

        Quaternion rotation = Quaternion.AngleAxis(fireAngle, Vector3.forward);
        transform.rotation = rotation;

        float speed = _speed + UnityEngine.Random.Range(0, _speedDelta);
        float time = _acceleration < -Mathf.Epsilon ? speed / -_acceleration : float.MaxValue;

        _rigidbody.velocity = BulletTravelVector(fireAngle) * speed;
        Invoke("OnDestroy", Mathf.Min(time, 1f));
    }


    private void FixedUpdate()
    {
        _rigidbody.velocity += (Vector2)(transform.right * _acceleration * Time.deltaTime);
    }


    private Vector3 BulletTravelVector(float bulletAngleOfTravel)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * bulletAngleOfTravel), Mathf.Sin(Mathf.Deg2Rad * bulletAngleOfTravel), 0).normalized;
    }


    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            collision.GetComponent<Enemy>().TakeDamage(_damage, _rigidbody.velocity, _attackType);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Orb"))
        {
            collision.GetComponent<Orb>().TakeDamage(_attackType);
        }

        Destroy(gameObject);
    }
}
