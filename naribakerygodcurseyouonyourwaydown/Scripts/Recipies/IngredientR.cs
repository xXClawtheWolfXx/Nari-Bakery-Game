using Godot;
using System;

[GlobalClass]

public partial class IngredientR : Resource {
    //[Export] private PackedScene ingredientScene;
    [Export] private IngredientR choppedIngredient;
    [Export] private IngredientR ovenBakedIngredient;
    [Export] private float maxSteps;

    public float GetMaxSteps { get => maxSteps; }
    //public PackedScene GetIngredientScene { get => ingredientScene; }
    public IngredientR GetChoppedIngredient { get => choppedIngredient; }
    public IngredientR GetOvenBakedIngredient { get => ovenBakedIngredient; }



    public override bool Equals(object obj) {
        if (obj is not IngredientR) return false;
        IngredientR other = (IngredientR)obj;
        // if (other.ingredientScene != ingredientScene) return false;
        if (other.choppedIngredient != choppedIngredient) return false;
        if (other.ovenBakedIngredient != ovenBakedIngredient) return false;
        if (other.maxSteps != maxSteps) return false;
        return true;
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }
}
