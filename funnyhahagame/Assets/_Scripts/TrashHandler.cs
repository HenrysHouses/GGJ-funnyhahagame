using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandler : MonoBehaviour
{
    GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlateController obj))
        {
            gameManager.GetComponent<GameManager>().instantiatePlate();

        }
    }
}
