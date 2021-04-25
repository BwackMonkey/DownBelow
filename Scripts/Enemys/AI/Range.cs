using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    LayerMask layersToIgnore = ~(1 << 9 | 1 << 10);

    GameObject player;

    RangeAttack rA;

    EnemyStats enemyStats;

    Animator animator;

    public bool move;

    PlaySound ps;

    System.Random rand = new System.Random();

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rA = GetComponent<RangeAttack>();
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
                    if (!double.IsNaN(rA.Angle()))
                    {
                        rA.Fire();
                    }
                    else if (move)
                        MoveTowardsPlayer();
                }

            }
            else
            {
                if (animator.GetCurrentAnimation() != animator.idle)
                    animator.ChangeAnimation(animator.idle);
            }
        }

    }

    void MoveTowardsPlayer()
    {
        if(!ps.IsPlaying())
            ps.Play("Idle");

        if(animator.GetCurrentAnimation() != animator.walk)
            animator.ChangeAnimation(animator.walk);

        if (player.transform.position.x < transform.position.x)
        {
            transform.position = new Vector2(transform.position.x - enemyStats.moveSpeed * Time.deltaTime, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            transform.position = new Vector2(transform.position.x + enemyStats.moveSpeed * Time.deltaTime, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
