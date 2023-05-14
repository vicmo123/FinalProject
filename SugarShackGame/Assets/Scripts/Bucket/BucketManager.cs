using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Manager(typeof(BucketManager))]
public class BucketManager : IFlow
{
    #region Singleton
    private static BucketManager instance;

    public static BucketManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BucketManager();
            }
            return instance;
        }
    }

    private BucketManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    public List<Bucket> buckets;
    public Dictionary<int, Vector3> bucketPositionDic;
    private GameObject bucketPrefab;


    public void PreInitialize()
    {
        bucketPrefab = Resources.Load("Prefabs/Bucket/Bucket") as GameObject;

        buckets = new List<Bucket>();
        buckets.AddRange(Object.FindObjectsOfType<Bucket>());


        for (int i = buckets.Count - 1; i >= 0; i--)
        {
            if (!buckets[i].CheckParent())
            {
                GameObject.Destroy(buckets[i].gameObject);
                buckets.Remove(buckets[i]);
            }
            else
            {
                buckets[i].PreInitialize();                
            }
        }

        GameObject[] maples = GameObject.FindGameObjectsWithTag("Maple");
        foreach (var maple in maples)
        {
            if (!maple.GetComponentInChildren<Bucket>())
            {
                Bucket bucket = GameObject.Instantiate(bucketPrefab).GetComponent<Bucket>();
                bucket.gameObject.transform.SetParent(maple.transform);
                bucket.PreInitialize();
                buckets.Add(bucket);
                Debug.Log("Buckets in the collection : " + buckets.Count + " Player associated with it  = " + bucket.player);
            }

        }
    }

    public void Initialize()
    {
        foreach (var bucket in buckets)
        {
            bucket.Initialize();
        }
    }

    public void Refresh()
    {
        foreach (var bucket in buckets)
        {
            bucket.Refresh();
        }
    }

    public void PhysicsRefresh()
    {
        foreach (var bucket in buckets)
        {
            bucket.PhysicsRefresh();
        }
    }
}
