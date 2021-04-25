using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float moveSpeed;
    public float damage;
    public float range;

    public float startHealth;

    public Image overlay;

    PlaySound ps;

    private void Start()
    {
        ps = GetComponent<PlaySound>();
    }

    public void Hit(float damage)
    {
        ps.Play("Hit");
        health -= damage;
        overlay.fillAmount = health / startHealth;
        StartCoroutine("HitWait");
    }

    private IEnumerator HitWait()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }



}
