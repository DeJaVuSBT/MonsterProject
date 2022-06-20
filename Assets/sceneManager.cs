using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

public class sceneManager : MonoBehaviour
{

    public GameObject loadingScreen;
    public TMP_Text loadingTxt;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(loadingScreen);
    }
    public void quitGame(InputAction.CallbackContext obj)
    {
        Application.Quit();
        if(EditorApplication.isPlaying)
        {
            EditorApplication.ExitPlaymode();
        }
    }
    public void restartGame(InputAction.CallbackContext obj)
    {
        loadingScreen.SetActive(true);
        loadLevel(0);
    }

    public void loadLevel(int levelIndex)
    {
        StartCoroutine(LoadLevelAsync(levelIndex));
    }

    private IEnumerator LoadLevelAsync(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name , LoadSceneMode.Single);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);
            loadingTxt.text = (progress * 100f).ToString("F0") + "%";
            if(progress > 0.8f)
            {
                //loadingScreen.SetActive(false);
                loadingTxt.text = "DONE";                
            }
            yield return null;
        }
        if(operation.isDone)
        {
            TimerAction.Create(() => loadingScreen.SetActive(false) , 0.5f);
        }
        
        
    }
}
