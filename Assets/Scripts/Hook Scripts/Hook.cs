using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hook : MonoBehaviour
{

    public int ropeLength = 10;
    public int forwardPos = 1;
    public GameObject ropePart;

    Rigidbody rb;
    //Vector3 fwd;
    Vector3 ropePos;
    GameObject first;
    GameObject second;

    Queue<GameObject> ropePool;
    [SerializeField] GameObject ropeParent;

    private void Start()
    {
        ropePool = new Queue<GameObject>();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Debug.DrawLine(transform.position, transform.position + transform.forward * ropeLength, Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //fwd = transform.TransformDirection(Vector3.forward);
            ropePos = new Vector3(transform.position.x, transform.position.y, transform.position.z + forwardPos);

            first = Instantiate(ropePart, ropePos, Quaternion.identity);

            first.transform.SetParent(ropeParent.transform);

            GetComponent<FixedJoint>().connectedBody = first.GetComponent<Rigidbody>();
            ropePool.Enqueue(first);

            if (Physics.Raycast(transform.position, transform.forward, out hit, ropeLength))
            {
                for(int i = 1; i < hit.distance; i++)
                {
                    Vector3 newPos = new Vector3(ropePos.x, ropePos.y + i, ropePos.z);
                    second = Instantiate(ropePart, newPos, Quaternion.identity);
                    second.transform.SetParent(ropeParent.transform);

                    first.GetComponent<FixedJoint>().connectedBody = second.GetComponent<Rigidbody>();
                    first = second;
                    ropePool.Enqueue(first);
                }

                first.GetComponent<FixedJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();

            }
            else
            {
                for (int i = 1; i < ropeLength; i++)
                {
                    Vector3 newPos = new Vector3(ropePos.x, ropePos.y + i, ropePos.z);
                    second = Instantiate(ropePart, newPos, Quaternion.identity);
                    second.transform.SetParent(ropeParent.transform);

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
