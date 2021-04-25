using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScreen", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
