using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClickTest : MonoBehaviour
{

    float interval = 0.25f;
    float doubleClickedTime = -1.0f;
    bool pickerOnOff = false;

    private void OnMouseUp()
    {
        if((Time.time-doubleClickedTime) < interval)
        {
            doubleClickedTime = -1.0f;
            if(ColorPalette.Instance.gameObject.activeSelf == false)
            {
                pickerOnOff = true;
                ColorPalette.Instance.gameObject.SetActive(true);

                ColorPalette.Instance.linkedObject = this.gameObject;
            }
            else
            {
                if (ColorPalette.Instance.linkedObject.name != this.gameObject.name)
                    return;
                pickerOnOff = false;
                ColorPalette.Instance.gameObject.SetActive(false);
                ColorPalette.Instance.linkedObject = null;
            }
        }
        else
        {
            doubleClickedTime = Time.time;
        }
    }

    void Update()
    {
        if(pickerOnOff)
        {
            ColorPalette.Instance.gameObject.transform.position
                = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        }
    }
}
