using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    private string highScoreDescriptionText = "High Score: ";
    private string highScoreSeparatorText = " by ";
    private string defaultText = "Unknown";

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
    }

    public void UpdateHighScoreText()
    {
        highScoreText.text = highScoreDescriptionText + DataManager.Instance.HighPlayerScore + highScoreSeparatorText + DataManager.Instance.HighPlayerName;
    }
}
