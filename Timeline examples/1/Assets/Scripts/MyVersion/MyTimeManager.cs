using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyTimeManager
{
    public List<ITimeUpdate> TimeObjects { get; set; }

    public MyTimeManager ()
    {
        TimeObjects = new List<ITimeUpdate>();
    } 
    private float _time;

    public float Time
    {
        get { return _time; }
        set
        {
            if (value != _time)
            {
                float _deltaTime = value - _time;
                foreach (ITimeUpdate t in TimeObjects)
                    t.AddTime(_deltaTime);

                _time = value;
            }
        }
    }

    public void SetTimeBruteForce(float time)
    {
        this._time = time;
    }
}
