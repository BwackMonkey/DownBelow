using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float bulletForce;

    WeaponStats ws;
    GameManager gm;

    bool wait = false;
    float waitTime;

    PlaySound ps;

    private void Start()
    {
        ws = GetComponent<WeaponStats>();
        gm = FindObjectOfType<GameManager>();
        ps = GetComponent<PlaySound>();
    }

    public void Fire(GameObject refrence)
    {
        if (!wait)
        {
            if (ws.inChaimber > 0)
            {
                GameObject bulletInst = Instantiate(bullet, firePoint.position, firePoint.rotation);
                bulletInst.GetComponent<Bullet>().damage = ws.damage;
                bulletInst.GetComponent<Bullet>().refrence = refrence;
                bulletInst.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                ps.Play("Bullet");
                ws.inChaimber--;
                gm.UpdateUIAmmoText(ws);
                wait = true;
                waitTime = ws.fireSpeed;
            }
        }
    }

    private void Update()
    {
        if (wait && waitTime > 0)
            waitTime -= Time.deltaTime;
        else if (wait && waitTime <= 0)
            wait = false;
    }
}
