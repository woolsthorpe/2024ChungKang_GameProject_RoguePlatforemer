using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers : MonoBehaviour
{
    public List<string> sceneList;
    public static SceneManagers instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene(sceneList[0]);
        else if (Input.GetKeyDown(KeyCode.F2))
            SceneManager.LoadScene(sceneList[1]);
        else if (Input.GetKeyDown(KeyCode.F3))
            SceneManager.LoadScene(sceneList[2]);
        else if (Input.GetKeyDown(KeyCode.F4))
            SceneManager.LoadScene(sceneList[3]);
        else if (Input.GetKeyDown(KeyCode.F5))
            SceneManager.LoadScene(sceneList[4]);
        else if (Input.GetKeyDown(KeyCode.F6))
            SceneManager.LoadScene(sceneList[5]);
        else if (Input.GetKeyDown(KeyCode.F7))
            SceneManager.LoadScene(sceneList[6]);

    }

    public void OnNextScence()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string nextSceneName = "";

        if (currentScene.name == sceneList[sceneList.Count - 1])
            nextSceneName = sceneList[0];
        else
        {
            for (int i = 0; i < sceneList.Count; i++)
            {
                if (currentScene.name == sceneList[i])
                {
                    nextSceneName = sceneList[i + 1];
                    break;
                }
            }
        }

        Debug.Log(nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
    public void RetryScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
