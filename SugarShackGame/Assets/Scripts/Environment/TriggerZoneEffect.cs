using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneEffect : MonoBehaviour
{
    public Transform rockWallParent;
    private List<CapsuleCollider> colliders;

    private void Start()
    {
        colliders = new List<CapsuleCollider>();

        //Get all the rocks children's component sphere collider
        CapsuleCollider[] children = rockWallParent.GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].enabled = false;
            Debug.Log(children[i].name.ToString());
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
