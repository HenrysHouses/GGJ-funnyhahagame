using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowTorch : InteractableObjects
{
    public float BurnTimer, MaxBurnedtimer = 1;
    [SerializeField]
    private bool hasBeenBurned;
    [SerializeField]
    private Transform Shootpoint;




    // Update is called once per frame
    void Update()
    {


        if (player == null)
            return;

    }

    public override void OnPickup(PlayerController playerCon)
    {
        base.OnPickup(playerCon);

        StartCoroutine(Burn());
           

       

        
    }



    public override void OnRelease()
    {
        base.OnRelease();
        StopAllCoroutines();
    }

    public IEnumerator Burn()
    {

        Ray ourRay = new Ray(Shootpoint.position, Shootpoint.forward);

        RaycastHit[] hits = Physics.RaycastAll(ourRay, Mathf.Infinity, 1 << 0);

        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.TryGetComponent(out InteractableObjects component))
            {
               if((component.Layers & (1 << 7)) !=0 && hasBeenBurned)
                {




                 //   component.GetComponent<Renderer>().material.color = 


                }
            }
        }

        if (burnedTimer > 10)
        {
          //  Destroy(target.gameObject);
            yield break;
        }

        if (burnedTimer == burnedTimer)
        {

            //    target.GetComponent<Renderer>().material.color
            yield return new WaitForEndOfFrame();
        }

    }


}
