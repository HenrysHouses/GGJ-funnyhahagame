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
    [SerializeField] LayerMask IgnoreMask;
    [SerializeField] LayerMask GrabbableObjectMask;
    playerInput _inputs;
    InputSettings _inputSettings;
    GameObject grabbed_object;

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
        pickupObject();

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

        HandTarget.transform.position = _inputs.targetPosition;
    }

    GameObject FindClosestObjectWithinRadius(Vector3 center, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(center, radius);

        Collider closestCollider = null;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            if (!collider.gameObject.CompareTag("Grabbable")) continue;

            float distance = Vector3.Distance(center, collider.bounds.center);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCollider = collider;
            }
        }

        return closestCollider ? closestCollider.gameObject : null;
    }

    private void pickupObject() {
        if (_inputs.grabbing && grabbed_object == null)
        {

            grabbed_object = FindClosestObjectWithinRadius(HandBone.transform.position, .4f);

            if (grabbed_object != null)
            {
                Debug.DrawLine(HandBone.position, grabbed_object.transform.position, Color.magenta, 0.2f);
                Debug.Log("Picked up an object");

                grabbed_object.transform.SetParent(HandBone);
                grabbed_object.GetComponent<Rigidbody>().isKinematic = true;
                grabbed_object.layer = 2;
            }
            else
                Debug.Log("No object found within range");


        }
        else if (!_inputs.grabbing && grabbed_object != null) {
            grabbed_object.transform.parent = null;
            grabbed_object.layer = 0;
            grabbed_object.GetComponent<Rigidbody>().isKinematic = false;
            grabbed_object = null;
        }

        
    }
}