using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Transform hitCollider;

    public float reloadTime;
    public float waitTime;

    bool wait = false;

    EnemyStats enemyStats;

    PlaySound ps;

    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        hitCollider.GetComponent<Strike>().damage = enemyStats.damage;
        ps = GetComponent<PlaySound>();
    }

    public void Attack()
    {
        if (!wait)
        {
            ps.Play("Bite");
            hitCollider.GetComponent<Strike>().CheckCollision(hitCollider);
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
}
