using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Utilites
{
    public static class Timer
    {
        public static DateTime mLastTime;

        public static void Start()
        {
            mLastTime = DateTime.Now;
        }

        public static float GetElapsedSeconds()
        {
            DateTime now = DateTime.Now;
            TimeSpan elasped = now - mLastTime;
            mLastTime = now;
            return (float)elasped.Ticks / TimeSpan.TicksPerSecond;
        }
    }
}
