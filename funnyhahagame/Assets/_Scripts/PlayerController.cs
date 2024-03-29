using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [field:SerializeField] public Rigidbody HandTarget { get; private set; }
    [SerializeField] private Transform HandBone;
    [SerializeField] private Transform StretchBone;
    [SerializeField] private float stretchSpeed = 1;
    [SerializeField] private float stretchThreshold = 0.01f;
    private float stretchMinMagnitude;
    Vector3 targetStretchLength;
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

        stretchMinMagnitude = StretchBone.localPosition.magnitude;
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

        // HandTarget.transform.position = _inputs.targetPosition;

        float dist = Vector3.Distance(_inputs.targetPosition, HandTarget.transform.position);

        HandTarget.AddForce(direction * handForce.Evaluate(dist) * 5);

        Vector3 damping = -HandTarget.velocity * (Mathf.Clamp(handForce.keys[1].value * 5 - dist + 1, 0, handForce.keys[1].value));
        HandTarget.AddForce(damping);

        Debug.DrawLine(HandTarget.transform.position, (HandTarget.transform.position +  damping) *3, Color.cyan);

        float armStretchLength = Vector3.Distance(HandTarget.transform.position, HandBone.position);
        targetStretchLength = StretchBone.transform.localPosition;

        Vector3 StretchDir = HandTarget.transform.position - HandBone.position;
        Debug.DrawLine(HandBone.position, StretchDir, Color.magenta);

        if((armStretchLength - 0.05678258f) < stretchThreshold && (armStretchLength - 0.05678258f) > -stretchThreshold)
            return;

        if(Vector3.Dot(HandBone.InverseTransformDirection(StretchDir), HandBone.right) < 0)
            targetStretchLength.y = Mathf.Clamp(StretchBone.transform.localPosition.y + armStretchLength * stretchSpeed, stretchMinMagnitude, 3);
        else
            targetStretchLength.y = Mathf.Clamp(StretchBone.transform.localPosition.y - 1 * stretchSpeed, stretchMinMagnitude, 3);
    }

    void LateUpdate()
    {
        StretchBone.transform.localPosition =  targetStretchLength;
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

    public void loseObject() => pickupObject(true);

    private void pickupObject(bool lostObject = false) {
        if (_inputs.grabbing && grabbed_object == null && !lostObject)
        {
            grabbed_object = FindClosestObjectWithinRadius(HandBone.transform.position, .4f);

            if (grabbed_object != null)
            {
                Debug.DrawLine(HandBone.position, grabbed_object.transform.position, Color.magenta, 0.2f);

                InteractableObjects grabbable = grabbed_object.GetComponent<InteractableObjects>();
                if(grabbable.isPickupp)
                {
                    grabbed_object.transform.SetParent(HandBone);
                    grabbed_object.transform.localPosition = Vector3.zero;
                    grabbed_object.GetComponent<Rigidbody>().isKinematic = true;
                    grabbed_object.layer = 2;

                }

                    grabbable.OnPickup(this);


            }
            else
                Debug.Log("No object found within range");
        }
        else if (!_inputs.grabbing && grabbed_object != null || lostObject) {
            if(!lostObject)
            {
                grabbed_object.transform.parent = null;
                grabbed_object.GetComponent<Rigidbody>().isKinematic = false;
                grabbed_object.GetComponent<Rigidbody>().velocity = HandTarget.velocity;
                grabbed_object.layer = 0;

            }
            grabbed_object.GetComponent<InteractableObjects>().OnRelease();
            grabbed_object = null;
        }
    }
}