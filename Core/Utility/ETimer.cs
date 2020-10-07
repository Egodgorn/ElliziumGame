using System;


namespace Ellizium.Core.Utility
{
    public class ETimer
    {
        private Action Callback { get; set; }

        public float NeedTime { get; set; }
        public float CurrentTime { get; set; } = 0;
        public bool IsRepeat { get; set; }
        public bool IsPhysics { get; set; }
        public int CountRepeat { get; set; } = -1;

        public bool IsActive { get; private set; } = false;

        public ETimer(float needTime, bool isRepeat, bool isPhysics, Action callBack)
        {
            NeedTime = needTime;
            IsRepeat = isRepeat;
            Callback = callBack;
            IsPhysics = isPhysics;
        }
        public ETimer(float needTime, bool isRepeat, bool isPhysics, int countRepeat, Action callBack)
        {
            NeedTime = needTime;
            IsRepeat = isRepeat;
            CountRepeat = countRepeat - 1;
            Callback = callBack;
            IsPhysics = isPhysics;
        }
        public void Start()
        {
            if (IsPhysics)
            {
                if (IsRepeat)
                {
                    if (CountRepeat != -1)
                    {
                        Main.Physics += UpdateCoutRepeat;
                    }
                    else
                    {
                        Main.Physics += UpdateRepeat;
                    }
                }
                else
                {
                    Main.Physics += Update;
                }
            }
            else
            {
                if (IsRepeat)
                {
                    if (CountRepeat != -1)
                    {
                        Main.Render += UpdateCoutRepeat;
                    }
                    else
                    {
                        Main.Render += UpdateRepeat;
                    }
                }
                else
                {
                    Main.Render += Update;
                }
            }

            IsActive = true;
        }
        public void Stop(bool resetTime)
        {
            if (IsPhysics)
            {
                if (IsRepeat)
                {
                    if (CountRepeat != -1)
                    {
                        if (resetTime)
                            CurrentTime = 0;
                        Main.Physics += UpdateCoutRepeat;
                    }
                    else
                    {
                        if (resetTime)
                            CurrentTime = 0;
                        Main.Physics += UpdateRepeat;
                    }
                }
                else
                {
                    if (resetTime)
                        CurrentTime = 0;
                    Main.Physics += Update;
                }
            }
            else
            {
                if (IsRepeat)
                {
                    if (CountRepeat != -1)
                    {
                        if (resetTime)
                            CurrentTime = 0;
                        Main.Render += UpdateCoutRepeat;
                    }
                    else
                    {
                        if (resetTime)
                            CurrentTime = 0;
                        Main.Render += UpdateRepeat;
                    }
                }
                else
                {
                    if (resetTime)
                        CurrentTime = 0;
                    Main.Render += Update;
                }
            }

            IsActive = false;
        }
        private void Update(float delta)
        {
            if (CurrentTime >= NeedTime)
            {
                Stop(true);
                Callback();
                return;
            }

            CurrentTime += delta;
        }
        private void UpdateRepeat(float delta)
        {
            if (CurrentTime >= NeedTime)
            {
                CurrentTime = 0;
                Callback();
            }

            CurrentTime += delta;
        }
        private void UpdateCoutRepeat(float delta)
        {
            if (CurrentTime >= NeedTime)
            {
                if (CountRepeat > 0)
                {
                    CountRepeat--;
                    CurrentTime = 0;
                    Callback();
                }
                else
                {
                    Callback();
                    Stop(true);
                    return;
                }
            }

            CurrentTime += delta;
        }
    }
}
