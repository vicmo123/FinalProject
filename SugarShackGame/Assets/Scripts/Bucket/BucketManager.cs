using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Manager(typeof(BucketManager))]
public class BucketManager : MonoBehaviour, IFlow
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

    public void PreInitialize() {
        buckets = new List<Bucket>();
        buckets.AddRange(Object.FindObjectsOfType<Bucket>());
        for (int i = buckets.Count - 1; i >= 0; i--) {
            if (!buckets[i].CheckParent()) {
                Destroy(buckets[i].gameObject);
                buckets.Remove(buckets[i]);
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
    }


}
