using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public PlayerController player;
    public bool isPickupp = true;

    [field: SerializeField] public LayerMask Layers { get; private set; }

    public virtual void OnPickup(PlayerController playerCon)
    {
        player = playerCon;
 
       
    }

    public virtual void OnRelease()
    {
        player = null;
    }
}
