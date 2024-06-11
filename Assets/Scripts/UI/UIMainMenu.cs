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
    [SerializeField] private TMP_InputField nameTextInput;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private string highScoreDescriptionText = "High Score: ";
    private string highScoreSeparatorText = " by ";
    private string defaultText = "Unknown";

    // Start is called before the first frame update
    void Start()
    {
        UpdateHighScoreText();
    }

    // Enter the main game
    public void StartButtonPressed()
    {
        if (nameTextInput.text != null && !nameTextInput.text.Equals(""))
        {
            DataManager.Instance.CurrentPlayerName = nameTextInput.text;
        } 
        else
        {
            DataManager.Instance.CurrentPlayerName = defaultText;
        }

        SceneManager.LoadScene(1);
    }

    // Exit play mode/the application
    public void ExitButtonPressed()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Reset the high score data file back to its original state
    public void ResetScoreButtonPressed()
    {
        // To keep file calls to a minimum, only implement this method if the data values aren't at their default states to begin with
        if (DataManager.Instance.HighPlayerScore != 0)
        {
            DataManager.Instance.HighPlayerName = defaultText;
            DataManager.Instance.HighPlayerScore = 0;

            DataManager.Instance.SaveScore();
            UpdateHighScoreText();
        }
    }

    // Update the high score text on the main menu
    public void UpdateHighScoreText()
    {
        highScoreText.text = highScoreDescriptionText + DataManager.Instance.HighPlayerScore + highScoreSeparatorText + DataManager.Instance.HighPlayerName;
    }
}
