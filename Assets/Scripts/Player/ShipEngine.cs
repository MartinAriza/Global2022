using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{
    #region Ship Parts
    [SerializeField] Rigidbody shipBody;
    [SerializeField] TrailRenderer leftEngineTrail;
    [SerializeField] TrailRenderer rightEngineTrail;
    
    [SerializeField] [Tooltip("Activar si es el motor izquierdo")] bool leftEngine = false;
    [SerializeField] [Tooltip("Activar si es el motor derecho")]  bool rightEngine = true;
    #endregion


    #region Input
    float xAxis = 0f;

    bool thrustInput = false;
    bool hookInput = false;
    #endregion


    #region Movement variables
    [SerializeField] [Tooltip("Velocidad a la que rota el motor")] float rotationSpeed = 3f;
    [SerializeField] [Tooltip("Velocidad lineal que aplica el motor")] float thrustSpeed = 10f;
    [SerializeField] [Tooltip("Torque que aplica el motor")] float torque = 10f;
    [SerializeField] [Range(0f, 1f)] [Tooltip("Porcentaje de torque mínimo que se aplica cuando el motor está en vertical")] float minTorqueApplied = 0.2f;
    [SerializeField] float trailFadeOut = 1f;
    #endregion

    void Start()
    {

    }

    void Update()
    {
        if (leftEngine) getLeftEngineInput();
        else if (rightEngine) getRightEngineInput();

        
    }

    private void FixedUpdate()
    {
        if (leftEngine)
        {
            RotateEngine(90f, 180f);
        }

        else if (rightEngine)
        {
            RotateEngine(180f, 270f);
        }

        if (thrustInput)
        {
            shipBody.AddForce(transform.up * thrustSpeed, ForceMode.Force);

            float torqueMultiplier = 0f;

            if (leftEngine)
            {
                leftEngineTrail.time = 1f;

                torqueMultiplier = Mathf.Clamp01(Mathf.Abs(
                    ((Mathf.Lerp(90f, 180f, (transform.localEulerAngles.y - 90f)
                    / 
                    (180f - 90f)) / (180f - 90f)) 
                    - 2f)
                    ) + minTorqueApplied);

                shipBody.AddTorque(transform.forward * torque * torqueMultiplier, ForceMode.Force);
            }

            else if (rightEngine)
            {
                rightEngineTrail.time = 1f;

                torqueMultiplier = Mathf.Clamp01(Mathf.Abs(
                    ((Mathf.Lerp(180f, 270f, (transform.localEulerAngles.y - 180f) 
                    / 
                    (270f - 180f)) / (270f - 180f)) 
                    - 2f)
                    ) + minTorqueApplied);

                shipBody.AddTorque(-transform.forward * torque * torqueMultiplier, ForceMode.Force);
            }
                
        }
        else
        {
            if(leftEngine) leftEngineTrail.time = Mathf.Lerp(leftEngineTrail.time, 0f, Time.deltaTime * trailFadeOut);
            if (rightEngine) rightEngineTrail.time = Mathf.Lerp(rightEngineTrail.time, 0f, Time.deltaTime * trailFadeOut);
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

    //TO DO: Max angular and linear speed; El hook se lanza cuando los dos motores pulsan el input
}
