using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBucket : MonoBehaviour, IUsable, IFlow
{
    private enum ActionsToSpill
    {
        WALK,
        RUN,
        JUMP,
        RAGDOLL
    }

    private CustomInputHandler inputHandler;
    private Ragdoll ragdoll;

    public GameObject bucketLid;
    public float sapAmount = 0.0f;
    public Transform spillingAttatchPoint;
    private float maxSapAmount = 30.0f;

    [HideInInspector] public bool isSpillable = true;
    private Dictionary<ActionsToSpill, float> spillQuantities = new Dictionary<ActionsToSpill, float>() {
        {ActionsToSpill.WALK, 0.2f},
        {ActionsToSpill.RUN, 0.4f},
        {ActionsToSpill.JUMP, 0.8f},
        {ActionsToSpill.RAGDOLL, 30.0f}
    };



    public void Initialize()
    {
        bucketLid.SetActive(false);
    }

    public void PhysicsRefresh()
    {
    }

    public void PreInitialize()
    {
        inputHandler = GetComponent<CustomInputHandler>();
        ragdoll = GetComponent<Ragdoll>();
    }

    public void Refresh()
    {
        SpillSap();

        //Debug.Log(sapAmount);
    }

    public void Use(Player _player)
    {

    }

    public float AddSap(float amount)
    {
        float amountAdded = amount;
        if (amount + sapAmount > maxSapAmount)
        {
            amountAdded = maxSapAmount - sapAmount;
            sapAmount = maxSapAmount;
        }
        else
        {
            sapAmount += amount;
        }

        return amountAdded;
    }

    public void RemoveSap(float amount)
    {
        sapAmount = Mathf.Clamp(sapAmount - amount, 0, maxSapAmount);
        if (sapAmount > 0)
            SpillingEffect();
    }

    public void SpillSap()
    {
        if (!isSpillable)
            return;
        if (inputHandler.Sprint)
            RemoveSap(spillQuantities[ActionsToSpill.RUN] * Time.deltaTime);
        else if (inputHandler.Move != Vector2.zero)
            RemoveSap(spillQuantities[ActionsToSpill.WALK] * Time.deltaTime);
        if (inputHandler.Jump)
            RemoveSap(spillQuantities[ActionsToSpill.JUMP] * Time.deltaTime);
        if (ragdoll.isRagdollTriggered)
            RemoveSap(spillQuantities[ActionsToSpill.RAGDOLL]);

    }
    private void SpillingEffect()
    {
        GameObject spilling = ParticleEffectManager.Instance.Create(ParticleEffectType.Spilling);
        spilling.transform.position = this.spillingAttatchPoint.position;
    }

    public IEnumerator NoSpill(float duration)
    {
        bucketLid.SetActive(true);
        isSpillable = false;
        yield return new WaitForSeconds(duration);
        isSpillable = true;
        bucketLid.SetActive(false);
    }
}
