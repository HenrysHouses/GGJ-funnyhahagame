using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObject : InteractableObjects
{
    [SerializeField] public Ingredient ingredient;
    private Rigidbody _rigidbody;
    private Collider _collider;

    internal virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    internal virtual void OnCollisionEnter(Collision other)
    {
        if(player == null)
            return;

        if(other.collider.TryGetComponent(out PlateController plate))
        {
            plate.addToPlate(this);
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            player.loseObject();
            return;
        }
    }
}
