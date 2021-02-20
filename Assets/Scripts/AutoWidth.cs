using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoWidth : MonoBehaviour
{
    public Text text;
    public RectTransform rectTransform;
    public BoxCollider2D coll;

    private void Update()
    {
        int len = LengthOfText(text);
        if (len == 0)
        {
            len = LengthOfEnterText(text);
        }
        rectTransform.sizeDelta = new Vector2(len + 30, 40);
        coll.size = new Vector2(len + 30, coll.size.y);
    }

    static int LengthOfText(Text itext)
    {
        int totalLength = 0;
        Font myFont = itext.font;
        myFont.RequestCharactersInTexture(itext.text, itext.fontSize, itext.fontStyle);
        CharacterInfo characterInfo = new CharacterInfo();

        char[] arr = itext.text.ToCharArray();

        foreach (char c in arr)
        {
            myFont.GetCharacterInfo(c, out characterInfo, itext.fontSize);

            totalLength += characterInfo.advance;
        }

        return totalLength;
    }

    static int LengthOfEnterText(Text itext)
    {
        int totalLength = 0;
        Font myFont = itext.font;
        myFont.RequestCharactersInTexture("Enter text...", itext.fontSize, itext.fontStyle);
        CharacterInfo characterInfo = new CharacterInfo();

        char[] arr = "Enter text...".ToCharArray();

        foreach (char c in arr)
        {
            myFont.GetCharacterInfo(c, out characterInfo, itext.fontSize);

            totalLength += characterInfo.advance;
        }

        return totalLength;
    }
}
