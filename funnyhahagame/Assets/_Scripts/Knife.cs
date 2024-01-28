using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : InteractableObjects
{
    public float cutTimer, cutTimerReset = 1;
    [SerializeField]
    private bool hasCut;
    public GameObject SpashVFX;

    // Update is called once per frame
    void Update()
    {
       if(hasCut)
        {
            cutTimer += Time.deltaTime;
            if (cutTimer >= cutTimerReset)
            {
                hasCut = false;
                cutTimer = 0;
            }
        }
    }

    public override void OnPickup(PlayerController playerCon)
    {
        base.OnPickup(playerCon);
        transform.rotation = new Quaternion(0.0813010931f,0.0425013602f,-0.990172446f,0.105557606f);
    }

    public void Cut(IngredientObject target)
    {
        if (target.timeCut > 3)
        {
            //Change game objct to shrimp not desroy if time lel
            Destroy(target.gameObject);
            return;
        }

        if(!hasCut && (target.Layers & (1 << 6)) !=0 )
        {
            Destroy(Instantiate(SpashVFX, target.transform.position, Quaternion.identity), 3);

            gameObject.GetComponent<AudioSource>().Play();
            GameObject instance0 = Instantiate(target.gameObject, target.transform.position, Quaternion.identity);
            instance0.transform.localScale = new Vector3(target.transform.localScale.x * 0.5f, target.transform.localScale.y * 0.5f, target.transform.localScale.z * 0.5f);
            instance0.GetComponent<IngredientObject>().timeCut = target.timeCut +1;
            GameObject instance1 = Instantiate(target.gameObject, target.transform.position, Quaternion.identity);
            instance1.transform.localScale = new Vector3(target.transform.localScale.x * 0.5f, target.transform.localScale.y * 0.5f, target.transform.localScale.z * 0.5f);
            instance1.GetComponent<IngredientObject>().timeCut = target.timeCut + 1;

            Destroy(target.gameObject);
            hasCut = true;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (player == null)
            return;

        IngredientObject objectInteraction = collision.gameObject.GetComponent<IngredientObject>();
        if (objectInteraction)
        {
            if(player.HandTarget.velocity.magnitude > 2.2f)
             Cut(objectInteraction);
        }
    }
}
