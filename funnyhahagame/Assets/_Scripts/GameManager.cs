using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;


public class GameManager : MonoBehaviour
{
    // [SerializeField] Transform PlateDispenser;
    [SerializeField] GameObject PlatePrefab;
    [SerializeField] GameObject JackBoxPrefab;
    [SerializeField] DishRecipe_ScriptableObject[] Dishes;

    public TMP_Text TextComponent;
    public GameObject current_plate;
    public GameObject current_ingredientBox;
    private int plate_index = 0;
    List<AudioSource> sounds = new List<AudioSource>();


    

    // Start is called before the first frame update
    void Start()
    {
        instantiatePlate();
        Debug.Log("Number of dishes" + Dishes.Length);
    }

    void Update()
    {
        if (current_plate && current_plate.GetComponent<PlateController>().handed_in)
        {
            plate_index++;
            instantiatePlate();
        }
    }





    public void instantiatePlate()
    {
        if (current_plate != null)
        {
            Destroy(current_plate);
        }
        if (plate_index < Dishes.Length)
        {
            DishRecipe_ScriptableObject recipe = Dishes[plate_index];

            TextComponent.text = "Recipe: \n";

            foreach (Ingredient ing in recipe.GetIngredients())
            {
                TextComponent.text += ing.ToString() + "\n";
            }

            current_plate = Instantiate(PlatePrefab);
            current_plate.transform.position = transform.position - new Vector3(0, 0, 2);
            current_plate.GetComponent<PlateController>().init(recipe.recipe);

            current_ingredientBox = Instantiate(JackBoxPrefab, transform.position, Quaternion.identity);
            current_ingredientBox.GetComponentInChildren<JackBoxController>().recipe = recipe.recipe;
        
        }
            
        
        

    }
}
