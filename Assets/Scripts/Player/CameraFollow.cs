using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float offset = -18f;

    [SerializeField] float smoothTimeInDeadzone = 0.3f;
    [SerializeField] float smoothRotation = 0.06f;

    private Vector3 velocity = Vector3.zero;
    private Camera playerCamera;



    private void Start()
    {
        playerCamera = Camera.main;

        Vector3 viewPos = playerCamera.WorldToViewportPoint(target.position);

        Vector3 targetPosition = target.TransformPoint(new Vector3(0f, offset, 0f));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTimeInDeadzone);
    }

    void FixedUpdate()
    {
        Vector3 viewPos = playerCamera.WorldToViewportPoint(target.position);

        //Translation
        Vector3 targetPosition = target.TransformPoint(new Vector3(0f, offset, 0f));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTimeInDeadzone);

        //Rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.forward, -target.transform.forward), smoothRotation);
    }
}
