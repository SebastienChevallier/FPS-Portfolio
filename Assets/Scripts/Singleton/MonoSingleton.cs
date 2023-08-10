using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTemplate.Behaviours
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _sInstance;
        private static readonly object SLock = new object();

        public static T Instance
        {
            get
            {
                lock (SLock)
                {
                    if (_sInstance == null)
                    {   
                        Object[] instances = FindObjectsOfType(typeof(T));

                        if (instances.Length > 1)
                        {
                            Debug.LogWarning("MULTIPLE instances of \"" + typeof(T).Name + "\" in the scene");
                        }

                        if (instances == null || instances.Length == 0)
                        {
                            Debug.LogWarning("NO instance found for the type \"" + typeof(T).Name + "\"");
                            return null;
                        }

                        _sInstance = (T)instances[0];
                    }

                    return _sInstance;
                }
            }
        }
    }
}

