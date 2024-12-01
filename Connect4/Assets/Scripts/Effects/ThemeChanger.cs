using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeChanger : MonoBehaviour
{
    public GameObject[] backgroundObjects;
    public string _currentTheme = "blue";
    
    [SerializeField] private GameObject Background;
    [SerializeField] private Sprite _blueBackground;
    [SerializeField] private Sprite _pastelBackground;

    public Color _blueBackgroundColor;
    public Color _pastelBackgroundColor;
    
    private bool isBlueTheme = true;

    
    private void Start()
    {
        SetColor();
        backgroundObjects = GameObject.FindGameObjectsWithTag("Theme");
        Background.GetComponent<SpriteRenderer>().sprite = _blueBackground;
    }

    public void SetTheme(string theme)
    {
        _currentTheme = theme;
        SetColor();
    }
    

    public void ToggleTheme()
    {
        switch (isBlueTheme)
        {
            case true:
                SetTheme("blue");
                break;
            case false:
                SetTheme("pastel");
                break;
        }

        isBlueTheme = !isBlueTheme;
    }

    private void SetColor()
    {
        if (_currentTheme == "blue")
        {
            Background.GetComponent<SpriteRenderer>().sprite = _blueBackground;
            foreach (var bg in backgroundObjects)
            {
                var spriterenderer = bg.GetComponent<SpriteRenderer>();
                spriterenderer.color = _blueBackgroundColor;
            }
        }
        else if (_currentTheme == "pastel")
        {
            Background.GetComponent<SpriteRenderer>().sprite = _pastelBackground;
            foreach (var bg in backgroundObjects)
            {
                var spriterenderer = bg.GetComponent<SpriteRenderer>();
                spriterenderer.color = _pastelBackgroundColor;
            }
        }
    }
}
