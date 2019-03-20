using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

// on complete delegate

public class CreateOrbs : MonoBehaviour
{
    public GameObject _orbProto;
    public float _minRadius;
    public float _maxRadius;

    public IEnumerator PlaceOrbsOnMap(int numberOfOrbs)
    {
        yield return new WaitForSeconds(2f);

        List<float> orbsPlacedAngles = new List<float>();
        float angleToPlace = 0f;
        Orb._currentBallNumber = 0;
        Orb._numberOfTotalBalls = numberOfOrbs;

        for (int i = 0; i < numberOfOrbs; i++)
        {
            angleToPlace = Random.Range(0f, 359f);
            while (OrbCloseToOtherOrbs(orbsPlacedAngles, angleToPlace) == false)
            {
                angleToPlace = Random.Range(0f, 359f);
            }

            orbsPlacedAngles.Add(angleToPlace);
            Vector3 orbPosition = AngleToVector(angleToPlace) * Random.Range(_minRadius, _maxRadius);
            orbPosition.z = -3f;
            Orb orb = Instantiate(_orbProto, orbPosition, Quaternion.identity).GetComponent<Orb>();
            orb._ballNumber = i;

            yield return new WaitForSeconds(1f);
        }

        Shooter._canFire = true;
    }

    private static bool OrbCloseToOtherOrbs(List<float> orbs, float orbToPlace)
    {
        return orbs.TrueForAll(orbAngle => Mathf.Abs(orbAngle - orbToPlace) > 40f);
    }

    private static Vector3 AngleToVector(float angle)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0).normalized;
    }
}
