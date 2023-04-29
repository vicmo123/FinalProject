using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineEffect : MonoBehaviour
{

    public float UpwardsForce = 300f;
    private Vector3 trampolineForce = Vector3.up;

    public float fadeDuration = 1.0f;

    public void Start()
    {
        StartCoroutine(FadeOut(fadeDuration));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player") || other.transform.root.CompareTag("Animal"))
        {
            Ragdoll ragdollComponent = other.GetComponentInParent<Ragdoll>();

            if (ragdollComponent != null)
            {
                ragdollComponent.ragdollTriggerAll.Invoke((trampolineForce * UpwardsForce));

                StartCoroutine(FadeIn(fadeDuration));
                
            }
        }
    }

    public void Init()
    {
        //StartCoroutine(FadeOut(fadeDuration));
    }

    IEnumerator FadeIn(float duration)
    {
        // Get all the renderers in the object
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        // Set the initial opacity to 0.0 (fully transparent)
        float opacity = 0.0f;

        // Loop until the fade duration has elapsed
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            // Calculate the new opacity based on the elapsed time
            opacity = elapsed / duration;

            // Set the color of each renderer with the new opacity
            foreach (Renderer renderer in renderers)
            {
                Color color = renderer.material.color;
                color.a = opacity;
                renderer.material.color = color;
            }

            // Wait for the next frame
            yield return null;

            // Update the elapsed time
            elapsed += Time.deltaTime;
        }

        // Set the final opacity of each renderer to 1.0 (fully opaque)
        foreach (Renderer renderer in renderers)
        {
            Color color = renderer.material.color;
            color.a = 1.0f;
            renderer.material.color = color;
        }

        Destroy(gameObject);
    }

    IEnumerator FadeOut(float duration)
    {
        // Get all the renderers in the object
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        // Get the initial color of the material
        Color initialColor = renderers[0].material.color;

        // Loop until the fade duration has elapsed
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            // Calculate the new alpha value based on the elapsed time
            float alpha = Mathf.Lerp(initialColor.a, 0.0f, elapsed / duration);

            // Set the alpha value of the material for each renderer
            foreach (Renderer renderer in renderers)
            {
                Color color = renderer.material.color;
                color.a = alpha;
                renderer.material.color = color;
            }

            // Wait for the next frame
            yield return null;

            // Update the elapsed time
            elapsed += Time.deltaTime;
        }

        // Set the final alpha value of the material for each renderer to 0.0
        foreach (Renderer renderer in renderers)
        {
            Color color = renderer.material.color;
            color.a = 0.0f;
            renderer.material.color = color;
        }
    }

}
