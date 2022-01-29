using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private Vector3 spriteSize = Vector2.zero;
    private Vector3 spriteStartPosition = Vector2.zero;

    private GameObject camera;

    [SerializeField] float parallax = 0.2f;

    Vector2 distance = Vector2.zero;
    Vector2 distanceFromCamera = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        spriteStartPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        spriteSize = GetComponent<SpriteRenderer>().bounds.size;

        camera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromCamera = new Vector2(camera.transform.position.x, camera.transform.position.y) * (1 - parallax);

        //distance = new Vector2(camera.transform.position.x, camera.transform.position.y) * parallax;

        distance = new Vector2(transform.position.x - spriteStartPosition.x, transform.position.y - spriteStartPosition.y) * parallax;

        transform.position = new Vector3(spriteStartPosition.x + distance.x, spriteStartPosition.y + distance.y, transform.position.z);


        //To DO Fix popping
        if (distanceFromCamera.x > spriteStartPosition.x + spriteSize.x) spriteStartPosition.x += spriteSize.x;
        else if (distanceFromCamera.x < spriteStartPosition.x - spriteSize.x) spriteStartPosition.x -= spriteSize.x;

        if (distanceFromCamera.y > spriteStartPosition.y + spriteSize.y) spriteStartPosition.y += spriteSize.y;
        else if (distanceFromCamera.y < spriteStartPosition.y - spriteSize.y) spriteStartPosition.y -= spriteSize.y;
    }
}
