using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
   

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnNext()
    {
        
            SceneManager.LoadScene(SceneManagers.instance.sceneList[1]);


    }
}
