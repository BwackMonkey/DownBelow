using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    public Sprite[] walk;
    public Sprite[] attack;
    public Sprite[] idle;


    public float delay;

    private IEnumerator coroutine;

    Sprite[] currentAnimation;
    private void Start()
    {
        coroutine = Play(idle);
        currentAnimation = idle;
        StartCoroutine(coroutine);
    }

    public void ChangeAnimation(Sprite[] animation)
    {
        StopCoroutine(coroutine);
        coroutine = Play(animation);
        currentAnimation = animation;
        StartCoroutine(coroutine);
    }

    public Sprite[] GetCurrentAnimation()
    {
        return currentAnimation;
    }

    IEnumerator Play(Sprite[] imgs)
    {
        while (true)
        {
            for (int i = 0; i < imgs.Length; i++)
            {
                if(gameObject.GetComponent<SpriteRenderer>().sprite != imgs[i])
                    gameObject.GetComponent<SpriteRenderer>().sprite = imgs[i];

                yield return new WaitForSeconds(delay);
            }
        }
    }
}
