using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{


    public int timeCut, timeSmashed;
    private Rigidbody _rigidbody;
    private Collider _collider;
    public float burnedTimer;

    public PlayerController player;

    [field:SerializeField] public LayerMask Layers { get; private set; }

    internal virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public virtual void OnPickup(PlayerController playerCon)
    {
        player = playerCon;
    }

    public virtual void OnRelease()
    {
        player = null;
    }

    internal virtual void OnCollisionEnter(Collision other)
    {
        if(player == null)
            return;

        if(other.collider.TryGetComponent(out PlateController plate))
        {
            plate.addToPlate(transform);
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            player.loseObject();
            return;
        }
    }
}
