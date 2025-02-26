using Godot;
using System;

[Serializable]
public partial class IngredientInfo {
    public IngredientR ingredientR;
    public Ingredient ingredient;

    public IngredientInfo(IngredientR ingR, Ingredient ing) {
        ingredientR = ingR;
        ingredient = ing;
    }
}
