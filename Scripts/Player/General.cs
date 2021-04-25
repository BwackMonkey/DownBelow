using System.Collections;
using UnityEngine;

public class General : MonoBehaviour
{
    int selectedWeapon = 0;
    public int SelectedWeapon { get { return selectedWeapon; } set { selectedWeapon = value; } }
    public GameObject[] weapons;

    PlayerStats playerStats;

    GameManager gm;
    bool reloading = false;
    public bool Reloading { get { return reloading; } set { reloading = value; } }

    AudioSettings audioSettings;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        gm = FindObjectOfType<GameManager>();
        gm.UpdateUIAmmoText(weapons[selectedWeapon].GetComponent<WeaponStats>());
        audioSettings = FindObjectOfType<AudioSettings>();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(0) && selectedWeapon == 0 && !reloading)
            {
                weapons[selectedWeapon].GetComponent<Shoot>().Fire(this.gameObject);
            }

            if (Input.GetMouseButton(0) && selectedWeapon == 1 && !reloading)
            {
                weapons[selectedWeapon].GetComponent<Shoot>().Fire(this.gameObject);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (reloading)
                {
                    weapons[selectedWeapon].GetComponent<WeaponStats>().StopCoroutine("Reload");
                    reloading = false;
                    gm.UpdateUIReloading(true, 1, 1);
                }

                weapons[selectedWeapon].SetActive(false);

                if (selectedWeapon == 0)
                    selectedWeapon = 1;
                else
                    selectedWeapon = 0;

                weapons[selectedWeapon].SetActive(true);
                gm.UpdateUIAmmoText(weapons[selectedWeapon].GetComponent<WeaponStats>());
                if (weapons[selectedWeapon].GetComponent<AudioSource>().volume != audioSettings.effectPercent)
                    weapons[selectedWeapon].GetComponent<AudioSource>().volume = audioSettings.effectPercent;
            }

            if (Input.GetKeyDown(KeyCode.R) && !reloading && weapons[selectedWeapon].GetComponent<WeaponStats>().inChaimber != weapons[selectedWeapon].GetComponent<WeaponStats>().ammoCapacity)
            {
                reloading = true;
                IEnumerator reload = weapons[selectedWeapon].GetComponent<WeaponStats>().Reload(gm, this);
                weapons[selectedWeapon].GetComponent<WeaponStats>().StartCoroutine(reload);
            }
        }

        if (playerStats.health <= 0)
        {
            Destroy(gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScreen", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}
