using UnityEngine;

public class Player : MonoBehaviour
{
    public Color _bloodColor;
    private SpriteRenderer _spriteRenderer;
    private bool _dead;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _dead = false;
    }

    private void Update()
    {
        if (_dead)
        {
            return;
        }

        Vector3 fireVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _spriteRenderer.flipX = fireVector.x <= 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _dead = true;
        Destroy(GetComponentInChildren<Shooter>());
        Destroy(GetComponentInChildren<RotateWeaponScript>());
        GetComponent<Animator>().SetBool("Dead", true);

        for (int i = 0; i < 3; i++)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            position.z = -1f;
            BloodSplatterEffect._bloodSplatterEffect.PlaceBloodSplatter(position, _bloodColor);

            if (TrialOneGameController._instance == null)
            {
                TrialTwoGameController._instance.PlayerDied();
            }
            else
            {
                TrialOneGameController._instance.PlayerDied();
            }
        }
    }
}
