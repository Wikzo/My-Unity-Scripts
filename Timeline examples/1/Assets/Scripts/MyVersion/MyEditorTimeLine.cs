using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode]
public class MyEditorTimeLine : MyTimeLine
{
    public float NowTime = 0;
    public bool SetZero = false;
    public bool SetNowAsZero = false;

    void OnEnable()
    {
        EditorApplication.playmodeStateChanged += HandleOnPlayMode;
    }

    void OnDisable()
    {
        if (EditorApplication.playmodeStateChanged != null)
            EditorApplication.playmodeStateChanged -= HandleOnPlayMode;
    }

    public void HandleOnPlayMode()
    {
        // This method is run whenever the playmode state is changed.

        if (EditorApplication.isPlaying)
            return;

        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            NowTime = 0;
            UpdateTime();
        }
    }

    void UpdateTime()
    {
        CheckSetZero();
        UpdateTimeManager();
        CheckSetNowAsZero();
    }


    private void UpdateTimeManager()
    {
        TimeManager.Time = NowTime;
        NowTime = TimeManager.Time;
    }

    private void CheckSetNowAsZero()
    {
        if (SetNowAsZero)
        {
            TimeManager.SetTimeBruteForce(0);
            NowTime = 0;
            SetNowAsZero = false;
        }
    }

    private void CheckSetZero()
    {
        if (SetZero)
        {
            NowTime = 0;
            SetZero = false;
        }
    }

    void Update()
    {
        if (EditorApplication.isPlaying)
            return;

        FindAndAssignAllTimeObjects();
        UpdateTime();
    }

}
