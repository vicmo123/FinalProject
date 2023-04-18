using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class if for testing purposes for the Ragdoll system
public class Shoot : MonoBehaviour
{
    [SerializeField]
    private float _maxForce;
    [SerializeField]
    private float _maximumForceTime;
    private float _timeMouseButtonDown;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _timeMouseButtonDown = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Ragdoll ragdollComponent = hitInfo.collider.GetComponentInParent<Ragdoll>();

                if(ragdollComponent != null)
                {
                    float mouseButtonDownDuration = Time.time - _timeMouseButtonDown;
                    float forcePercentage = mouseButtonDownDuration / _maximumForceTime;
                    float forceMagnitude = Mathf.Lerp(1, _maxForce, forcePercentage);

                    Vector3 forceDirection = ragdollComponent.transform.position - _camera.transform.position;
                    forceDirection.y = 1;
                    forceDirection.Normalize();

                    Vector3 force = forceMagnitude * forceDirection;

                    ragdollComponent.ragdollTrigger.Invoke(hitInfo.point, force);
                }
            }
        }
    }
}
