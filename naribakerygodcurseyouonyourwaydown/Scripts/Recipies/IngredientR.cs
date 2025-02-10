using Godot;
using System;

[GlobalClass]

public partial class IngredientR : Resource {
    //[Export] private PackedScene ingredientScene;
    [Export] private float maxSteps;
    [Export] public Texture2D IngredientSprite { get; private set; }

    public float GetMaxSteps { get => maxSteps; }
    //public PackedScene GetIngredientScene { get => ingredientScene; }



    public override bool Equals(object obj) {
        if (obj is not IngredientR) return false;
        IngredientR other = (IngredientR)obj;
        // if (other.ingredientScene != ingredientScene) return false;
        if (other.maxSteps != maxSteps) return false;
        return true;
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }
}
