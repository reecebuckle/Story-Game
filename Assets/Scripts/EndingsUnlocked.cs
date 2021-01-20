using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EndingsUnlocked
{
    private static bool ending1 = false;
    private static bool ending2 = false;
    private static bool ending3 = false;

    public static bool Ending1
    {
        get 
        {
            return ending1;
        }
        set 
        {
            ending1 = value;
        }
    }

    public static bool Ending2
    {
        get 
        {
            return ending2;
        }
        set 
        {
            ending1 = value;
        }
    }

    public static bool Ending3
    {
        get 
        {
            return ending3;
        }
        set 
        {
            ending1 = value;
        }
    }


}