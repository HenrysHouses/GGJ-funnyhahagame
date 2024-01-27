using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{


    public int timeCut, timeSmashed;

    public float burnedTimer;

    public PlayerController player;

    [field:SerializeField] public LayerMask Layers { get; private set; }

    public virtual void OnPickup(PlayerController playerCon)
    {
        player = playerCon;
    }

    public virtual void OnRelease()
    {
        player = null;
    }
}
