using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	public float _circleSpawnRadius;
    public LevelInformation _levelInformation;
    public OnDeath _onDeathCall;

    private int _numberOfEnemies;

    public void SetNumberOfEnemies()
    {
        _numberOfEnemies = _levelInformation._numberOfEnemies;
    }

    public IEnumerator SpawnEnemies()
    {
        while (_numberOfEnemies > 0)
        {
            Enemy enemy = Instantiate(_levelInformation._enemies[GetEnemySpawnIndex()], GetSpawnPosition(), Quaternion.identity).GetComponent<Enemy>();
            enemy._onDeath = _onDeathCall;
            _numberOfEnemies--;

            yield return new WaitForSeconds(Random.Range(_levelInformation._regularSpawnIntervalMin, _levelInformation._regularSpawnIntervalMax));
        }
    }

    private int GetEnemySpawnIndex()
    {
        float spawnValue = Random.value;
        float currentSpawnValue = 0f;

        for (int i = 0; i < _levelInformation._percentages.Count; i++)
        {
            currentSpawnValue += _levelInformation._percentages[i];
            if (spawnValue < currentSpawnValue)
            {
                return i;
            }
        }

        return 0;
    }


    private Vector3 GetSpawnPosition()
    {
        float angle = Random.Range(0, 359f);
        Vector3 position = (AngleToVector(angle) * _circleSpawnRadius);
        position.z = -2;
        return position;
    }

    private Vector3 AngleToVector(float bulletAngleOfTravel)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * bulletAngleOfTravel), Mathf.Sin(Mathf.Deg2Rad * bulletAngleOfTravel), 0).normalized;
    }
}
