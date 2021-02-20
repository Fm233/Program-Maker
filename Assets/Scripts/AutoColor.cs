using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoColor : MonoBehaviour
{
    public InputField input;

    Color red1
    {
        get
        {
            return Get256Color(255, 149, 149);
        }
    }
    Color red2
    {
        get
        {
            return Get256Color(255, 82, 82);
        }
    }
    Color blue1
    {
        get
        {
            return Get256Color(142, 221, 249);
        }
    }
    Color blue2
    {
        get
        {
            return Get256Color(80, 195, 247);
        }
    }
    Color gray
    {
        get
        {
            return Get256Color(173, 173, 173);
        }
    }
    Color green1
    {
        get
        {
            return Get256Color(142, 222, 153);
        }
    }
    Color pink
    {
        get
        {
            return Get256Color(255, 171, 213);
        }
    }

    void Start()
    {

    }

    public void UpdateColor()
    {
        if (input.text.StartsWith("In"))
        {
            if (input.text.StartsWith("Ins"))
            {
                SetInputColor(red2);
                return;
            }
            SetInputColor(red1);
            return;
        }
        if (input.text.StartsWith("Con"))
        {
            SetInputColor(green1);
            return;
        }
        if (input.text.StartsWith("Gate"))
        {
            SetInputColor(green1);
            return;
        }
        if (input.text.StartsWith("Pre"))
        {
            SetInputColor(green1);
            return;
        }
        if (input.text.StartsWith("Out"))
        {
            SetInputColor(blue1);
            return;
        }
        if (input.text.StartsWith("Fac"))
        {
            SetInputColor(blue2);
            return;
        }
        if (input.text.StartsWith("Use"))
        {
            SetInputColor(pink);
            return;
        }
        SetInputColor(gray);
    }

    Color Get256Color(int a, int b, int c)
    {
        return new Color(a / 256f, b / 256f, c / 256f);
    }

    void SetInputColor(Color color)
    {
        ColorBlock cb = input.colors;
        cb.normalColor = color;
        input.colors = cb;
    }
}