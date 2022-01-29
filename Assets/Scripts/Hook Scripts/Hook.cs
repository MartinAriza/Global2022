using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hook : MonoBehaviour
{
    public int ropeLength = 10;

    [SerializeField] Rigidbody rb;
    [SerializeField] float attractionForceMultiplier = 4f;
    [SerializeField] float releaseInertiaMultiplier = 4f;
    [SerializeField] float releaseAngulaVelocityDivider = 2f;
    LineRenderer lineRenderer;

    GameObject hookedGameObject = null;

    bool firstFrameHook = true;
    float firstFrameDistance = 0f;

    RaycastHit hit;

    private void Start()
    {
        //ropePool = new Queue<GameObject>();
        lineRenderer = rb.gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * ropeLength, Color.green);

        if (Input.GetKey(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, ropeLength))
            {
                if(firstFrameHook)
                {
                    firstFrameHook = false;
                    firstFrameDistance = hit.distance;
                }

                hookedGameObject = hit.collider.gameObject;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            hookedGameObject = null;
            lineRenderer.SetVertexCount(0);

            Vector3 direction = new Vector3(rb.velocity.x, rb.velocity.y, 0f);

            Vector3 force = direction * releaseInertiaMultiplier;

            rb.AddForce(force, ForceMode.Impulse);
            rb.angularVelocity = rb.angularVelocity / releaseAngulaVelocityDivider;

            firstFrameHook = true;
            firstFrameDistance = 0f;
        }
    }

    private void FixedUpdate()
    {
        if(hookedGameObject != null)
        {
            //Draw line between ship and hooked object
            lineRenderer.SetVertexCount(2);

            lineRenderer.SetPosition(0, rb.transform.position);
            lineRenderer.SetPosition(1, hookedGameObject.transform.position);


            //Set Distance
            //Vector3 direction = (hookedGameObject.transform.position - rb.gameObject.transform.position).normalized;

            Vector3 direction = (hookedGameObject.transform.position - rb.gameObject.transform.position).normalized;


            rb.gameObject.transform.position = hookedGameObject.transform.position;
            rb.gameObject.transform.position = rb.gameObject.transform.position - rb.gameObject.transform.forward * firstFrameDistance;

            //Rotate ship towards hooked object
            rb.gameObject.transform.rotation = Quaternion.LookRotation(-direction, Vector3.forward);
        }
    }
}
