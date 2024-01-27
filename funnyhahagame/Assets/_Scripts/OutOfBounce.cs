using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounce : MonoBehaviour
{
    [SerializeField] Transform ReSpawn;

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlateController plate))
        {
            plate.transform.position = ReSpawn.position;
        }
        else if(other.TryGetComponent(out InteractableObjects component))
        {
            
        } 
    }
}
