using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Log
    {
        public enum Level
        {
            DEBUG,
            INFO,
            WARN,
            ERROR
        }

        public static bool Slient = false;
        public static Level filterLevel = Level.INFO;
        public static Action<string> OnOutputEvent;

        public static void Assert(bool condition, string assertString, bool pauseOnFail = false)
        {
            if (!condition)
            {
                Error("Assert Failed! {0}", assertString);
            }
        }

        public static void Debug(string msg, params System.Object[] args)
        {
            Output(Level.DEBUG, msg, args);
        }

        public static void Info(string msg, params System.Object[] args)
        {
            Output(Level.INFO, msg, args);
        }

        public static void Warn(string msg, params System.Object[] args)
        {
            Output(Level.WARN, msg, args);
        }

        public static void Error(string msg, params System.Object[] args)
        {
            Output(Level.ERROR, msg, args);
        }

        public static void Exception(Exception e)
        {
            Error(e.Message + "\n" + e.StackTrace);
        }

        public static void Binary(byte[] buf, int offset, int size)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < size; i++)
            {
                sb.Append(buf[i + offset]);
                sb.Append(",");
            }
            sb.Append("]");
            Info(sb.ToString());
        }

        private static string Prefix(Level level)
        {
#if SERVER
        return level.ToString();
#elif UNITY_EDITOR
            var now = DateTime.Now;
            return now.ToString("yyyy-MM-dd HH:mm:ss") + "." + now.Millisecond;
#else
#error must define one of SERVER or UNITY
#endif
        }

        private static void Output(Level level, string msg, params System.Object[] args)
        {
            if (args != null && args.Length > 0)
            {
                msg = string.Format(msg, args);
            }
            msg = string.Format("[{0}]{1}", Prefix(level), msg);
            switch (level)
            {
                case Level.DEBUG:
                    if (!Slient)
                    {
#if SERVER
                    System.Console.WriteLine(msg);
#elif UNITY_EDITOR
                        UnityEngine.Debug.Log(msg);
#else
#error must define one of SERVER or UNITY
#endif
                    }
                    if (OnOutputEvent != null)
                    {
                        OnOutputEvent(msg);
                    }
                    break;
                case Level.INFO:
                    if (!Slient)
                    {
#if SERVER
                    System.Console.WriteLine(msg);
#elif UNITY_EDITOR
                        UnityEngine.Debug.Log(msg);
#else
#error must define one of SERVER or UNITY
#endif
                    }
                    if (OnOutputEvent != null)
                    {
                        OnOutputEvent(msg);
                    }
                    break;
                case Level.WARN:
                    if (!Slient)
                    {
#if SERVER
                    System.Console.WriteLine(msg);
#elif UNITY_EDITOR
                        UnityEngine.Debug.LogWarning(msg);
#else
#error must define one of SERVER or UNITY
#endif
                    }
                    if (OnOutputEvent != null)
                    {
                        OnOutputEvent(msg);
                    }
                    break;
                case Level.ERROR:
                    if (!Slient)
                    {
#if SERVER
                    System.Console.WriteLine(msg);
#elif UNITY_EDITOR
                        UnityEngine.Debug.LogError(msg);
#else
#error must define one of SERVER or UNITY
#endif
                    }
                    if (OnOutputEvent != null)
                    {
                        OnOutputEvent(msg);
                    }
                    break;
            }
        }
    }

}

