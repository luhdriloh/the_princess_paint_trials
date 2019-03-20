using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneGameController : MonoBehaviour
{
    public List<Color> _colors;

    private void Awake()
    {
        Projectile._gameColors = _colors;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("CombatGame");
        }
    }
}
