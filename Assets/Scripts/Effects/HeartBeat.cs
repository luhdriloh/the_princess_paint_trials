using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    public float _defaultPitch;
    public float _maxPitch;
    public float _pitchDeltaPerSecond;

    private AudioSource _audioSource;
    private CircleCollider2D _collider;
    private float _radius;
    private float _pitchDifference;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<CircleCollider2D>();
        _radius = _collider.radius;
        _pitchDifference = _maxPitch - _defaultPitch;
    }

    private void Update()
    {
        if (TrialOneGameController._instance._gameover)
        {
            Destroy(gameObject);
        }

        float enemyDistance = EnemyOverlapDistance();
        if (enemyDistance > 0)
        {
            float toAdd = ((_radius - enemyDistance) / _radius) * _pitchDifference;
            float pitchToRemove = _pitchDeltaPerSecond * Time.deltaTime;

            _audioSource.pitch = Mathf.Max(_defaultPitch + toAdd, _audioSource.pitch - pitchToRemove);
        }
        else
        {
            float pitchToRemove = _pitchDeltaPerSecond * Time.deltaTime;
            _audioSource.pitch = Mathf.Max(_audioSource.pitch - pitchToRemove, _defaultPitch);
        }
    }

    private float EnemyOverlapDistance()
    {
        // set up contact filter and results array
        ContactFilter2D colliderFilter = new ContactFilter2D();
        colliderFilter.SetLayerMask(LayerMask.GetMask("Monster"));

        Collider2D[] results = new Collider2D[5];

        // find collider overlap
        int numberOfContacts = _collider.OverlapCollider(colliderFilter, results);
        return numberOfContacts > 0 ? ReturnClosestEnemyDistance(results, numberOfContacts) : -1;
    }

    private float ReturnClosestEnemyDistance(Collider2D[] enemyList, int numberOfContacts)
    {
        float closestDistance = float.MaxValue;

        for (int i = 0; i < numberOfContacts; i++)
        {
            float distanceToEnemy = ((Vector2)enemyList[i].gameObject.transform.position - (Vector2)transform.position).magnitude;
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
            }
        }

        return closestDistance;
    }
}
