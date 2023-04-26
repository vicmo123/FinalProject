using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Manager(typeof(ThrowableManager))]
public class ThrowableManager : IFlow
{
    #region Singleton
    private static ThrowableManager instance;

    public static ThrowableManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ThrowableManager();
            }
            return instance;
        }
    }

    private ThrowableManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    private HashSet<Throwable> Collection = new HashSet<Throwable>();
    private ThrowableFactoryPool.ThrowablePool pool;
    private ThrowableFactoryPool.ThrowableFactory factory;
    private int prefillAmountPerType = 5;

    private void RemoveObjectFromCollection(AbilityType type,Throwable throwable)
    {
        Collection.Remove(throwable);
        pool.Pool(type, throwable);
    }

    public bool TryAddObjectToCollection(AbilityType type, Thrower thrower)
    {
        if (!thrower.IsHoldingThrowable)
        {
            Throwable newObj = factory.CreateObject(type);
            Collection.Add(newObj);

            newObj.Initialize();
            newObj.PreInitialize();

            newObj.AttachToThrower(thrower);

            thrower.toThrow = newObj;
            thrower.ComputeNewLayerMask();

            return true;
        }
        else
        {
            return false;
        }
    }

    public void ManageCollectionWithConditions(params System.Predicate<Throwable>[] conditions)
    {
        Queue<Throwable> toDelete = new Queue<Throwable>();

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
            Throwable objToDel = toDelete.Dequeue();
            RemoveObjectFromCollection(objToDel.GetThrowableType(), objToDel);
        }
    }

    public void PreInitialize()
    {
        pool = new ThrowableFactoryPool.ThrowablePool();
        factory = new ThrowableFactoryPool.ThrowableFactory(pool);
        Collection = new HashSet<Throwable>();
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

    private void PrefillPool()
    {
        var enums = System.Enum.GetValues(typeof(AbilityType)).Cast<AbilityType>().ToArray();
        for (int i = 0; i < enums.Length; i++)
        {
            for (int j = 0; j < prefillAmountPerType; j++)
            {
                Throwable newThrowable = factory.CreateObject(enums[i]);
                pool.Pool(newThrowable.GetThrowableType(), newThrowable);
                Debug.Log(newThrowable.GetThrowableType());
            }
        }
    }
}

