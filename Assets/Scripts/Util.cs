using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static string ToSmallCamel(string inp)
    {
        return inp.Substring(0, 1).ToLower() + inp.Substring(1);
    }
    public static string ToBigCamel(string inp)
    {
        return inp.Substring(0, 1).ToUpper() + inp.Substring(1);
    }
}
