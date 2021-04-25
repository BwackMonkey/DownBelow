using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    float mousePos;

    float grabPos;
    public Transform backDrop;
    public Transform overlay;
    public Transform left;
    public Transform right;
    public float UpdateSlider()
    {
        mousePos = Input.mousePosition.x;

        gameObject.transform.position = new Vector3(mousePos, gameObject.transform.position.y, gameObject.transform.position.z);

        if (mousePos <= left.position.x)
            gameObject.transform.position = new Vector3(left.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        if (mousePos >= right.position.x)
            gameObject.transform.position = new Vector3(right.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        float halfWay;
        if (left.position.x - gameObject.transform.position.x != 0)
        {
            halfWay = left.position.x - (left.position.x - gameObject.transform.position.x) / 2;
        }
        else
            halfWay = left.position.x;

        overlay.position = new Vector3(halfWay, overlay.position.y, overlay.position.z);
        var t = gameObject.GetComponent<RectTransform>().anchoredPosition.x - left.gameObject.GetComponent<RectTransform>().anchoredPosition.x;
        overlay.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(t, overlay.GetComponent<RectTransform>().sizeDelta.y);

        float percent;
        if (left.position.x - gameObject.transform.position.x != 0)
        {
            percent = (gameObject.transform.position.x - Mathf.Abs(left.position.x)) / (right.position.x - Mathf.Abs(left.position.x));
        }
        else
        {
            percent = 0;
        }

        return percent;
    }

    public void UpdateSliderPercent(AudioSettings setting)
    {
        float percent = 0.5f;
        if (gameObject.tag == "Effect")
        {
            percent = setting.effectPercent;
        }
        if (gameObject.tag == "Music")
            percent = setting.musicPercent;

        Vector3 position = new Vector3(right.transform.position.x - ((right.position.x - left.position.x) * (1 - percent)), gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = position;

        float halfWay;
        if (left.position.x - gameObject.transform.position.x != 0)
        {
            halfWay = left.position.x - (left.position.x - gameObject.transform.position.x) / 2;
        }
        else
            halfWay = left.position.x;

        overlay.position = new Vector3(halfWay, overlay.position.y, overlay.position.z);
        var t = gameObject.GetComponent<RectTransform>().anchoredPosition.x - left.gameObject.GetComponent<RectTransform>().anchoredPosition.x;
        overlay.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(t, overlay.GetComponent<RectTransform>().sizeDelta.y);



    }
}
