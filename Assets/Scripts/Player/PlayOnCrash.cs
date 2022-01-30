using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnCrash : MonoBehaviour
{
    [SerializeField] AudioSource crashSound;
    [SerializeField] AudioSource crashWithPlayerSound;

    [SerializeField] [Range(-3,3)] float minPitch = -1f;
    [SerializeField] [Range(-3, 3)] float maxPitch = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.layer.Equals("WorldLimits"))
        {
            if(!collision.gameObject.tag.Equals("Ship"))
            {
                crashWithPlayerSound.pitch = Random.Range(minPitch, maxPitch);
                crashWithPlayerSound.PlayOneShot(crashWithPlayerSound.clip);
            }
            else
            {
                crashSound.pitch = Random.Range(minPitch, maxPitch);
                crashSound.PlayOneShot(crashSound.clip);
            }
        }
    }
}
