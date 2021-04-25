using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Transform spawn;

    int count = 0;

    public GameObject part1;
    public GameObject part2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            count++;
            if(count == 10)
            {
                Destroy(gameObject);
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                part1.SetActive(false);
                part2.SetActive(true);
                rb.velocity = Vector2.one;
                rb.angularVelocity = 0;
                Camera.main.orthographicSize = 3;

            }
            else
            {
                collision.transform.position = spawn.position;
            }
        }
    }
}
