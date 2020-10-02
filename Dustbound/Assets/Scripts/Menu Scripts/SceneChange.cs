using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{

    public void PlayGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame(){
        Application.Quit();
        
    }

    public void MainMenu(){
        Time.timeScale = 1.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Resume(){
        //Resume Code Goes in here
    }
    public void Options(){
        //Option Code Goes in here
    }
}
