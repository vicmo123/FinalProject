using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

namespace ThrowableFactoryPool
{
    public class ThrowableFactory
    {
        private Dictionary<AbilityType, Throwable> resourceDict;
        private string folderPath = "Prefabs/Throwables/";
        private ThrowablePool pool;

        public ThrowableFactory(ThrowablePool pool)
        {
            this.pool = pool;
            Initialize();
        }

        private void Initialize()
        {
            resourceDict = new Dictionary<AbilityType, Throwable>();
            List<AbilityType> enums = System.Enum.GetValues(typeof(AbilityType)).Cast<AbilityType>().ToList();

            foreach (var enumType in enums)
            {
                try
                {
                    GameObject prefab = LoadPrefab(enumType.ToString(), folderPath);
                    if (prefab == null)
                        Debug.LogError("Unable to load prefab of type : " + enumType.ToString() + " because it is null");
                    else
                        resourceDict.Add(enumType, prefab.GetComponent<Throwable>());
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Unable to load prefab of type : " + enumType.ToString());
                }
            }
        }

        public GameObject LoadPrefab(string path, string folder = null)
        {
            return Resources.Load<GameObject>(folder + path);
        }

        public Throwable CreateObject(AbilityType throwableName)
        {
            Throwable toRet = ReleaseObject(throwableName);
            if (toRet == null)
            {
                toRet = Instantiate(resourceDict[throwableName]).GetComponent<Throwable>();
            }
            return toRet;
        }

        public GameObject Instantiate(Throwable toCreate)
        {
            GameObject toRet = GameObject.Instantiate<GameObject>(toCreate.gameObject);
            return toRet;
        }

        private Throwable ReleaseObject(AbilityType throwableName)
        {
            Throwable toRet = pool.Depool(throwableName);
            if (toRet == null)
            {
                return null;
            }
            return toRet;
        }
    }
}

