using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonVisuals : MonoBehaviour
{
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void Hovering()
    {
        text.fontSize += 3;
    }

    public void StoptHovering()
    {
        text.fontSize -= 3;
    }
}
