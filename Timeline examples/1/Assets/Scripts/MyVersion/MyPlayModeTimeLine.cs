using UnityEngine;
using System.Collections;

public class MyPlayModeTimeLine : MyTimeLine
{
    public float NowTime;

    void Start()
    {
        FindAndAssignAllTimeObjects();
    }

    void Update()
    {
        NowTime = Time.time;
        TimeManager.Time = NowTime;
    }
}
