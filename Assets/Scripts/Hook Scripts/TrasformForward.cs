using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasformForward : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.ProjectOnPlane((cam.transform.position - transform.position).normalized, Vector3.right));
    }
}
