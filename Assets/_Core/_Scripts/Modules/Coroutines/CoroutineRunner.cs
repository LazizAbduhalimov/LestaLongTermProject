using System.Collections;
using UnityEngine;

namespace Modules.Coroutines
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;

        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    var runnerObject = new GameObject("[CoroutineRunner]");
                    _instance = runnerObject.AddComponent<CoroutineRunner>();
                    DontDestroyOnLoad(runnerObject);
                }

                return _instance;
            }
        }

        public static Coroutine Run(IEnumerator coroutine)
        {
            return Instance.StartCoroutine(coroutine);
        }

        public static void Stop(Coroutine coroutine)
        {
            if (_instance != null && coroutine != null)
            {
                _instance.StopCoroutine(coroutine);
            }
        }

        public static void StopAll()
        {
            if (_instance != null)
            {
                _instance.StopAllCoroutines();
            }
        }
    }
}
