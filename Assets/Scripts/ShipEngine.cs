using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{
    #region Ship Parts
    [SerializeField] Rigidbody shipBody;

    [SerializeField] bool leftEngine = false;
    [SerializeField] bool rightEngine = true;
    #endregion


    #region Input
    float xAxis = 0f;

    bool thrustInput = false;
    bool hookInput = false;
    #endregion


    #region Movement variables
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] float thrustSpeed = 10f;
    [SerializeField] float torque = 10f;
    [SerializeField] float torqueAngleThreshold = 45f;
    #endregion

    void Start()
    {

    }

    void Update()
    {
        if (leftEngine) getLeftEngineInput();
        else if (rightEngine) getRightEngineInput();

        if (leftEngine)
        {
            RotateEngine(90f, 180f);
        }

        else if (rightEngine)
        {
            RotateEngine(180f, 270f);
        }
    }

    private void FixedUpdate()
    {
        if (thrustInput)
        {
            shipBody.AddForce(transform.up * thrustSpeed, ForceMode.Force);

            if(leftEngine)
                shipBody.AddTorque(transform.forward * torque, ForceMode.Force);

            else if(rightEngine)
                shipBody.AddTorque(-transform.forward * torque, ForceMode.Force);
        }
    }

    #region Input Management
    void getLeftEngineInput()
    {
        xAxis = 0f;

        //Left side Keyboard Input
        if (Input.GetKey(KeyCode.A))
            xAxis -= 1;

        if (Input.GetKey(KeyCode.D))
            xAxis += 1;

        thrustInput = Input.GetKey(KeyCode.S);
        hookInput = Input.GetKey(KeyCode.W);

        //Left side Gamepad Input
    }

    void getRightEngineInput()
    {
        xAxis = 0f;

        //Right side Keyboard Input
        if (Input.GetKey(KeyCode.LeftArrow))
            xAxis -= 1;

        if (Input.GetKey(KeyCode.RightArrow))
            xAxis += 1;

        thrustInput = Input.GetKey(KeyCode.DownArrow);
        hookInput = Input.GetKey(KeyCode.UpArrow);

        //Right side Gamepad Input
    }
    #endregion

    void RotateEngine(float minAngle, float maxAngle)
    {
        float currentRotation = transform.localEulerAngles.y;
        float newRotation = rotationSpeed * xAxis;

        if ( (transform.localEulerAngles.y + newRotation) >= minAngle && (transform.localEulerAngles.y + newRotation) <= maxAngle)
            transform.Rotate(new Vector3(0f, 0f, -newRotation), Space.Self);
    }
}
