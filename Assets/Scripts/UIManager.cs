using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    public void musicVolume()
    {
        audioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void sfxVolume()
    {
        audioManager.Instance.MusicVolume(sfxSlider.value);
    }
}
