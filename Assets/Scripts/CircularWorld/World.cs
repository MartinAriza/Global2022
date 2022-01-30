using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] GameObject worldCenter;

    [SerializeField] float offset = 2f;
    [SerializeField] GameObject ship;
    [SerializeField] CameraFollow playerCamera;
    [SerializeField] ShipEngine leftEngine;
    [SerializeField] ShipEngine rightEngine;

    public void TransportShip(Vector3 collisionPoint)
    {
        LayerMask worldlLimitMask = LayerMask.GetMask("Ship", "Satellite");
        Vector3 direction = (worldCenter.transform.position - collisionPoint).normalized;
        RaycastHit hit;

        Debug.DrawLine(collisionPoint, direction * 100f, Color.green, 10f);

        if(Physics.Raycast(collisionPoint, direction, out hit, Mathf.Infinity/*, worldlLimitMask*/))
        {
            ship.transform.position = hit.point - direction * offset;

            leftEngine.ClearTrails();
            
            playerCamera.InstantTransport();

            leftEngine.ClearTrails();
        }
    }
}
