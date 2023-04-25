using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ThrowableFactoryPool
{
    public class ThrowablePool
    {
        private Dictionary<AbilityType, Queue<Throwable>> throwablePools;

        public ThrowablePool()
        {
            Initialize();
        }

        public void Initialize()
        {
            throwablePools = new Dictionary<AbilityType, Queue<Throwable>>();

            List<AbilityType> enums = System.Enum.GetValues(typeof(AbilityType)).Cast<AbilityType>().ToList();

            for (int i = 0; i < enums.Count; i++)
            {
                throwablePools[enums[i]] = new Queue<Throwable>();
            }
        }

        public void Pool(AbilityType throwableType,Throwable objToPool)
        {
            objToPool.Deactivate();
            throwablePools[throwableType].Enqueue(objToPool);
        }

        public Throwable Depool(AbilityType throwableType)
        {
            Throwable toRet = (throwablePools[throwableType].Count > 0) ? throwablePools[throwableType].Dequeue() : null;
            if (toRet)
                toRet.Activate();
            return toRet;
        }
    }
}

