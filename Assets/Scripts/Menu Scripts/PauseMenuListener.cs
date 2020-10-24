using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuListener : MonoBehaviour
{
    public Input controls;
    public bool isPaused = false;
    public GameObject pauseMenu;

    private void Awake()
    {
        controls = new Input();

        //if (isPaused == false){
        //    controls.Gameplay.PauseMenu.performed += ctx => Pause();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {
            controls.Gameplay.PauseMenu.performed += ctx => Pause();
        }
        else if(isPaused == true)
        {
            controls.Gameplay.PauseMenu.performed += ctx => Resume();
        }
    }

    public void Resume()
    {
        Debug.Log("Test");
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
