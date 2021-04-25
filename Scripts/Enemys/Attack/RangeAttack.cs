using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public Transform rotationPoint;
    public Transform firePoint;
    public GameObject bullet;

    GameObject player;

    public float bulletForce;
    public float reloadTime;

   float waitTime;

    bool wait = false;

    EnemyStats enemyStats;

    PlaySound ps;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyStats = GetComponent<EnemyStats>();
        ps = GetComponent<PlaySound>();
    }
    public void Fire()
    {
        if (!wait)
        {
            LookTowardsPlayer();
            GameObject bulletInst = Instantiate(bullet, firePoint.position, firePoint.rotation);
            bulletInst.GetComponent<Bullet>().damage = enemyStats.damage;
            bulletInst.GetComponent<Bullet>().refrence = this.gameObject;
            bulletInst.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            ps.Play("Spit");
            wait = true;
            waitTime = reloadTime;
        }
    }

    private void Update()
    {
        if (wait && waitTime > 0)
            waitTime -= Time.deltaTime;
        else if (wait && waitTime <= 0)
            wait = false;
    }

    void LookTowardsPlayer()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 lookDir = playerPos - new Vector2(rotationPoint.position.x, rotationPoint.position.y);
        if (playerPos.x < transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
        else
            GetComponent<SpriteRenderer>().flipX = true;

        if (Mathf.Abs(lookDir.x) < 0.1)
            rotationPoint.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        else
        {
            float angle = Angle() * Mathf.Rad2Deg;

            if (GetComponent<SpriteRenderer>().flipX == false)
                rotationPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            else
                rotationPoint.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        }
    }

    public float Angle()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 lookDir = playerPos - new Vector2(rotationPoint.position.x, rotationPoint.position.y);
        float y = Mathf.Abs(lookDir.y);
        float x = Mathf.Abs(lookDir.x);
        float a = ((9.81f * Mathf.Pow(x, 2)) / Mathf.Pow(bulletForce, 2)) - y;
        float b = Mathf.Sqrt(Mathf.Pow(y, 2) + Mathf.Pow(x, 2));
        float c = Mathf.Acos(a / b);
        float d = Mathf.Atan(x / y);
        float angle = (c + d) / 2;
       
        return angle;

    }
}
