using UnityEngine;
using System.Collections;
using System;

public static class ExtensionMethods
{
    public static int WordCount(this string str)
    {
        return str.Split(new char[] { ' ', '.', '?' },
                            StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public static void SetActiveIfNeeded(this GameObject obj, bool active)
    {
        if(obj.activeInHierarchy != active)
        {
            obj.SetActive(active);
        }
    }
    public static long ToLong(this DateTime dt) { return dt.Ticks; }

    public static long ToSeconds(this long l) { return l / 10000000; }
}
