using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMainMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Initialize text boxes with saved data here
    }

    public void StartButtonPressed()
    {
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
}
