using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowDelivery : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = GameObject.Find("Player");
        if (gameObject != null)
            player = gameObject.GetComponent<PlayerController>();
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().TryGetComponent(out PlateController plate))
        {
            if (plate.completed)
            {
                Debug.Log("Platening Completed");
                plate.handed_in = true;
                player.loseObject();
            }
            return;
        }
    }
}
