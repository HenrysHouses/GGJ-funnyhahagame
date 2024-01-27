using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform LeftLimit;
    [SerializeField] private Transform RightLimit;

    [SerializeField] private GameObject Player;

    private float sensitivity = 1f; // Controls the speed of tilting
    private float minAngle = 15f; // The minimum angle the camera can tilt to
    private float maxAngle = 50f; // The maximum angle the camera can tilt to

    private float angle; // The current tilt angle of the camera

    [SerializeField] private AnimationCurve moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        UpdateHorizontal();
        UpdateVertical();
    }

    private void UpdateHorizontal()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Calculate the amount to move the GameObject
        float moveAmount = 0;
        if (mousePosition.x < Screen.width * 0.25f)
        {
            // Debug.Log("Scalar" + (Screen.width * 0.25f - mousePosition.x) / Screen.width * 0.25f);
            // Move to the left
            moveAmount = -moveSpeed.Evaluate((Screen.width * 0.25f - mousePosition.x) / Screen.width * 0.25f);


        }
        else if (mousePosition.x > Screen.width * 0.75f)
        {
            // Move to the right
            moveAmount = moveSpeed.Evaluate((mousePosition.x - Screen.width * 0.75f) / Screen.width * 0.25f);

        }

        // Adjust the GameObject's position
        if (Mathf.Abs(moveAmount) > 0)
        {
            var new_z = Player.transform.position.z + moveAmount * 20;

            if (new_z < LeftLimit.position.z)
                new_z = LeftLimit.position.z;
            else if (new_z > RightLimit.position.z)
                new_z = RightLimit.position.z;

            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, new_z);
        }

    }
    // Update is called once per frame
    void UpdateVertical()
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
