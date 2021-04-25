using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpHeight = 10f;

    int layersToIgnore = 1 << 8;

    Rigidbody2D rb;
    public Camera cam;

    Vector2 mousePos;

    General general;

    public Transform rotationPoint;
    public Transform groundCheck;

    public SpriteRenderer gun;

    Animator animator;

    PlaySound ps;

    private void Start()
    {
        general = GetComponent<General>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ps = GetComponent<PlaySound>();
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetKeyDown(KeyCode.Space) && CanJump())
            {
                ps.Play("Jump");
                rb.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
                if (animator.GetCurrentAnimation() != animator.idle)
                    animator.ChangeAnimation(animator.idle);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                if (animator.GetCurrentAnimation() != animator.walk)
                    animator.ChangeAnimation(animator.walk);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                if (animator.GetCurrentAnimation() != animator.walk)
                    animator.ChangeAnimation(animator.walk);
            }

            if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) && animator.GetCurrentAnimation() == animator.walk)
                animator.ChangeAnimation(animator.idle);
        }
    }

    void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            if (mousePos.x > transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            Vector2 lookDir = mousePos - new Vector2(rotationPoint.position.x, rotationPoint.position.y);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

            rotationPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    bool CanJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, layersToIgnore);
        if (hit.collider != null)
        {
            return true;
        }
        else
            return false;
    }


}
