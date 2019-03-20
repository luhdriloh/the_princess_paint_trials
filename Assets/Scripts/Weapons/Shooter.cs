using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject _projectilePrototype;
    public GameObject _muzzle;
    public AudioClip _weaponFireClip;

    public float _weaponRPM;
    public int _weaponDamage;
    public int _numberOfProjectiles;
    public float _recoil;
    public static bool _canFire;

    private static RecoilScript _cameraRecoil;
    private RecoilScript _recoilScript;

    private float _weaponDelay;
    private float _currentDelay;

    private void Start()
    {
        _canFire = true;
        _weaponDelay = 60f / _weaponRPM;
        _currentDelay = 0f;

        _recoilScript = GetComponent<RecoilScript>();

        if (_cameraRecoil == null)
        {
            _cameraRecoil = Camera.main.GetComponent<RecoilScript>();
            _cameraRecoil.ChangeRecoilValues(_recoilScript._recoilAcceleration * 2, _recoilScript._weaponRecoilStartSpeed * 2, _recoilScript._maximumOffsetDistance * 2);
        }
    }

    private void Update()
    {
        if (_canFire == false)
        {
            return;
        }
        // controller player dead
        AttackType attackType = AttackType.NONE;
        if (Input.GetMouseButton(0))
        {
            attackType = attackType | AttackType.LEFT;
            attackType &= ~AttackType.NONE;
        }

        if (Input.GetMouseButton(1))
        {
            attackType = attackType | AttackType.RIGHT;
            attackType &= ~AttackType.NONE;
        }

        _currentDelay += Time.deltaTime;
        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && _currentDelay >= _weaponDelay)
        {
            FireBullet(attackType);
            _recoilScript.AddRecoil();
            _cameraRecoil.AddRecoil(_muzzle.transform.right);
            AudioEffects._audioEffects.PlaySoundEffect(_weaponFireClip);
            _currentDelay = 0f;
        }
    }

    private void FireBullet(AttackType attackType)
    {
        for (int i = 0; i < _numberOfProjectiles; i++)
        {
            Vector3 fireVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(fireVector.y, fireVector.x) * Mathf.Rad2Deg;
            angle += Random.Range(-_recoil / 2f, _recoil / 2f);

            Vector3 startPosition = _muzzle.transform.position;
            startPosition.z = -2f;
            Projectile projectile = Instantiate(_projectilePrototype, startPosition, Quaternion.identity).GetComponent<Projectile>();
            projectile.FireProjectile(angle, attackType);
        }
    }
}
