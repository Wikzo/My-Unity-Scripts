using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// from: http://letsmakeagame.net/level-creation-utility-unity/

public class MyTimeLine : MonoBehaviour
{
    protected MyTimeManager TimeManager = new MyTimeManager();

    protected void FindAndAssignAllTimeObjects()
    {
        ITimeUpdate[] allTimeObjects = GetComponentsInChildren<ITimeUpdate>();
        TimeManager.TimeObjects = new List<ITimeUpdate>(allTimeObjects);
    }
}
