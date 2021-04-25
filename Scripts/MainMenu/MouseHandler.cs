using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseHandler : MonoBehaviour
{
    bool buttonHovering = false;

    GameObject text = null;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    ClickEvents clickEvents;

    string[] sliderParts = new string[] { "BackDrop", "Overlay", "Grab" };

    int layerMask = 9;

    AudioSettings audioSettings;

    GameManager gm;

    void Start()
    {
        layerMask = ~layerMask;
        clickEvents = GetComponent<ClickEvents>();
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
        audioSettings = FindObjectOfType<AudioSettings>();
    }

    private void Update()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count != 0)
        {
            ButtonVisuals visuals;
            if (results[0].gameObject.TryGetComponent<ButtonVisuals>(out visuals) && buttonHovering == false)
            {
                text = results[0].gameObject;
                text.GetComponent<ButtonVisuals>().Hovering();
                buttonHovering = true;
            }
        }

        if (results.Count == 0)
        {
            if (buttonHovering == true)
            {
                text.GetComponent<ButtonVisuals>().StoptHovering();
                text = null;
                buttonHovering = false;
            }

        }
        else
        {
            if (results[0].gameObject != text && buttonHovering == true)
            {
                text.GetComponent<ButtonVisuals>().StoptHovering();
                text = results[0].gameObject;
                buttonHovering = false;
            }
        }

        Click c;
        if (Input.GetMouseButtonDown(0) && results.Count != 0)
        {
            if (results[0].gameObject.TryGetComponent<Click>(out c))
            {
                clickEvents.Clicked(c);
            }
        }

        if (Input.GetMouseButton(0) && results.Count != 0 && CheckTag(results[0].gameObject.name, sliderParts))
        {
            var par = results[0].gameObject.transform.parent;
            foreach (Transform child in par.GetComponentInChildren<Transform>())
            {
                if (child.gameObject.name == "Grab")
                {
                    float percent = child.gameObject.GetComponent<Slider>().UpdateSlider();
                    if (child.gameObject.tag == "Effect")
                    {
                        audioSettings.SetEffect(percent);
                        audioSettings.UpdateEffect();
                    }
                    if (child.gameObject.tag == "Music")
                    {
                        audioSettings.SetMusic(percent);
                        audioSettings.UpdateMusic();
                    }
                }
            }
        }
    }

    bool CheckTag(string tag, string[] tags)
    {
        foreach (string item in tags)
        {
            if (item == tag)
                return true;
        }
        return false;
    }
}

