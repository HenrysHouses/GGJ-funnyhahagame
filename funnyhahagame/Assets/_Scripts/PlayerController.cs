using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody HandTarget;
    [SerializeField] private Transform HandBone;
    [SerializeField] private Transform _Start;
    [SerializeField] private AnimationCurve handForce;
    [SerializeField] private float handOffset = 1;
    Camera playerCam;
    [SerializeField] LayerMask RaycastMask;
    playerInput _inputs;
    InputSettings _inputSettings;


    struct playerInput
    {
        public Vector3 MousePosition;
        public float rotation;
        public bool grabbing;
        public Vector3 targetPosition;

        public void set(playerInput inputs)
        {
            MousePosition = inputs.MousePosition;
            rotation = inputs.rotation;
            grabbing = inputs.grabbing;
            targetPosition = inputs.targetPosition;
        }
    }

    struct InputSettings
    {
        public string rotateAxis;
        public KeyCode grab;

        public void set(InputSettings settings)
        {
            rotateAxis = settings.rotateAxis;
            grab = settings.grab;
        }
    }

    void Start()
    {
        playerCam = Camera.main;
        
        InputSettings Controls = new InputSettings();

        Controls.grab = KeyCode.Mouse0;
        Controls.rotateAxis = "Horizontal";
        _inputSettings.set(Controls);

        HandTarget.transform.position = _Start.position;
    }

    // Update is called once per frame
    void Update()
    {
        setInputs();
    }

    void FixedUpdate()
    {
        handleInputs();
    }

    private void setInputs()
    {
        playerInput currentInput = new playerInput();
        currentInput.MousePosition = Input.mousePosition;
        currentInput.rotation = Input.GetAxis(_inputSettings.rotateAxis);
        currentInput.grabbing = Input.GetKey(_inputSettings.grab);

        Ray ray = playerCam.ScreenPointToRay(_inputs.MousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit,10, RaycastMask))
        {
            _inputs.MousePosition.z = Vector3.Distance(playerCam.transform.position, hit.point) - handOffset;  
            
            currentInput.targetPosition = playerCam.ScreenToWorldPoint(_inputs.MousePosition);
            Debug.DrawLine(playerCam.transform.position, currentInput.targetPosition);
        }
        _inputs.set(currentInput);

    }

    private void handleInputs()
    {
        Vector3 direction =  _inputs.targetPosition - HandTarget.transform.position;
        Debug.DrawLine(_inputs.targetPosition, HandTarget.transform.position, Color.red, 0.1f);
        Debug.DrawLine(HandBone.position, HandTarget.transform.position, Color.green, 0.1f);

        float dist = Vector3.Distance(_inputs.targetPosition, HandTarget.transform.position);

        if(dist > 0)
        {
            
        }

        HandTarget.AddForce(direction * handForce.Evaluate(dist));

        float distanceThreshold = 1f;

        if(dist < distanceThreshold)
        {
            Vector3 desiredVelocity = HandTarget.velocity;
            // HandTarget.AddForce(direction * handForce);

            desiredVelocity = desiredVelocity * (0.1f * Mathf.Pow(desiredVelocity.magnitude, -2) - 0.1f);
            
            Debug.DrawLine(HandTarget.transform.position, HandTarget.transform.InverseTransformDirection(desiredVelocity) *3, Color.cyan);
            Debug.Log(desiredVelocity);
            // HandTarget.velocity = desiredVelocity;
        }
    }

}