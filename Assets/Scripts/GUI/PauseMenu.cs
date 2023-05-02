using JetBrains.Annotations;
using System;
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

    [Header("Inventory")]
    public Animation[] inventoryAnimations;
    public Animator anim;
    public bool isInventory;
    public GameObject inventoryObj;

    public void Update()
    {
        if (playerInput.actions["Pause"].WasPressedThisFrame())
        {
            if (!isPaused)
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
            else
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }

        if (playerInput.actions["Inventory"].WasPressedThisFrame())
        {
            
            if (!isInventory)
            {

                Inventory();
            }
            else
            {
                InventoryAway();
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

    public void Inventory()
    {
        isInventory = true;
        anim.SetBool("isInven", true);
        
        

    }

    public void InventoryAway()
    {
        isInventory = false;
        anim.SetBool("isInven", false);
        
    }
  
}
