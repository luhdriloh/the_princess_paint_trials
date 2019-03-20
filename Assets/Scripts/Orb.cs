using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public static CompleteRound _onCompleteRound;
    public static int _currentBallNumber;
    public static int _numberOfTotalBalls;
    public int _ballNumber;
    public AttackType _attackType;

    public AudioClip _spawn;
    public AudioClip _wrongOne;
    public AudioClip _rightOne;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private bool _shot;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _attackType = GetAttackType();
        _shot = false;
        SoundEffect._sfxPlayer.PlaySoundEffect(_spawn);
        Flash();
    }

    public void Flash()
    {
        if ((_attackType & AttackType.LEFT) == AttackType.LEFT)
        {
            _spriteRenderer.color = TrialTwoGameController._instance._colors[0];
        }
        else if ((_attackType & AttackType.RIGHT) == AttackType.RIGHT)
        {
            _spriteRenderer.color = TrialTwoGameController._instance._colors[1];
        }

        Invoke("StopFlash", .5f);
    }

    public void StopFlash()
    {
        _spriteRenderer.color = Color.white;
    }

    public void TakeDamage(AttackType type)
    {
        if (_shot == true)
        {
            return;
        }

        _shot = true;

        // gameover
        if (type != _attackType || _currentBallNumber != _ballNumber)
        {
            SoundEffect._sfxPlayer.PlaySoundEffect(_wrongOne);
            _rigidbody.velocity = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized * 2f;
            return;
        }

        _currentBallNumber++;
        Flash();
        SoundEffect._sfxPlayer.PlaySoundEffect(_rightOne);
        Invoke("DestroyThis", .5f);
    }

    private void DestroyThis()
    {
        if (_currentBallNumber == _numberOfTotalBalls)
        {
            _onCompleteRound();
        }

        Destroy(gameObject);
    }

    private AttackType GetAttackType()
    {
        return Random.value <= .5f ? AttackType.LEFT : AttackType.RIGHT;
    }
}
