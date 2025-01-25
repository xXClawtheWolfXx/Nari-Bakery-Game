using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;


[GlobalClass]
public partial class RecipieR : Resource {

    [Export] private Array<RecipieStep> recipieSteps = new();
    [Export] private PackedScene finalDishScene;

    public PackedScene GetFinalDishScene { get => finalDishScene; }

    public bool HasAllIngredients(List<IngredientR> ingredients) {
        foreach (RecipieStep rs in recipieSteps)
            if (!ingredients.Contains(rs.GetIngredientR))
                return false;
        return true;
    }


}
