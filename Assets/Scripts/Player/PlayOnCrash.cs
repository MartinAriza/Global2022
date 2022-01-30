using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnCrash : MonoBehaviour
{
    [SerializeField] AudioSource crashSound;
    [SerializeField] [Range(-3,3)] float minPitch = -1f;
    [SerializeField] [Range(-3, 3)] float maxPitch = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.layer.Equals("WorldLimits"))
        {
            crashSound.pitch = Random.Range(minPitch, maxPitch);
            crashSound.PlayOneShot(crashSound.clip);
        }
    }
}
