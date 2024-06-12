using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    [SerializeField] private UIGame gameUI;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private int scoreIndex;
    private int maxHighScores = 10;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        
        // If the player manages to get a higher score than the previous high score, update the high score accordingly
        if(DataManager.Instance != null && m_Points > DataManager.Instance.HighPlayerScore[DataManager.Instance.HighPlayerScore.Count - 1])
        {
            gameUI.UpdateHighScoreText();
        }

    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (DataManager.Instance != null && 
            ((m_Points > DataManager.Instance.HighPlayerScore[DataManager.Instance.HighPlayerScore.Count - 1] && DataManager.Instance.HighPlayerScore.Count >= maxHighScores) 
            || DataManager.Instance.HighPlayerScore.Count < maxHighScores))
        {
            AddPlayerHighScore();
            DataManager.Instance.SaveScore();
        }
    }

    private void AddPlayerHighScore()
    {
        bool isScoreInserted = false;

        for (int i = 0; i < DataManager.Instance.HighPlayerScore.Count; i++)
        {
            if(m_Points > DataManager.Instance.HighPlayerScore[i])
            {
                DataManager.Instance.HighPlayerScore.Insert(i, m_Points);
                DataManager.Instance.HighPlayerName.Insert(i, DataManager.Instance.CurrentPlayerName);
                isScoreInserted = true;
                break;
            }
        }

        // If necessary, truncate the last value to keep the high scores pruned at 10 values at most
        // Otherwise, add a new high score to the end of the high scores list
        if(DataManager.Instance.HighPlayerScore.Count > maxHighScores)
        {
            DataManager.Instance.HighPlayerScore.RemoveAt(DataManager.Instance.HighPlayerScore.Count-1);
            DataManager.Instance.HighPlayerName.RemoveAt(DataManager.Instance.HighPlayerName.Count-1);
        } else if(!isScoreInserted)
        {
            DataManager.Instance.HighPlayerScore.Add(m_Points);
            DataManager.Instance.HighPlayerName.Add(DataManager.Instance.CurrentPlayerName);
        }
    }
}
