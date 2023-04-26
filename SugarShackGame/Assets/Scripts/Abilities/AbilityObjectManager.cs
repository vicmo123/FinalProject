using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Manager(typeof(AbilityObjectManager))]
public class AbilityObjectManager : IFlow
{
    #region Singleton
    private static AbilityObjectManager instance;

    public static AbilityObjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AbilityObjectManager();
            }
            return instance;
        }
    }

    private AbilityObjectManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    private HashSet<AbilityComponent> Collection = new HashSet<AbilityComponent>();
    private AbilityFactoryPool.AbilityObjectPool pool;
    private AbilityFactoryPool.AbilityObjectFactory factory;
    private int prefillAmountPerType = 5;

    private void RemoveObjectFromCollection(AbilityType type, AbilityComponent abilityObj)
    {
        Collection.Remove(abilityObj);
        pool.Pool(type, abilityObj);
    }

    public AbilityComponent AddObjectToCollection(AbilityType type, Player player)
    {
        AbilityComponent newObj = factory.CreateObject(type);
        Collection.Add(newObj);

        return newObj;
    }

    public void ManageCollectionWithConditions(params System.Predicate<AbilityComponent>[] conditions)
    {
        Queue<AbilityComponent> toDelete = new Queue<AbilityComponent>();

        foreach (var obj in Collection)
        {
            foreach (var condition in conditions)
            {
                if (condition.Invoke(obj))
                {
                    toDelete.Enqueue(obj);
                    break;
                }
            }
        }

        for (int i = 0; i < toDelete.Count; i++)
        {
            AbilityComponent objToDel = toDelete.Dequeue();
            RemoveObjectFromCollection(objToDel.abilityStats.type, objToDel);
        }
    }

    public void PreInitialize()
    {
        pool = new AbilityFactoryPool.AbilityObjectPool();
        factory = new AbilityFactoryPool.AbilityObjectFactory(pool);
        Collection = new HashSet<AbilityComponent>();
    }

    public void Initialize()
    {
        //PrefillPool();
    }

    public void Refresh()
    {
        ManageCollectionWithConditions((val) => { return val.readyToBeDestroyed; });

        foreach (var item in Collection)
        {
            item.Refresh();
        }
    }

    public void PhysicsRefresh()
    {
        foreach (var item in Collection)
        {
            item.PhysicsRefresh();
        }
    }
}
