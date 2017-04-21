using UnityEngine;

namespace Util {
    public class PersistentSingleton<T> : MonoBehaviour
        where T : Component {
        private static T _instance;

        void Awake() {
            if (_instance == null || _instance == this) {
                DontDestroyOnLoad(gameObject);
                _instance = this as T;
            } else
                Destroy(gameObject);
        }

        public static T Instance {
            get {
                if (_instance == null) {
                    var objs = FindObjectsOfType(typeof(T)) as T[];
                    if (objs.Length > 0) {
                        if (objs.Length > 1) {
                            for (int i = 1; i < objs.Length; ++i)
                                Destroy(objs[i]);
                            _instance = objs[0] as T;
                        } else _instance = objs[0] as T;
                    } else {
                        GameObject gameobj = new GameObject(typeof(T).ToString());
                        _instance = gameobj.AddComponent<T>();
                    }
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
            private set { }
        }
    }
}