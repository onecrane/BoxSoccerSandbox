using UnityEngine;

public class DriveControls : MonoBehaviour
{
    #region Properties

    [Range(0f, 500f)]
    public float maxSpeed = 10;

    [Range(0f, 100f)]
    public float accelerationRate = 1;

    [Range(0f, 1f)]
    public float frictionDecay = 0.1f;

    [Range(0f, 500f)]
    public float steerRate = 1;

    [Range(0f, 50000f)]
    public float brakeRate = 10;

    public float currentSpeed { get; private set; }

    #endregion

    #region Utilities

    float FDT { get { return Time.fixedDeltaTime; } }

    #endregion

    #region Controls

    void FixedUpdate()
    {
        HandleSpeedInput();
        HandleSteeringInput();
    }

    private void HandleSteeringInput()
    {
        float steerDelta = steerRate * FDT;

        bool inputLeft = Input.GetKey(KeyCode.A), inputRight = Input.GetKey(KeyCode.D);

        if (!(inputLeft && inputRight))
        {
            if (inputLeft) transform.Rotate(transform.up, -steerDelta);
            if (inputRight) transform.Rotate(transform.up, steerDelta);
        }
    }

    private void HandleSpeedInput()
    {
        bool inputForward = Input.GetKey(KeyCode.W);
        bool inputReverse = Input.GetKey(KeyCode.S);

        float brakeDelta = brakeRate * FDT, speedDelta = accelerationRate * FDT, frictionDelta = frictionDecay * FDT;

        // Exclusive OR to determine if user-controlled speed change is applied
        if (!(inputForward && inputReverse))
        {
            if (inputForward)
            {
                if (currentSpeed < 0)
                    currentSpeed = Mathf.Clamp(currentSpeed + brakeDelta, currentSpeed, 0);
                else
                    currentSpeed += speedDelta;
            }

            if (inputReverse)
            {
                if (currentSpeed > 0)
                    currentSpeed = Mathf.Clamp(currentSpeed - brakeDelta, 0, currentSpeed);
                else
                    currentSpeed -= speedDelta;
            }
        }
        else
        {
            currentSpeed *= (1.0f - frictionDelta);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
        GetComponent<Rigidbody>().velocity = transform.forward * currentSpeed;
    }

    #endregion
}