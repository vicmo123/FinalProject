using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornOLD : MonoBehaviour, IFlow
{
    public AbilitySO abilityStats;
    
    private string abilityName;
    private float duration;
    private float timeLeft = 0;
    private float cooldownTime;
    private bool isActive;
    private float velocity;

    private Rigidbody rb;
    private MeshRenderer renderer;
    private BoxCollider collider;


    public void PreInitialize()
    {
        this.abilityName = this.abilityStats.abilityName;
        this.duration = this.abilityStats.activeTime;
        this.timeLeft = this.abilityStats.activeTime;
        this.cooldownTime = this.abilityStats.cooldownTime;
        this.isActive = false;
        this.velocity = this.abilityStats.velocity;

        this.rb = GetComponent<Rigidbody>();
        this.renderer = GetComponent<MeshRenderer>();
        this.collider = GetComponent<BoxCollider>();
    }
    public void Initialize()
    {
    }

    public void PhysicsRefresh()
    {
    }


    public void Refresh()
    {
        if (this.isActive)
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0)
            {

                Debug.Log("horn time effect is over. ");
                this.isActive = false;
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    public void Activate(Vector3 position)
    {
        //Do effect according to position
        //Scare the animals away (the ones that are in a certain perimeter from player)
        this.isActive = true;
        Debug.Log("Horn scaring the animals away!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Activate(collision.transform.position);
            //Particle effect
            //Object disappears
            //this.rb.Sleep();
            //this.renderer.enabled = false;
            //this.collider.enabled = false;
        }
    }

}
