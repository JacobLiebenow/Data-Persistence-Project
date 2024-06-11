using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameInputText;
    [SerializeField] TextMeshProUGUI highScoreText;
    private string defaultText = "Unknown";
    private string highScoreDescriptionText = "High Score: ";
    private string highScoreSeparatorText = " by ";

    // Start is called before the first frame update
    void Start()
    {
        UpdateHighScoreText();
    }

    public void StartButtonPressed()
    {
        if (nameInputText != null)
        {
            DataManager.Instance.CurrentPlayerName = nameInputText.text;
        } else
        {
            DataManager.Instance.CurrentPlayerName = defaultText;
        }

        SceneManager.LoadScene(1);
    }

    public void ExitButtonPressed()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void UpdateHighScoreText()
    {
        highScoreText.text = highScoreDescriptionText + DataManager.Instance.HighPlayerScore + highScoreSeparatorText + DataManager.Instance.HighPlayerName;
    }
}
