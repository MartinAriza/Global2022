using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float offset = -80f;

    [SerializeField] float smoothTimeInDeadzone = 1f;
    [SerializeField] float rotationSpeed = 1f;

    [SerializeField] Vector2 deadZoneX = new Vector2(0.25f, 0.75f);
    [SerializeField] Vector2 deadZoneY = new Vector2(0.25f, 0.75f);

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
        //transform.up = Vector3.SmoothDamp(transform.up, -target.transform.forward, ref velocity, rotationSpeed);

        transform.LookAt(target.position, -target.transform.forward);

        //Debug.Log(target.rotation.eulerAngles.x);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, target.rotation.eulerAngles.x - 90), rotationSpeed * Time.deltaTime);
    }
}
