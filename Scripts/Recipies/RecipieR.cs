using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;


[GlobalClass]
public partial class RecipieR : Resource {

    [Export] public bool IsMixRecipie { get; private set; }
    [Export] public string RecipieName { get; private set; }
    [Export] public PackedScene FinalDishScene { get; private set; }

    [Export] private Array<RecipieStep> recipieSteps = new();

    public bool HasAllIngredients(List<IngredientR> ingredients) {
        foreach (RecipieStep rs in recipieSteps)
            if (!ingredients.Contains(rs.GetIngredientR))
                return false;
        return true;
    }


}
