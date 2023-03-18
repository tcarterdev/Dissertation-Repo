using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausemenuObj;
    public PlayerInput playerInput;
    public bool isPaused;

   
    public void Update()
    {
        if (playerInput.actions["Pause"].WasPressedThisFrame())
        {
            if (!isPaused)
            {
                Pause();
                
            }
            else
            {
                Resume();
                
            }

        }
    }
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausemenuObj.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausemenuObj.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OptionsMM()
    { 
    
    }
}
