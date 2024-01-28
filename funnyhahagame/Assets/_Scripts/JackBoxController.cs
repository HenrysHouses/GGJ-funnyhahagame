using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackBoxController : InteractableObjects
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject Origin;
    public Recipe recipe;
    public GameObject FishPrefab;
    public GameObject TopBun;
    public GameObject patty;
    public GameObject BottomBun;
    public GameObject fullburger;
    public GameObject brain;
    public GameObject toiletPaper;
    public GameObject BabyPowder;

    public override void OnPickup(PlayerController playerCon)
    {
        base.OnPickup(playerCon);
        anim.Play("Turn");
    }

    public override void OnRelease()
    {
        base.OnRelease();
        anim.Play("Stop");

        for (int i = 0; i < recipe.Ingredients.Count; i++)
        {
            switch(recipe.Ingredients[i].Type)
            {
                case IngredientType.Fish:
                    Instantiate(FishPrefab, Origin.transform.position, Quaternion.identity);
                    break;
                case IngredientType.BabyPowder:
                    Instantiate(BabyPowder, Origin.transform.position, Quaternion.identity);
                    break;
                case IngredientType.BottomBun:
                    Instantiate(BottomBun, Origin.transform.position, Quaternion.identity);
                    break;
                case IngredientType.Brain:
                    Instantiate(brain, Origin.transform.position, Quaternion.identity);
                    break;
                case IngredientType.TopBun:
                    Instantiate(TopBun, Origin.transform.position, Quaternion.identity);
                    break;
                case IngredientType.Patty:
                    Instantiate(patty, Origin.transform.position, Quaternion.identity);
                    break;
                case IngredientType.toiletPaper:
                    Instantiate(toiletPaper, Origin.transform.position, Quaternion.identity);
                    break;
            }
        }

        Destroy(Origin);
        Destroy(gameObject);
    }
}
