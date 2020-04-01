using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My {
    public class Timer {
        private bool mRun;
        private float mCurTime;
        private float mTime;
        private string mTimeName;
        private event System.Action completeAction;
        public Timer(string name,float time,System.Action action) {
            this.mCurTime=0f;
            this.mTime=time;
            this.mTimeName=name;
            this.mRun=false;
            SetCompleteAction(action);
        }

        private void SetCompleteAction(System.Action action) {
            this.completeAction+=action;
            this.completeAction+=() => { TimeManager.Instance.RemoveTimer(this); };
        }

        public Timer Start() {
            mRun=true;
            return this;
        }

        public Timer Stop() {
            mRun=false;
            return this;
        }

        public Timer Continue() {
            mRun=true;
            return this;
        }

        public Timer Restart() {
            mRun=true;
            mCurTime=0f;
            return this;
        }

        public void Update(float deltaTime) {
            if (mRun) {
                mCurTime+=deltaTime;
                if (mCurTime>=mTime) {
                    mRun=false;
                    completeAction?.Invoke();
                }
            }
        }
    }

    public class TimeManager:MonoSingleton<TimeManager> {
        private List<Timer> timers;

        protected override void Init() {
            base.Init();
            timers=new List<Timer>();
        }
        public Timer CreateTimer(string name,float time,System.Action action) {
            Timer timer = new Timer(name,time,action);
            timers.Add(timer);
            return timer;
        }

        public void RemoveTimer(Timer timer) {
            timers.Remove(timer);
        }


        public void Update() {
            for (int i = timers.Count-1;i>=0;i--) {
                timers[i].Update(Time.deltaTime);
            }
        }

    }
}

