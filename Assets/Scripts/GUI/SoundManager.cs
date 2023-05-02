using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    [SerializeField] GameObject pauseButtonsUIObject;
    [SerializeField] GameObject volumeUIObject;
    // Start is called before the first frame update
    void Start()
    {
        //if there is no save data for volume - then set it to a certain dcb
        if (!PlayerPrefs.HasKey("gameVolume"))
        {
            //defaultly set it to a low volume as to not ruin the player UX upon starting the game
            PlayerPrefs.SetFloat("gameVolume", 0.3f);
            Load();
        }

        else
        {
            Load();
        }

        if (volumeUIObject.activeSelf)
        {
            //unlock cursor
            Debug.Log("it works so go ahead and unlock cursor");
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    //use player prefs to save and load volume settings
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("gameVolume");
        
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("gameVolume", volumeSlider.value);
    }

    public void BackButton()
    {
        volumeUIObject.SetActive(false);
        pauseButtonsUIObject.SetActive(true);
    }

    public void OpenOptions()
    {
        volumeUIObject.SetActive(true);
        pauseButtonsUIObject.SetActive(false);
    }
}
