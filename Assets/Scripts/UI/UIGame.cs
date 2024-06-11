using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject gameUIScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private List<TextMeshProUGUI> highScorePauseScreenTexts;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private string highScoreDescriptionText = "High Score: ";
    private string highScoreSeparatorText = " by ";
    private string defaultText = "Unknown";

    private bool isGamePaused = false;

    // Start is called before the first frame update
    void Start()
    {

        if(DataManager.Instance != null)
        {
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
            highScoreText.text = highScoreDescriptionText + DataManager.Instance.HighPlayerScore + highScoreSeparatorText + DataManager.Instance.HighPlayerName;
        }
        else
        {
            Debug.Log("No data manager found to keep track of scores, data will not be saved!");
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
