using System;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    LayerMask layersToIgnore = ~(1 << 9 | 1 << 10);

    GameObject player;

    MeleeAttack mA;

    EnemyStats enemyStats;

    public GameObject hit;

    PlaySound ps;

    System.Random rand = new System.Random();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mA = GetComponent<MeleeAttack>();
        enemyStats = GetComponent<EnemyStats>();
        ps = GetComponent<PlaySound>();

    }

    private void Update()
    {
        if (enemyStats.health <= 0)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Mathf.Infinity, layersToIgnore);
            if (hit.collider != null)
            {
                if (hit.collider.transform.tag == "Player")
                {
                    if (hit.distance <= enemyStats.range)
                    {
                        mA.Attack();
                    }
                    else
                        MoveTowardsPlayer();
                }

            }
        }

    }

    void MoveTowardsPlayer()
    {
        if(!ps.IsPlaying())
            ps.Play("Idle");

        if (player.transform.position.x < transform.position.x)
        {
            transform.position = new Vector2(transform.position.x - enemyStats.moveSpeed * Time.deltaTime, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = false;
            hit.transform.localPosition = new Vector2(-Mathf.Abs(hit.transform.localPosition.x), hit.transform.localPosition.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x + enemyStats.moveSpeed * Time.deltaTime, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = true;
            hit.transform.localPosition = new Vector2(Mathf.Abs(hit.transform.localPosition.x), hit.transform.localPosition.y);
        }
    }
}
