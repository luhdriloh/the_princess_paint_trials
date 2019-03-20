using UnityEngine;

public class RotateWeaponScript : MonoBehaviour
{
    private float _startYScale;

	private void Start ()
    {
        _startYScale = transform.localScale.y;
    }
	
	private void Update ()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        RotateWeapon(angle);
    }

    private void RotateWeapon(float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        // switch orientation of weapon depending on where you are pointing
        transform.transform.localScale = transform.right.x <= 0
            ? (Vector3)new Vector2(transform.transform.localScale.x, -_startYScale)
            : (Vector3)new Vector2(transform.transform.localScale.x, _startYScale);
    }
}
