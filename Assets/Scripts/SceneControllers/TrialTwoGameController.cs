using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void CompleteRound();

public class TrialTwoGameController : MonoBehaviour
{
    public static TrialTwoGameController _instance;
    public int _round;
    public int _numberOfRounds;
    public int _startOrbs;

    public List<Color> _colors;
    public GameObject _startCanvas;
    public GameObject _part2;
    public GameObject _deadDialogue;
    public GameObject _doneDialogue;
    public bool _gameover;
    public bool _started;
    public bool _done;

    private CreateOrbs _orbSpawner;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            _started = false;
            Projectile._gameColors = _colors;
            Orb._onCompleteRound = OnCompletion;
            _done = false;
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
            _part2.SetActive(false);
            SetUp();
        }

        if (_done && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("LastScene");
        }

        if (_gameover == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("IntelligenceGame");
        }
    }

    public void PlayerDied()
    {
        _gameover = true;
        _deadDialogue.SetActive(true);
    }

    private void SetUp()
    {
        _startCanvas.SetActive(false);
        Shooter._canFire = false;

        if (_round == _numberOfRounds)
        {
            _doneDialogue.SetActive(true);
            _done = true;
            return;
        }
        _orbSpawner = GetComponent<CreateOrbs>();
        StartCoroutine(_orbSpawner.PlaceOrbsOnMap(_startOrbs));
    }

    private void OnCompletion()
    {
        _round++;
        _startOrbs++;
        _started = false;

        if (_round == _numberOfRounds)
        {
            _doneDialogue.SetActive(true);
            _done = true;
            return;
        }

        _part2.SetActive(true);
    }
}
