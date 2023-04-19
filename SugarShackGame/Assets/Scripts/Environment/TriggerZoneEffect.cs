using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneEffect : MonoBehaviour
{
    public Transform rockWallParent;
    private List<SphereCollider> colliders;

    private void Start()
    {
        colliders = new List<SphereCollider>();

        //Get all the rocks children's component sphere collider
        SphereCollider[] children = rockWallParent.GetComponentsInChildren<SphereCollider>();
        for (int i = 1; i < children.Length; i++)
        {
            children[i].enabled = false;
            colliders.Add(children[i]);            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 1; i < colliders.Count; i++)
        {
            colliders[i].enabled = true;
        }
        Debug.Log("Rocks of " + rockWallParent.name + " have been enabled to collide.");
    }
    private void OnTriggerExit(Collider other)
    {
        for (int i = 1; i < colliders.Count; i++)
        {
            colliders[i].enabled = false;
        }
    }
}
