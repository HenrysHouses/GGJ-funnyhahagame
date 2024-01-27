using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float sensitivity = 1f; // Controls the speed of tilting
    private float minAngle = 15f; // The minimum angle the camera can tilt to
    private float maxAngle = 50f; // The maximum angle the camera can tilt to

    private float angle; // The current tilt angle of the camera

    [SerializeField] private AnimationCurve moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        float moveAmount = 0;
        if (mousePosition.y < Screen.height * 0.25f)
        {
            // Move the camera down
            moveAmount = -moveSpeed.Evaluate((0.25f -(mousePosition.y / Screen.height)) * 4);


        }
        else if (mousePosition.y > Screen.height * 0.75f)
        {
            // Move the camera up
            moveAmount = moveSpeed.Evaluate(((mousePosition.y / Screen.height) - 0.75f) * 4);

        }

        // Calculate the tilt angle based on the y-coordinate of the mouse position
        float angleDelta = moveAmount * sensitivity;

        float angle = transform.rotation.eulerAngles.x - angleDelta;

        if (angle < minAngle)
            angle = minAngle;
        else if (angle > maxAngle)
            angle = maxAngle;

        // Apply the tilt to the camera
        Camera.main.transform.rotation = Quaternion.Euler(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

    }
}
