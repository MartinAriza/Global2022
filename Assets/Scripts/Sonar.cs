using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] GameObject shipBody;

    GameObject[] satellites;

    float distanceToClosest = Mathf.Infinity;
    GameObject closestSatellite = null;

    private void Start()
    {
        satellites = GameObject.FindGameObjectsWithTag("Satellite");
    }

    // Update is called once per frame
    void Update()
    {
        //Translate under the player ship
        transform.position = shipBody.transform.position;

        //Find Closest Satellite
        foreach(GameObject satellite in satellites)
        {
            float distance = Mathf.Abs((transform.position - satellite.transform.position).magnitude);

            if(distance < distanceToClosest)
            {
                distanceToClosest = distance;
                closestSatellite = satellite;
            }
        }
    }
}
