using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipEngine : MonoBehaviour
{
    #region Ship Parts
    [SerializeField] Rigidbody shipBody;
    [SerializeField] TrailRenderer leftEngineTrail;
    [SerializeField] TrailRenderer rightEngineTrail;
    [SerializeField] GameObject brakeAnimSpawnPoint;
    [SerializeField] GameObject brakeAnimPrefab;
    
    [SerializeField] [Tooltip("Activar si es el motor izquierdo")] bool leftEngine = false;
    [SerializeField] [Tooltip("Activar si es el motor derecho")]  bool rightEngine = true;
    #endregion


    #region Input
    float xAxis = 0f;

    bool thrustInput = false;
    bool brakeInput = false;
    bool brakeReleaseInput = false;
    #endregion

    #region Movement variables
    [SerializeField] [Tooltip("Velocidad a la que rota el motor")] float rotationSpeed = 3f;
    [SerializeField] [Tooltip("Velocidad lineal que aplica el motor")] float thrustSpeed = 10f;
    [SerializeField] [Tooltip("Torque que aplica el motor")] float torque = 10f;
    [SerializeField] [Range(0f, 1f)] [Tooltip("Porcentaje de torque mínimo que se aplica cuando el motor está en vertical")] float minTorqueApplied = 0.2f;

    [SerializeField] float trailFadeOut = 1f;

    [SerializeField] float maxVelocity = 5f;
    [SerializeField] float maxAngularVelocity = 2.5f;

    [SerializeField] float brakeVelocitySmooothTime = 0.4f;
    [SerializeField] float brakeAngularVelocitySmoothTime = 0.4f;
    #endregion

    bool firsFrameBrake = true;

    [SerializeField] AudioSource rightEngineAudioSource;
    [SerializeField] AudioSource leftEngineAudioSource;

    //[SerializeField] AudioSource constantBrake;
    [SerializeField] AudioSource burstBrake;


    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

        if (leftEngine) getLeftEngineInput();
        else if (rightEngine) getRightEngineInput();

        if (brakeInput)
        {
            if (firsFrameBrake)
            {
                firsFrameBrake = false;

                //Spawn animation
                GameObject brakeAnim = Instantiate(brakeAnimPrefab, brakeAnimSpawnPoint.transform.position, Quaternion.Euler(0f, 0f, 0f));
                brakeAnim.transform.rotation = Quaternion.LookRotation(shipBody.transform.up, -shipBody.transform.forward);
                brakeAnim.GetComponent<Animator>().Play("Brakes");

                //BrakeSFX
                burstBrake.PlayOneShot(burstBrake.clip);
            }
        }

        if (brakeReleaseInput)
        {
            firsFrameBrake = true;
            //constantBrake.volume = 0f;

            //shipBody.angularVelocity = Vector3.zero;
            //shipBody.velocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        //Engine rotation
        if (leftEngine)
        {
            RotateEngine(90f, 180f);
        }

        else if (rightEngine)
        {
            RotateEngine(180f, 270f);
        }

        //Thrust
        if (!brakeInput && thrustInput)
        {
            if(shipBody.velocity.magnitude < maxVelocity)
                shipBody.AddForce(transform.up * thrustSpeed, ForceMode.Force);

            float torqueMultiplier = 0f;

            if (leftEngine)
            {
                leftEngineTrail.time = 1f;
                leftEngineAudioSource.volume = 1f;

                torqueMultiplier = Mathf.Clamp01(Mathf.Abs(
                    ((Mathf.Lerp(90f, 180f, (transform.localEulerAngles.y - 90f)
                    /
                    (180f - 90f)) / (180f - 90f))
                    - 2f)
                    ) + minTorqueApplied);

                //Debug.Log("Left engine Torque: " + transform.forward * torque * torqueMultiplier);

                if (shipBody.angularVelocity.magnitude < maxAngularVelocity)
                    shipBody.AddTorque(transform.forward * torque * torqueMultiplier, ForceMode.Force);
            }

            if (rightEngine)
            {
                rightEngineTrail.time = 1f;
                rightEngineAudioSource.volume = 1f;

                torqueMultiplier = Mathf.Clamp01(Mathf.Abs(
                    ((Mathf.Lerp(180f, 270f, (transform.localEulerAngles.y - 180f)
                    /
                    (270f - 180f)) / (270f - 180f))
                    - 2f)
                    ) + minTorqueApplied);

                //Debug.Log("Right engine Torque: " + transform.forward * torque * torqueMultiplier);

                if (shipBody.angularVelocity.magnitude < maxAngularVelocity)
                    shipBody.AddTorque(-transform.forward * torque * torqueMultiplier, ForceMode.Force);
            }
        }
        else
        {
            if (leftEngine)
            {
                leftEngineAudioSource.volume = Mathf.Lerp(leftEngineAudioSource.volume, 0f, Time.deltaTime * trailFadeOut);
                leftEngineTrail.time = Mathf.Lerp(leftEngineTrail.time, 0f, Time.deltaTime * trailFadeOut);
            }

            if (rightEngine)
            {
                rightEngineAudioSource.volume = Mathf.Lerp(rightEngineAudioSource.volume, 0f, Time.deltaTime * trailFadeOut);
                rightEngineTrail.time = Mathf.Lerp(rightEngineTrail.time, 0f, Time.deltaTime * trailFadeOut);
            }
        }
        

        if (brakeInput)
        {
            //constantBrake.volume = 1f;

            Vector3 velocity = Vector3.zero;

            //shipBody.velocity = Vector3.zero;
            //shipBody.angularVelocity = Vector3.zero;

            shipBody.velocity = Vector3.SmoothDamp(shipBody.velocity, Vector3.zero, ref velocity, brakeVelocitySmooothTime);
            
            shipBody.AddTorque(-shipBody.angularVelocity * brakeAngularVelocitySmoothTime);

            leftEngineAudioSource.volume = Mathf.Lerp(leftEngineAudioSource.volume, 0f, Time.deltaTime * trailFadeOut);
            rightEngineAudioSource.volume = Mathf.Lerp(rightEngineAudioSource.volume, 0f, Time.deltaTime * trailFadeOut);

            leftEngineTrail.time = Mathf.Lerp(leftEngineTrail.time, 0f, Time.deltaTime * trailFadeOut);
            rightEngineTrail.time = Mathf.Lerp(rightEngineTrail.time, 0f, Time.deltaTime * trailFadeOut);
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

        thrustInput = Input.GetKey(KeyCode.W);
        brakeInput = Input.GetKey(KeyCode.Space);
        brakeReleaseInput = Input.GetKeyUp(KeyCode.Space);
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

        thrustInput = Input.GetKey(KeyCode.UpArrow);
        brakeInput = Input.GetKey(KeyCode.Space);
        brakeReleaseInput = Input.GetKeyUp(KeyCode.Space);
        //Right side Gamepad Input
    }
    #endregion

    void RotateEngine(float minAngle, float maxAngle)
    {
        float currentRotation = transform.localEulerAngles.y;
        float newRotation = rotationSpeed * xAxis;

        if (rightEngine)
        {

            if (transform.localEulerAngles.y < (179f + rotationSpeed))
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f + rotationSpeed, transform.localEulerAngles.z);
            }
        }

        if (leftEngine)
        {
            if (transform.localEulerAngles.y > (179f - rotationSpeed))
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f - rotationSpeed, transform.localEulerAngles.z);
            }
        }

        if ( (transform.localEulerAngles.y + newRotation) > minAngle && (transform.localEulerAngles.y + newRotation) < maxAngle)
            transform.Rotate(new Vector3(0f, 0f, -newRotation), Space.Self);
    }

    public void ClearTrails()
    {
        leftEngineTrail.Clear();
        rightEngineTrail.Clear();

        leftEngineAudioSource.volume = 0f;
        rightEngineAudioSource.volume = 0f;
    }

    /*
    Vector3[] leftTrailPosition;
    Vector3[] rightTrailPosition;

    Vector3[] newLeftTrailPosition;
    Vector3[] newRightTrailPosition;

    public void TransportTrails(bool beforeTransport, Vector3 hitPoint, Vector3 direction, float offset)
    {
        //leftEngineTrail.Clear();
        //rightEngineTrail.Clear();

        if(beforeTransport)
        {
            //leftEngineTrail.Clear();
            //rightEngineTrail.Clear();

            leftTrailPosition = new Vector3[leftEngineTrail.positionCount];
            rightTrailPosition = new Vector3[rightEngineTrail.positionCount];

            newLeftTrailPosition = new Vector3[leftEngineTrail.positionCount - 1];
            newRightTrailPosition = new Vector3[rightEngineTrail.positionCount - 1];

            leftEngineAudioSource.volume = 0f;
            rightEngineAudioSource.volume = 0f;

            leftEngineTrail.emitting = false;
            rightEngineTrail.emitting = false;

            leftEngineTrail.GetPositions(leftTrailPosition);
            rightEngineTrail.GetPositions(rightTrailPosition);

            foreach (Vector3 position in rightTrailPosition)
                Debug.Log(position);

            for(int i = 0; i < rightTrailPosition.Length; i++)
            {
                if (i != 0)
                    Debug.Log(rightTrailPosition[i].y - rightTrailPosition[i - 1].y);
            }

            leftEngineTrail.Clear();
            rightEngineTrail.Clear();
        }
        else
        {
            //leftEngineAudioSource.volume = 0f;
            //rightEngineAudioSource.volume = 0f;

            leftEngineTrail.Clear();
            rightEngineTrail.Clear();

            for (int i = 0; i < (leftTrailPosition.Length - 1); i++)
            {
                Vector3 vertexDistanceOffset = Vector3.zero;

                if (i == 0)
                {
                    newLeftTrailPosition[i] = leftEngineTrail.transform.position;
                }
                else
                {
                    vertexDistanceOffset =  (leftTrailPosition[i - 1] - leftTrailPosition[i]);

                    newLeftTrailPosition[i].x = leftEngineTrail.transform.position.x - i * leftEngineTrail.minVertexDistance * 10f * direction.x * vertexDistanceOffset.x;
                    newLeftTrailPosition[i].y = leftEngineTrail.transform.position.y - i * leftEngineTrail.minVertexDistance * 10f * direction.y * vertexDistanceOffset.y;
                }
            }
            for (int i = 0; i < (rightTrailPosition.Length - 1); i++)
            {
                Vector3 vertexDistanceOffset = Vector3.zero;

                if(i == 0)
                {
                    newRightTrailPosition[i] = rightEngineTrail.transform.position;
                }
                else
                {
                    vertexDistanceOffset =  (rightTrailPosition[i - 1] - rightTrailPosition[i]);

                    newRightTrailPosition[i].x = rightEngineTrail.transform.position.x - i * rightEngineTrail.minVertexDistance * 10f * direction.x * vertexDistanceOffset.x;
                    newRightTrailPosition[i].y = rightEngineTrail.transform.position.y - i * rightEngineTrail.minVertexDistance * 10f * direction.y * vertexDistanceOffset.y;
                }
            }

            leftEngineTrail.AddPositions(newLeftTrailPosition);
            rightEngineTrail.AddPositions(newRightTrailPosition);

            leftEngineTrail.emitting = true;
            rightEngineTrail.emitting = true;
        }

    }*/
    
}
