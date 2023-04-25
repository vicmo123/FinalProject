using System;
using UnityEngine;

public class Bucket : MonoBehaviour, IFlow, IUsable
{
    Player player;

    [SerializeField] private GameObject fillingBarObject;
    private FillingBar fillingBar;
    [SerializeField] private Transform positionForFillingBar;

    public float sapAmount = 0.0f;
    private float maxSapAmount = 20.0f;
    private float sapGainSpeed = 3.0f;

    private float remainingTimeToClaim = 0.0f;
    private float timeToClaim = 3.0f;
    private float timeToSteal = 6.0f;
    private float lastUsedTime = 0.0f;

    private float cooldown = 1.0f;
    private float endOfCooldown = 0.0f;

    private Color originalColor;
    private Renderer bucketRenderer;

    public void Initialize() {
        Placement();

        bucketRenderer = GetComponentInChildren<Renderer>();
        originalColor = bucketRenderer.material.color;

        fillingBar.transform.SetParent(null);
    }

    public void PhysicsRefresh() {
        if (player)
            fillingBar.Rotate(player);
    }

    public void PreInitialize() {
        fillingBar = GameObject.Instantiate(fillingBarObject).GetComponent<FillingBar>();
        fillingBar.PreInitialize();
        fillingBar.transform.SetParent(transform);
        fillingBar.transform.position = positionForFillingBar.position;
    }

    public void Refresh() {
        Sap();
    }

    public void Use(Player _player) {
        if (!player) {
            // If there is no owner yet, allows them to claim the bucket
            Claim(_player);
        }
        else if (_player == player) {
            CollectSap(_player);
        }
        else {
            // If it is not the owner, allows them to claim the bucket
            Claim(_player);
        }
    }

    private void Placement() {
        float yPos = .8f;

        Transform _parent = transform.parent;
        transform.SetParent(null);
        transform.localScale = new Vector3(1, 1, 1);

        Bounds treeBounds = _parent.gameObject.GetComponentInChildren<BoxCollider>().bounds;
        Bounds bucketBounds = transform.GetComponentInChildren<BoxCollider>().bounds;

        transform.position = new Vector3(0, yPos, 0);
        Ray ray = new Ray(transform.position + bucketBounds.ClosestPoint(_parent.gameObject.transform.position), (treeBounds.ClosestPoint(transform.position) - transform.position).normalized);
        float distance;
        treeBounds.IntersectRay(ray, out distance);

        transform.position += (treeBounds.ClosestPoint(transform.position) - transform.position + bucketBounds.ClosestPoint(_parent.gameObject.transform.position)).normalized * distance;

        transform.SetParent(_parent);
    }

    private void Claim(Player _player) {
        Debug.Log("Bucket is being claimed!");
        if (Time.time - lastUsedTime <= 1.0f) { // If the player is already claiming
            if (remainingTimeToClaim > 0.0f)
                remainingTimeToClaim -= Time.deltaTime;
            else {
                // Bucket is claimed
                Claimed(_player);
                endOfCooldown = Time.time + cooldown;
            }
        }
        else { // If the player starts to claim
            if (!player)
                remainingTimeToClaim = timeToClaim;
            else
                remainingTimeToClaim = timeToSteal;
        }

        lastUsedTime = Time.time;
    }

    private void ChangeColor(Color color) {
        bucketRenderer.material.color = color;
    }

    private void ResetColor() {
        bucketRenderer.material.color = originalColor;
    }

    private void CollectSap(Player _player) {
        if (Time.time >= endOfCooldown) {
            Debug.Log("Sap collected!");
            // If the playing using the bucket is the owner, gets sap
            sapAmount -= _player.playerBucket.AddSap(sapAmount);
            fillingBar.Fill(sapAmount, maxSapAmount);
            endOfCooldown = Time.time + cooldown;
        }
    }

    private void Claimed(Player _player) {
        ChangeColor(Color.blue);
        if (player)
            Debug.Log("Bucket was stolen!");
        else
            Debug.Log("Bucket was claimed!");

        player = _player;

        // Temporary change of color
    }

    private void Sap() {
        sapAmount = Mathf.Clamp(sapAmount + (sapGainSpeed * Time.deltaTime), 0, maxSapAmount);
        fillingBar.Fill(sapAmount, maxSapAmount);
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
