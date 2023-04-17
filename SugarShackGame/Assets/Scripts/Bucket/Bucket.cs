using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour, IFlow
{
    [SerializeField] private float sapAmount = 0.0f;
    private float maxSapAmount = 20.0f;
    private float sapGainSpeed = 1.0f;

    private Dictionary<int, Vector3> bucketPositionDic;



    public void Initialize() {
        bucketPositionDic = new Dictionary<int, Vector3>();
        bucketPositionDic.Add(1, new Vector3(-0.01358f, 0.01862f, 0.0061f));
        bucketPositionDic.Add(2, new Vector3(0.0275f, -0.0001f, 0.0422f));
        bucketPositionDic.Add(3, new Vector3(-0.012362f, 0.00132f, -0.015025f));

        int mapleType = Int32.Parse(transform.parent.name.Substring(7, 1));
        transform.localPosition = bucketPositionDic[mapleType];
    }

    public void PhysicsRefresh() {
    }

    public void PreInitialize() {
    }

    public void Refresh() {
        Sap();
    }

    private void Sap() {
        sapAmount += sapGainSpeed * Time.deltaTime;
        if (sapAmount > maxSapAmount)
            sapAmount = maxSapAmount;
    }

    public float GetSap() {
        return sapAmount;
    }

    private void EmptySap() {
        sapAmount = 0.0f;
    }

    public bool CheckParent() {
        if (transform.parent)
            if (transform.parent.CompareTag("Maple"))
                return true;
            else return false;
        else
            return false;
    }
}
