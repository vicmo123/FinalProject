using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupCanManager
{
    private int canCount = 0;

    public SyrupCanManager()
    {
        canCount = 0;
    }

    public int GetCanCount()
    {
        return canCount;
    }

    public void AddCan()
    {
        canCount++;
    }

    public void RemoveCan()
    {
        if(canCount > 0)
        {
            canCount--;
        }
    }
}
