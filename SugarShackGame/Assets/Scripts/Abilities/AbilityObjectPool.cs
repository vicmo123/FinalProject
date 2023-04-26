using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AbilityFactoryPool
{
    public class AbilityObjectPool
    {
        private Dictionary<AbilityType, Queue<AbilityComponent>> AbilityObjectPools;

        public AbilityObjectPool()
        {
            Initialize();
        }

        public void Initialize()
        {
            AbilityObjectPools = new Dictionary<AbilityType, Queue<AbilityComponent>>();

            List<AbilityType> enums = System.Enum.GetValues(typeof(AbilityType)).Cast<AbilityType>().ToList();

            for (int i = 0; i < enums.Count; i++)
            {
                AbilityObjectPools[enums[i]] = new Queue<AbilityComponent>();
            }
        }

        public void Pool(AbilityType throwableType, AbilityComponent objToPool)
        {
            objToPool.Deactivate();
            AbilityObjectPools[throwableType].Enqueue(objToPool);
        }

        public AbilityComponent Depool(AbilityType throwableType)
        {
            AbilityComponent toRet = (AbilityObjectPools[throwableType].Count > 0) ? AbilityObjectPools[throwableType].Dequeue() : null;
            if (toRet)
                toRet.Activate();
            return toRet;
        }
    }
}
