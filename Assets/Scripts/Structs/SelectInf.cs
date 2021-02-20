using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct SelectInf
{
    static List<SelectInf> instances = new List<SelectInf>();

    public ElementType type;
    public int index;

    public SelectInf(ElementType type, int index)
    {
        this.type = type;
        this.index = index;
        instances.Add(this);
    }

    public static SelectInf Create(ElementType type)
    {
        int biggest = 0;
        foreach (SelectInf instance in instances)
        {
            if (instance.type == type)
            {
                if (instance.index > biggest)
                {
                    biggest = instance.index;
                }
            }
        }
        return new SelectInf(type, biggest + 1);
    }

    public static void AddSelectInf(SelectInf inf)
    {
        instances.Add(inf);
    }

    public override bool Equals(object obj)
    {
        return obj is SelectInf inf &&
               type == inf.type &&
               index == inf.index;
    }

    public override int GetHashCode()
    {
        int hashCode = 186638336;
        hashCode = hashCode * -1521134295 + type.GetHashCode();
        hashCode = hashCode * -1521134295 + index.GetHashCode();
        return hashCode;
    }

    public static bool operator ==(SelectInf l, SelectInf r)
    {
        return l.Equals(r);
    }

    public static bool operator !=(SelectInf l, SelectInf r)
    {
        return !l.Equals(r);
    }

    public override string ToString()
    {
        return type.ToString().Substring(0, 1) + type.ToString().Substring(1).ToLower() + " " + index.ToString();
    }

    public static SelectInf none
    {
        get
        {
            return new SelectInf(ElementType.NONE, 0);
        }
    }
}

public enum ElementType
{
    NODE,
    CONN,
    INTER,
    NONE
}