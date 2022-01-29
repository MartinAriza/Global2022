using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hook : MonoBehaviour
{

    public int ropeLength = 10;
    public int forwardPos = 1;
    public GameObject ropePart;

    Rigidbody rb;
    Vector3 fwd;
    Vector3 ropePos;
    GameObject first;
    GameObject second;

    Queue<GameObject> ropePool;

    private void Start()
    {
        ropePool = new Queue<GameObject>();

        rb = GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //prueba movimiento
        //int velmove = 10;
        //float movx = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector3(movx * velmove, rb.velocity.y, 0);

        Debug.DrawRay(transform.position, transform.position + fwd * ropeLength, Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fwd = transform.TransformDirection(Vector3.forward);
            ropePos = new Vector3(transform.position.x + forwardPos, transform.position.y, transform.position.z);

            first = Instantiate(ropePart, ropePos, Quaternion.identity);
            GetComponent<FixedJoint>().connectedBody = first.GetComponent<Rigidbody>();
            ropePool.Enqueue(first);

            if (Physics.Raycast(transform.position, fwd, out hit, ropeLength))
            {
                for(int i = 0; i < hit.distance; i++)
                {
                    Vector3 newPos = new Vector3(ropePos.x + i, ropePos.y, ropePos.z);
                    second = Instantiate(ropePart, newPos, Quaternion.identity);
                    first.GetComponent<FixedJoint>().connectedBody = second.GetComponent<Rigidbody>();
                    first = second;
                    ropePool.Enqueue(first);
                }

                first.GetComponent<FixedJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();

            }
            else
            {
                for (int i = 0; i < ropeLength; i++)
                {
                    Vector3 newPos = new Vector3(ropePos.x + i, ropePos.y, ropePos.z);
                    second = Instantiate(ropePart, newPos, Quaternion.identity);
                    first.GetComponent<FixedJoint>().connectedBody = second.GetComponent<Rigidbody>();
                    first = second;
                    ropePool.Enqueue(first);
                }
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            while(ropePool.Count > 0)
            {
                GameObject go = ropePool.Dequeue();
                Destroy(go);
            }
        }

    }
}
