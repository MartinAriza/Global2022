using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{

    public int ropeLength = 10;
    public int forwardPos = 1;
    public GameObject ropePart;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 ropePos = new Vector3(transform.position.x + forwardPos, transform.position.y, transform.position.z);

        //prueba movimiento
        //int velmove = 10;
        //float movx = Input.GetAxis("horizontal");
        //rb.velocity = new Vector3(movx * velmove, rb.velocity.y, 0);

        Debug.DrawRay(transform.position, transform.position + fwd * ropeLength, Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject first = Instantiate(ropePart, ropePos, Quaternion.identity);
            GetComponent<FixedJoint>().connectedBody = first.GetComponent<Rigidbody>();

            if (Physics.Raycast(transform.position, fwd, out hit, ropeLength))
            {
                for(int i = 1; i < hit.distance; i++)
                {
                    Vector3 newPos = new Vector3(ropePos.x + i, ropePos.y, ropePos.z);
                    GameObject second = Instantiate(ropePart, newPos, Quaternion.identity);
                    first.GetComponent<FixedJoint>().connectedBody = second.GetComponent<Rigidbody>();
                    first = second;
                }

                first.GetComponent<FixedJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();

            }

            else
            {
                for (int i = 1; i < ropeLength; i++)
                {
                    Vector3 newPos = new Vector3(ropePos.x + i, ropePos.y, ropePos.z);
                    GameObject second = Instantiate(ropePart, newPos, Quaternion.identity);
                    first.GetComponent<FixedJoint>().connectedBody = second.GetComponent<Rigidbody>();
                    first = second;
                }
            }

        }
    }
}
