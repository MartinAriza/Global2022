using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] ShipEngine leftEngine;
    [SerializeField] ShipEngine rightEngine;

    [SerializeField] float torqueForce = 10f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /*Vector3 torque = (leftEngine.transform.up + rightEngine.transform.up) * torqueForce;

        rb.AddTorque(torque, ForceMode.Force);*/
    }
}
