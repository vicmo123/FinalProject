using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

namespace AbilityFactoryPool
{
    public class AbilityObjectFactory
    {
        private Dictionary<AbilityType, AbilityComponent> resourceDict;
        private string folderPath = "Prefabs/Abilities/";
        private AbilityObjectPool pool;

        public AbilityObjectFactory(AbilityObjectPool pool)
        {
            this.pool = pool;
            Initialize();
        }

        private void Initialize()
        {
            resourceDict = new Dictionary<AbilityType, AbilityComponent>();
            List<AbilityType> enums = System.Enum.GetValues(typeof(AbilityType)).Cast<AbilityType>().ToList();

            foreach (var enumType in enums)
            {
                try
                {
                    GameObject prefab = LoadPrefab(enumType.ToString(), folderPath);
                    if (prefab == null)
                        Debug.LogError("Unable to load prefab of type : " + enumType.ToString() + " because it is null");
                    else
                        resourceDict.Add(enumType, prefab.GetComponent<AbilityComponent>());
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

        public AbilityComponent CreateObject(AbilityType abilityType)
        {
            AbilityComponent toRet = ReleaseObject(abilityType);
            if (toRet == null)
            {
                toRet = Instantiate(resourceDict[abilityType]).GetComponent<AbilityComponent>();
            }
            return toRet;
        }

        public GameObject Instantiate(AbilityComponent toCreate)
        {
            GameObject toRet = GameObject.Instantiate<GameObject>(toCreate.gameObject);
            return toRet;
        }

        private AbilityComponent ReleaseObject(AbilityType abilityType)
        {
            AbilityComponent toRet = pool.Depool(abilityType);
            if (toRet == null)
            {
                return null;
            }
            return toRet;
        }
    }
}
