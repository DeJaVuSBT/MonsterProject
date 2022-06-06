using System;
using UnityEngine;
public class TimerAction
{
    public static TimerAction Create(Action action, float timer) {

        GameObject actionUpdater = new GameObject("actionUpdater", typeof(Mono));
        TimerAction timerAction = new TimerAction(action, timer, actionUpdater);
        actionUpdater.GetComponent<Mono>().needUpdate = timerAction.Update;
        return timerAction;
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
    private TimerAction(Action action,float timer,GameObject gO) {
        this.action = action;
        this.timer = timer;
        this.gO = gO;
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
    }
}
