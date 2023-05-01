using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace footsteps
{
    public class FootprintFactoryPool
    {
        public float lifeTime = 10f;
        private GameObject footStepPrefab;
        private const string filePath = "Prefabs/FootSteps/";
        private Queue<FootPrint> pool = new Queue<FootPrint>();

        public FootprintFactoryPool(GameObject _footStepPrefab)
        {
            footStepPrefab = _footStepPrefab;
        }

        public FootPrint Create(Vector3 position, Quaternion rotation)
        {
            FootPrint footprint = DePool();
            if (footprint == null)
            {
                footprint = GameObject.Instantiate(footStepPrefab).GetComponent<FootPrint>();
                footprint.Initialize();
                footprint.PreInitialize();
            }

            footprint.transform.position = position;
            footprint.transform.rotation = rotation;

            return footprint;
        }

        public void Pool(FootPrint toPool)
        {
            toPool.gameObject.SetActive(false);
            pool.Enqueue(toPool);
        }

        public FootPrint DePool()
        {
            FootPrint footprint = null;
            if (pool.Count > 0)
            {
                footprint = pool.Dequeue();
                footprint.gameObject.SetActive(true);
            }
            return footprint;
        }
    }
}
