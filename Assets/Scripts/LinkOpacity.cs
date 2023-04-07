using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LinkOpacity : MonoBehaviour
{
    public float opacity;
    private SpriteRenderer[] spriteRenderers;
    private TextMeshPro[] textMeshPros;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        textMeshPros = GetComponentsInChildren<TextMeshPro>();
    }
    private void Update()
    {
        ChangeOpacity();
    }
    private void ChangeOpacity()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = opacity;
            spriteRenderer.color = spriteColor;
        }

        
        foreach (TextMeshPro textMeshPro in textMeshPros)
        {
            Color textColor = textMeshPro.color;
            textColor.a = opacity;
            textMeshPro.color = textColor;
        }
    }
}