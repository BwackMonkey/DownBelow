using System;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvents : MonoBehaviour
{

    static GameObject musicBox;

    static Click btn;

    static GameManager gm;

    private void Start()
    {
        try
        {
            gm = FindObjectOfType<GameManager>();
        }
        catch { }
    }


    public void Clicked(Click obj)
    {
        try
        {
            obj.GetComponent<PlaySound>().Play("Click");
        }
        catch { }

        if (obj.GetComponent<Click>().type == "QuitGameBtn")
            Application.Quit();
        else
        {
            btn = obj;
            ButtonEvents[btn.type]();
        }
    }



    Dictionary<string, Action> ButtonEvents = new Dictionary<string, Action>()
    {
        {"StartBtn",() => StartBtn()},
        {"MainMenuBtn",() => MainMenuBtn()},
        {"MenuClose",() => MenuClose()},
        {"Button",() => Button()},
    };

    static void StartBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    static void MainMenuBtn()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    static void MenuClose()
    {
        btn.parent.SetActive(false);
        gm.Pause();
    }

    static void Button()
    {
        if (btn.next != null)
            btn.next.SetActive(true);
        if (btn.parent != null)
            btn.parent.SetActive(false);
    }
}
