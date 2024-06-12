using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics.Eventing.Reader;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject gameUIScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private List<TextMeshProUGUI> highScorePauseScreenTexts;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Text scoreText;

    private string highScoreDescriptionText = "High Score: ";
    private string highScoreSeparatorText = " by ";
    private string defaultText = "Unknown";

    private bool isGamePaused = false;

    // Start is called before the first frame update
    void Start()
    {

        if(DataManager.Instance != null)
        {
            scoreText.text = DataManager.Instance.CurrentPlayerName + "'s score: 0";
            UpdateHighScoreText();
        }
        else
        {
            highScoreText.text = highScoreDescriptionText + defaultText + highScoreSeparatorText + defaultText;
        }

        SetGameUIACtive();
        SetPauseUIInactive();
    }

    // Handle pause screen
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            GamePaused();
        } 
        else if(Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
        {
            GameResumed();
        }
    }


    // Update the high score text at the top of the game screen
    public void UpdateHighScoreText()
    {
        if (DataManager.Instance != null)
        {
            highScoreText.text = highScoreDescriptionText + DataManager.Instance.HighPlayerScore[0] + highScoreSeparatorText + DataManager.Instance.HighPlayerName[0];
        }
        else
        {
            Debug.Log("No data manager found to keep track of scores, data will not be saved!");
        }
        
    }

    // Update the high score text boxes on the pause screen
    public void UpdatePauseScreenHighScores()
    {
        if (DataManager.Instance != null)
        {
            for (int i = 0; i < highScorePauseScreenTexts.Count; i++)
            {
                if (i < DataManager.Instance.HighPlayerScore.Count)
                {
                    // Handle edge cases for ordinals
                    switch (i)
                    {
                        case (0):
                            highScorePauseScreenTexts[i].text = "1st: " + DataManager.Instance.HighPlayerScore[i] + highScoreSeparatorText + DataManager.Instance.HighPlayerName[i];
                            break;
                        case (1):
                            highScorePauseScreenTexts[i].text = "2nd: " + DataManager.Instance.HighPlayerScore[i] + highScoreSeparatorText + DataManager.Instance.HighPlayerName[i];
                            break;
                        case (2):
                            highScorePauseScreenTexts[i].text = "3rd: " + DataManager.Instance.HighPlayerScore[i] + highScoreSeparatorText + DataManager.Instance.HighPlayerName[i];
                            break;
                        default:
                            highScorePauseScreenTexts[i].text = (i + 1) + "th: " + DataManager.Instance.HighPlayerScore[i] + highScoreSeparatorText + DataManager.Instance.HighPlayerName[i];
                            break;
                    }
                    highScorePauseScreenTexts[i].gameObject.SetActive(true);
                } 
                else
                {
                    highScorePauseScreenTexts[i].gameObject.SetActive(false);
                }
            }
        }
    }


    // Handle pause screen resume button functionality
    public void ResumeButtonPressed()
    {
        GameResumed();
    }

    // Handle pause screen main menu button functionality
    public void MainMenuButtonPressed()
    {
        if(DataManager.Instance != null)
        {
            DataManager.Instance.SaveScore();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


    // Handle pause functions
    private void GamePaused()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        UpdatePauseScreenHighScores();
        SetPauseUIActive();
    }

    private void GameResumed()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        SetPauseUIInactive();
    }


    // Functions for setting broader UI screens active or inactive
    public void SetGameUIACtive()
    {
        gameUIScreen.SetActive(true);
    }

    public void SetGameUIInactive()
    {
        gameUIScreen.SetActive(false);
    }

    public void SetPauseUIActive()
    {
        pauseScreen.SetActive(true);
    }

    public void SetPauseUIInactive()
    {
        pauseScreen.SetActive(false);
    }
}
