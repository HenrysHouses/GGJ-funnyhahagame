using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : InteractableObjects
{
    public float hitTimer, hitTimerReset = 1;
    [SerializeField]
    private bool hasBeenBeat;

    // Update is called once per frame
    void Update()
    {
        if (hasBeenBeat)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= hitTimerReset)
            {
                hasBeenBeat = false;
                hitTimer = 0;
            }
        }
    }

    public void Smash(InteractableObjects target)
    {
        if (!hasBeenBeat && (target.Layers & (1 << 7)) != 0)
        {
            if(target.transform.rotation == Quaternion.identity)
            {
                gameObject.GetComponent<AudioSource>().Play();
                target.transform.localScale = new Vector3(target.transform.localScale.x * 1.4f, target.transform.localScale.y * 0.6f, target.transform.localScale.z * 1.4f);
                hasBeenBeat = true;

            }
            else if(target.transform.rotation == Quaternion.FromToRotation(Vector3.zero,Vector3.right))
            {
                gameObject.GetComponent<AudioSource>().Play();
                target.transform.localScale = new Vector3(target.transform.localScale.x * 1.4f, target.transform.localScale.y * 1.4f, target.transform.localScale.z * 0.6f);
                hasBeenBeat = true;

            }
            
            IngredientObject _ingredientObject = target as IngredientObject;

            if(_ingredientObject != null)
            {
                _ingredientObject.ingredient.timesSmashed++;
            }

            Debug.Log("Smash!!");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (player == null)
            return;
        
        InteractableObjects interactionObject = collision.gameObject.GetComponent<InteractableObjects>();
        if (interactionObject)
        {
            if (player.HandTarget.velocity.magnitude > 2.2f)
                Smash(interactionObject);
        }
    }
}
