using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] GameObject shipBody;
    [SerializeField] SpriteRenderer sonarSprite;
    [SerializeField] float smoothTime = 0.3f;

    GameObject[] satellites;

    float distanceToClosest = Mathf.Infinity;
    GameObject closestSatellite = null;

    //bool sonarInput;
    //[SerializeField] float showSonarDuringTime = 10f;

    private void Start()
    {
        satellites = GameObject.FindGameObjectsWithTag("Satellite");
        //sonarSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*sonarInput = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        if(sonarInput)
        {
            StopCoroutine(ShowSonarSomeTime());
            StartCoroutine(ShowSonarSomeTime());
        }*/

        //Translate under the player ship
        transform.position = shipBody.transform.position;

        distanceToClosest = Mathf.Infinity;

        if (satellites.Length > 0)
        {
            sonarSprite.enabled = true;

            //Find Closest Satellite
            foreach (GameObject satellite in satellites)
            {
                float distance = Mathf.Abs((transform.position - satellite.transform.position).magnitude);

                if (distance < distanceToClosest)
                {
                    distanceToClosest = distance;
                    closestSatellite = satellite;
                }
            }

            //Rotate sonar to closest satellite
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, (closestSatellite.transform.position - transform.position).normalized), smoothTime);
        }
        else
        {
           sonarSprite.enabled = false;
        }
    }

    /*IEnumerator ShowSonarSomeTime()
    {
        sonarSprite.enabled = true;
        yield return new WaitForSecondsRealtime(showSonarDuringTime);
        sonarSprite.enabled = false;
    }*/

    public void UpdateSatelliteList()
    {
        satellites = GameObject.FindGameObjectsWithTag("Satellite");
    }
}
