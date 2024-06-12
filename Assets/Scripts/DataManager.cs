using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string CurrentPlayerName;
    public List<string> HighPlayerName;
    public List<int> HighPlayerScore;

    // Data to save for later use between sessions
    [Serializable]
    class SaveData
    {
        public List<string> HighPlayerName;
        public List<int> HighPlayerScore;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        HighPlayerName = new List<string>();
        HighPlayerScore = new List<int>();

        LoadScore();
    }

    // Update the save file to reflect the high score
    public void SaveScore()
    {
        SaveData data = new SaveData();
        string json;

        data.HighPlayerName = Instance.HighPlayerName;
        data.HighPlayerScore = Instance.HighPlayerScore;

        json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
    }

    // Load from the save file should it exist.  Otherwise, set default values for DataManager
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if(data.HighPlayerName.Count > 0)
            {
                Instance.HighPlayerName = data.HighPlayerName;
                Instance.HighPlayerScore = data.HighPlayerScore;
            } 
            else
            {
                Instance.HighPlayerName.Add("Unknown");
                Instance.HighPlayerScore.Add(0);
            }
            
        }
        else
        {
            Instance.HighPlayerName.Add("Unknown");
            Instance.HighPlayerScore.Add(0);
            Debug.Log("Data initialized");
        }
    }
}
