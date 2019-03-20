using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public delegate void OnDeath();

public class TrialOneGameController : MonoBehaviour
{
    public static TrialOneGameController _instance;
    public int _level;
    public List<LevelInformation> _levelsInformation;
    public LevelInformation _currentLevel;
    public List<Color> _colors;

    public GameObject _startCanvas;
    public GameObject _deathCount;
    public GameObject _part2;
    public GameObject _doneDialogue;
    public GameObject _deadDialogue;

    public Text _enemiesLeft;
    public Image _leftClick;
    public Image _rightClick;
    public bool _gameover;
    public bool _started;
    private bool _done;

    private Spawner _enemySpawner;
    private int _numberEnemies;
    private int _whichColorSet;
    private GameObject _toSet;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            _started = false;
            _whichColorSet = 0;
            _colors = _levelsInformation[_whichColorSet]._typeColors;
            Projectile._gameColors = _colors;
            _done = false;
            _toSet = _startCanvas;

            _leftClick.color = _colors[0];
            _rightClick.color = _colors[1];
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_started == false && Input.GetKeyDown(KeyCode.Space))
        {
            _started = true;
            _toSet.SetActive(false);
            _deathCount.SetActive(true);
            SetUp();
        }

        if (_done && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("IntelligenceGame");
        }

        if (_gameover == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("CombatGame");
        }
    }

    public void PlayerDied()
    {
        _gameover = true;
        _deadDialogue.SetActive(true);
    }

    private void SetUp()
    {
        _currentLevel = _levelsInformation[_level];
        _numberEnemies = _levelsInformation[_level]._numberOfEnemies;
        _enemiesLeft.text = "x " + _numberEnemies.ToString();

        _gameover = false;
        _enemySpawner = GetComponent<Spawner>();
        _enemySpawner._onDeathCall = OnEnemyDeath;
        _enemySpawner._levelInformation = _levelsInformation[_level];
        _enemySpawner.SetNumberOfEnemies();
        StartCoroutine(_enemySpawner.SpawnEnemies());
    }

    private void OnEnemyDeath()
    {
        _numberEnemies--;
        _enemiesLeft.text = "x " + _numberEnemies.ToString();

        if (_numberEnemies <= 0)
        {
            _level++;
            if (_level >= _levelsInformation.Count)
            {
                _doneDialogue.SetActive(true);
                _done = true;
            }
            else
            {
                _part2.SetActive(true);
                _toSet = _part2;
                _started = false;
            }
        }
    }
}
