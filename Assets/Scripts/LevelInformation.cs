using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInformation.asset", menuName = "LevelInformation/SpawnerValues", order = 1)]
public class LevelInformation : ScriptableObject
{
    public int _numberOfEnemies;
    public int _enemyDelta;

    public float _regularSpawnIntervalMax;
    public float _regularSpawnIntervalMin;

    public List<GameObject> _enemies;
    public List<float> _percentages;

    public List<AttackType> _typeAllowed;
    public List<Color> _typeColors;
}
