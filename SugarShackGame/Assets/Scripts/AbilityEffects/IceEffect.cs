using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : MonoBehaviour
{
    public float freezeTime = 5f;
    public Vector3 startSize = new Vector3(5f, 5f, 5f);
    public Vector3 endSize = new Vector3(1f, 1f, 1f);

    public void FreezeEffect(GameObject objectToFreeze)
    {
        StartCoroutine(FreezeObject(objectToFreeze));
    }

    private IEnumerator FreezeObject(GameObject objectToFreeze)
    {
        // Disable any components that might interfere with freezing
        var input = objectToFreeze.GetComponent<CustomInputHandler>();
        var animator = objectToFreeze.GetComponent<Animator>();
        var characterController = objectToFreeze.GetComponent<CharacterController>();
        var agent = objectToFreeze.GetComponent<UnityEngine.AI.NavMeshAgent>();
        var playerController = objectToFreeze.GetComponent<PlayerController>();

        if (input)
        {
            input.enabled = false;
        }
        if (animator)
        {
            animator.enabled = false;
        }
        if (characterController)
        {
            characterController.enabled = false;
        }
        if (agent)
        {
            agent.enabled = false;
        }
        if (playerController)
        {
            playerController.enabled = false;
        }

        // Freeze the object in place
        transform.position = objectToFreeze.transform.position;

        // Shrink the object gradually over the specified freeze time
        float timeElapsed = 0f;
        while (timeElapsed < freezeTime)
        {
            transform.localScale = Vector3.Lerp(startSize, endSize, timeElapsed / freezeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endSize;

        // Re-enable any disabled components
        if (input)
        {
            input.enabled = true;
        }
        if (animator)
        {
            animator.enabled = true;
        }
        if (characterController)
        {
            characterController.enabled = true;
        }
        if (agent)
        {
            agent.enabled = true;
            agent.Warp(transform.position);
        }
        if (playerController)
        {
            playerController.enabled = true;
        }

        Destroy(gameObject);
    }
}
