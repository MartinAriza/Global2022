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
        
        //Prueba movimiento
        //int velMove = 10;
        //float movX = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector3(movX * velMove, rb.velocity.y, 0);

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
