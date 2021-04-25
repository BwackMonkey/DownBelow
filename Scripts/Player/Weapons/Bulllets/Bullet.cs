using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float time = 5f;
    public float damage;
    public GameObject refrence;
    bool hit = false;
    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.layer == 9 && refrence.gameObject.layer != 9 && !hit)
            {
                collision.gameObject.GetComponent<EnemyStats>().Hit(damage);
                hit = true;
                Destroy(gameObject);
            }
            if (collision.gameObject.tag == "Player" && refrence.gameObject.tag != "Player" && !hit)
            {
                collision.gameObject.GetComponent<PlayerStats>().Hit(damage);
                hit = true;
                Destroy(gameObject);
            }
            if (collision.gameObject.layer == 8)
                Destroy(gameObject);
        }
        catch { }
    }
}
