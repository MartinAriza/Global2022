using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLimit : MonoBehaviour
{

    [SerializeField] World world;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Ship"))
        {
            world.TransportShip(other.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ship"))
        {
            world.TransportShip(collision.transform.position);
        }
    }
}
