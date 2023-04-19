using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMap : MonoBehaviour
{
    Rigidbody rb;
    public Transform nortBound;
    public Transform southBound;
    public Transform leftBound;
    public Transform rightBound;
    public Transform underMapBound;
    public Transform Shack_Spawn_Pos;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //If players or objects exit of the bounds of the map ...
        if (rb.position.x > rightBound.position.x
            || rb.position.x < leftBound.position.x
            || rb.position.z > nortBound.position.z
            || rb.position.z < southBound.position.z
            || rb.position.y < underMapBound.position.y) 
        {
            if (gameObject.tag == "Player")
            {
                //Player spawns near the shack
                ResetPlayer();
            }
            else
            {
                //Objects get destroyed
                GameObject.Destroy(gameObject);
            }
        }
    }

    private void ResetPlayer()
    {
        transform.position = Shack_Spawn_Pos.position;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularDrag = 0.05f;
        rb.drag = 0;
        transform.localRotation = Shack_Spawn_Pos.localRotation;
        rb.freezeRotation = true;
    }
}
