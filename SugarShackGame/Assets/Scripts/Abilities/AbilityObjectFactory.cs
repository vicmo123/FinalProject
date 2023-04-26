using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

namespace AbilityFactoryPool
{
    public class AbilityObjectFactory
    {
<<<<<<< HEAD:SugarShackGame/Assets/Scripts/Throwing/ThrowableFactory.cs
        private Dictionary<AbilityType, Throwable> resourceDict;
        private string folderPath = "Prefabs/Throwables/";
        private ThrowablePool pool;
=======
        private Dictionary<AbilityType, AbilityComponent> resourceDict;
        private string folderPath = "Prefabs/Abilities/";
        private AbilityObjectPool pool;
>>>>>>> 17a14d23657b327dcba3a2eae271288ba8a22c72:SugarShackGame/Assets/Scripts/Abilities/AbilityObjectFactory.cs

        public AbilityObjectFactory(AbilityObjectPool pool)
        {
            this.pool = pool;
            Initialize();
        }

        private void Initialize()
        {
<<<<<<< HEAD:SugarShackGame/Assets/Scripts/Throwing/ThrowableFactory.cs
            resourceDict = new Dictionary<AbilityType, Throwable>();
=======
            resourceDict = new Dictionary<AbilityType, AbilityComponent>();
>>>>>>> 17a14d23657b327dcba3a2eae271288ba8a22c72:SugarShackGame/Assets/Scripts/Abilities/AbilityObjectFactory.cs
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

<<<<<<< HEAD:SugarShackGame/Assets/Scripts/Throwing/ThrowableFactory.cs
        public Throwable CreateObject(AbilityType throwableName)
=======
        public AbilityComponent CreateObject(AbilityType abilityType)
>>>>>>> 17a14d23657b327dcba3a2eae271288ba8a22c72:SugarShackGame/Assets/Scripts/Abilities/AbilityObjectFactory.cs
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

<<<<<<< HEAD:SugarShackGame/Assets/Scripts/Throwing/ThrowableFactory.cs
        private Throwable ReleaseObject(AbilityType throwableName)
=======
        private AbilityComponent ReleaseObject(AbilityType abilityType)
>>>>>>> 17a14d23657b327dcba3a2eae271288ba8a22c72:SugarShackGame/Assets/Scripts/Abilities/AbilityObjectFactory.cs
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
