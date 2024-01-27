using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float cutTimer, cutTimerReset = 1;
    [SerializeField]
    private bool hasCut;
    
   
  
    void Start()
    {
       
        
    }

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

    public void Cut(InteractableObjects target)
    {
        if(!hasCut)
        {
            gameObject.GetComponent<AudioSource>().Play();
            GameObject instance0 = Instantiate(target.gameObject, target.transform.position, Quaternion.identity);
            instance0.transform.localScale = new Vector3(target.transform.localScale.x * 0.5f, target.transform.localScale.y * 0.5f, target.transform.localScale.z * 0.5f);
           GameObject instance1 = Instantiate(target.gameObject, target.transform.position, Quaternion.identity);
           instance1.transform.localScale = new Vector3(target.transform.localScale.x * 0.5f, target.transform.localScale.y * 0.5f, target.transform.localScale.z * 0.5f);

            Destroy(target.gameObject);
            hasCut = true;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        InteractableObjects objectstointeract = collision.gameObject.GetComponent<InteractableObjects>();
        if (objectstointeract)
        {
            Cut(objectstointeract);
        }
    }
    


}
