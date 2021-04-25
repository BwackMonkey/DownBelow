using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    public float damage;
    public void CheckCollision(Transform hitPoint)
    {
        Collider2D[] collision = Physics2D.OverlapBoxAll(hitPoint.position, new Vector2(1,1), 90);
        foreach(Collider2D col in collision)
        {
            if (col.tag == "Player" && this.gameObject.tag != "Player")
                col.gameObject.GetComponent<PlayerStats>().Hit(damage);
            if(col.gameObject.layer == 9 && this.gameObject.layer != 9)
                col.gameObject.GetComponent<EnemyStats>().Hit(damage);
        }
    }
}
