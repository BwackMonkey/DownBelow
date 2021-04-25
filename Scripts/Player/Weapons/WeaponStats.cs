using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float bulletForce;
    public float damage;
    public float reloadTime;
    public float fireSpeed;
    public float ammoCapacity;
    public float inChaimber;

    float time;

    public IEnumerator Reload(GameManager gm, General g)
    {
        time = 0;
        while(time < reloadTime)
        {
            time += Time.deltaTime;
            gm.UpdateUIReloading(false, time, reloadTime);
            yield return new WaitForEndOfFrame();
        }
        inChaimber = ammoCapacity;
        gm.UpdateUIReloading(true, time, reloadTime);
        gm.UpdateUIAmmoText(this);
        g.Reloading = false;
        yield return null;
    }
}
