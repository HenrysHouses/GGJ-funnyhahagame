using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowTorch : InteractableObjects
{
    // public float BurnTimer, MaxBurnedtimer = 1;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField] float burnStrength = 6;

    public override void OnPickup(PlayerController playerCon)
    {
        base.OnPickup(playerCon);
        StartCoroutine(Burn());
    }

    public IEnumerator Burn()
    {

        while(player != null)
        {
            Ray ourRay = new Ray(shootPoint.position, shootPoint.forward);
            RaycastHit[] hits = Physics.RaycastAll(ourRay, Mathf.Infinity, 1 << 0);

            Debug.Log("is On: " + hits.Length);

            foreach (RaycastHit hit in hits)
            {
                if(hit.collider.TryGetComponent(out IngredientObject component))
                {
                    if((component.Layers & (1 << 7)) !=0)
                    {
                        component.ingredient.burnProgress += burnStrength * Time.deltaTime;
                        component.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.black, component.ingredient.burnProgress);

                        Debug.Log("is burning: " + hit.collider.gameObject.name);

                        // if(component.ingredient.burnProgress >= 1)
                        // {

                        // }
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}