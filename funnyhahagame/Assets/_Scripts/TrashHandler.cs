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
            Destroy(gameManager.GetComponent<GameManager>().current_ingredientBox);
        }
        else if (other.TryGetComponent(out IngredientObject ing))
        {
            Destroy(ing.gameObject);
            Destroy(gameManager.GetComponent<GameManager>().current_plate);
            Destroy(gameManager.GetComponent<GameManager>().current_ingredientBox);
            

            gameManager.GetComponent<GameManager>().instantiatePlate();
        }
        else if (other.TryGetComponent(out InteractableObjects intobj))
        {
            intobj.transform.position = new Vector3(-1.5f, 0, -5);
        }
    }
}
