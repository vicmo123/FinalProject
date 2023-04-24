using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Manager(typeof(BucketManager))]
public class BucketManager : IFlow
{
    #region Singleton
    private static BucketManager instance;

    public static BucketManager Instance {
        get {
            if (instance == null) {
                instance = new BucketManager();
            }
            return instance;
        }
    }

    private BucketManager() {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    private List<Bucket> buckets;
    public Dictionary<int, Vector3> bucketPositionDic;

    public void PreInitialize() {
        bucketPositionDic = new Dictionary<int, Vector3>();
        bucketPositionDic.Add(1, new Vector3(-0.01358f, 0.0224f, 0.0061f));
        bucketPositionDic.Add(2, new Vector3(0.04f, -0.0001f, 0.0422f));
        bucketPositionDic.Add(3, new Vector3(-0.012362f, 0.00184f, -0.01499f));

        buckets = new List<Bucket>();
        buckets.AddRange(Object.FindObjectsOfType<Bucket>());


        for (int i = buckets.Count - 1; i >= 0; i--) {
            if (!buckets[i].CheckParent()) {
                GameObject.Destroy(buckets[i].gameObject);
                buckets.Remove(buckets[i]);
            } else {
                buckets[i].PreInitialize();
            }
        }
    }

    public void Initialize() {
        foreach (var bucket in buckets) {
            bucket.Initialize();
        }
    }

    public void Refresh() {
        foreach (var bucket in buckets) {
            bucket.Refresh();
        }
    }

    public void PhysicsRefresh() {
        foreach (var bucket in buckets) {
            bucket.PhysicsRefresh();
        }
    }
}
