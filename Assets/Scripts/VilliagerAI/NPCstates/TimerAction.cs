using System;
using UnityEngine;
using System.Collections.Generic;
public class TimerAction
{
    private static List<TimerAction> activedTimerList;
    private static GameObject initGO;
    private static void Init()
    {
        if (initGO==null)
        {
            initGO = new GameObject("TimerGO");
            activedTimerList = new List<TimerAction>();
        }

    }

    public static TimerAction Create(Action action, float timer,string timerName=null) {
        Init();
        GameObject actionUpdater = new GameObject("actionUpdater", typeof(Mono));
        TimerAction timerAction = new TimerAction(action, timer, timerName,actionUpdater);
        actionUpdater.GetComponent<Mono>().needUpdate = timerAction.Update;
        activedTimerList.Add(timerAction);
        return timerAction;
    }

    private static void RemoveTimer(TimerAction TA) {
        Init();
        activedTimerList.Remove(TA);
    }
    public static void StopTimer(string timerName) {
        for (int i = 0; i < activedTimerList.Count; i++)
        {
            if (activedTimerList[i].timerName== timerName)
            {
                activedTimerList[i].Stop();
                i--;
            }
        }
    }
    public class Mono : MonoBehaviour {
        public Action needUpdate;
        private void Update()
        {
            if (needUpdate!=null)
            {
                needUpdate();
            }
        }
    }
    private Action action;
    private float timer;
    private GameObject gO;
    private bool stop = false;
    private string timerName;
    private TimerAction(Action action,float timer, string timerName,GameObject gO) {
        this.action = action;
        this.timer = timer;
        this.gO = gO;
        this.timerName = timerName;
    }

    public void Update() {
        if (!stop)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                action();
                Stop();
            }
        }
    }

    private void Stop() {
        stop = true;
        GameObject.Destroy(gO);
        RemoveTimer(this);
    }
}
