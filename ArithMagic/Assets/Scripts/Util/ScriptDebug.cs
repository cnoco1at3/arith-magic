using UnityEngine;
using System.Collections;

namespace Util {
    public static class ScriptDebug {
        public static void Log(Object type, int line, string msg) {
            Debug.Log(type.GetType().Name + "(" + line + ") : " + msg);
        }
    }
}
