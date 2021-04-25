using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ammoText;

    [Header("Reloading")]
    public GameObject reloadBar;
    public Image overlay;

    public GameObject menu;

    AudioSettings audioSettings;

    Slider slider;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(9, 9, true);
        audioSettings = FindObjectOfType<AudioSettings>();
        audioSettings.UpdateEffect();
        audioSettings.UpdateMusic();
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            menu.SetActive(!menu.activeSelf);
            if (Time.timeScale == 0)
            {
                slider = FindObjectOfType<Slider>();
                slider.UpdateSliderPercent(audioSettings);
            }
        }
        
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void UpdateUIAmmoText(WeaponStats ws)
    {
        ammoText.text = ws.ammoCapacity + "/" + ws.inChaimber;
    }

    public void UpdateUIReloading(bool done, float currentTime, float goalTime)
    {
        if (!reloadBar.activeSelf && !done)
            reloadBar.SetActive(true);
        else if (reloadBar.activeSelf && done)
            reloadBar.SetActive(false);

        overlay.fillAmount = currentTime / goalTime;

    }
}
