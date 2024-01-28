using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    Fish,
    TopBun,
    BottomBun,
    Patty,
    Brain,
    BabyPowder
}

[System.Serializable]
public class Recipe
{
    public List<Ingredient> Ingredients = new List<Ingredient>();
}

[System.Serializable]
public struct Ingredient
{
    public IngredientType Type;
    public int Amount;
    public int timesCut;
    public int timesSmashed;
    public float burnProgress;
}

[CreateAssetMenu(menuName = "funnyhahagame/Recipe")]
public class DishRecipe_ScriptableObject : ScriptableObject
{
    [field:SerializeField] public Recipe recipe {get; private set;}
    public List<Ingredient> GetIngredients() => recipe.Ingredients;
}