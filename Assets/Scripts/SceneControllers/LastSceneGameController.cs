using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastSceneGameController: MonoBehaviour
{
    public GameObject _startDialogue;
    public GameObject _optionDialogue;

    public GameObject _dateOption;
    public GameObject _leaveOption;

    public Button _dateButton;
    public Button _leaveButton;

    private bool _started;

    private void Start()
    {
        _started = false;

        _dateButton.onClick.AddListener(StartDateOption);
        _leaveButton.onClick.AddListener(LeaveOption);
    }

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _started == false)
        {
            _startDialogue.SetActive(false);
            _optionDialogue.SetActive(true);
            _started = true;
        }
    }

    private void StartDateOption()
    {
        _optionDialogue.SetActive(false);
        _dateOption.SetActive(true);
        Invoke("GoBackToTitle", 4);
    }

    private void LeaveOption()
    {
        _optionDialogue.SetActive(false);
        _leaveOption.SetActive(true);
        Invoke("GoBackToTitle", 4);
    }

    private void GoBackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

































