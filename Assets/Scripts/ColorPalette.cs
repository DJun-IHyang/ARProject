using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Generated.PropertyProviders;
using UnityEngine;
using UnityEngine.UI;

public class ColorPalette : MonoBehaviour
{
    private static ColorPalette instance = null;

    public Image circlePalette;
    public Image picker;
    public Color selectedColor;
    public GameObject linkedObject;

    private Vector2 sizeOfPalette;
    private CircleCollider2D paletteCollider;

    public static ColorPalette Instance
    {
        get
        {
            if (null == instance)
                instance = FindObjectOfType<ColorPalette>();
            return instance;
        }
    }

    private void Awake()
    {
        if (null == instance) instance = this;
    }

    void Start()
    {
        this.gameObject.SetActive(false);

        paletteCollider = circlePalette.GetComponent<CircleCollider2D>();

        sizeOfPalette = new Vector2
            (circlePalette.GetComponent<RectTransform>().rect.width,
            circlePalette.GetComponent<RectTransform>().rect.height);
        
    }

    public void MousePointerDown()
    {
        SelectedColor();
    }

    public void MouseDrag()
    {
        SelectedColor();
    }

    private void SelectedColor()
    {
        Vector3 offset = Input.mousePosition - transform.position;
        Vector3 diff = Vector3.ClampMagnitude(offset, paletteCollider.radius);

        picker.transform.position = transform.position + diff;

        selectedColor = GetColor();

        linkedObject.GetComponent<Image>().color = selectedColor;
    }

    private Color GetColor()
    {
        Vector2 circlePalettePosition = circlePalette.transform.position;
        Vector2 pickerPosition = picker.transform.position;

        Vector2 position = pickerPosition - circlePalettePosition + sizeOfPalette * 0.5f;
        Vector2 normalized = new Vector2(
            (position.x / (circlePalette.GetComponent<RectTransform>().rect.width)),
            (position.y / (circlePalette.GetComponent<RectTransform>().rect.height)));

        Texture2D texture = circlePalette.mainTexture as Texture2D;
        Color circularSelectedColor = texture.GetPixelBilinear(normalized.x, normalized.y);

        return circularSelectedColor;
    }
}
