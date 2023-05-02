using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour, IFlow
{
    [SerializeField] private List<Renderer> renderers;
    [SerializeField] private Color color;

    private float highlightDelay = 0.2f;
    private float endDelay;

    private List<Material> materials;

    public void PreInitialize() {
        materials = new List<Material>();

        renderers.AddRange(gameObject.GetComponentsInChildren<Renderer>());
        foreach (var renderer in renderers) {
            foreach (var material in renderer.materials)
                materials.Add(material);
        }
    }

    public void Initialize() {
    }

    public void Refresh() {
        if (Time.time >= endDelay)
            ToggleHighlight(false);
    }

    public void PhysicsRefresh() {
    }

    public void ToggleHighlight(bool val) {
        if (val) {
            endDelay = Time.time + highlightDelay;
            foreach (var material in materials) {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color * -0.8f);
            }
        }
        else {
            foreach (var material in materials) {
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
