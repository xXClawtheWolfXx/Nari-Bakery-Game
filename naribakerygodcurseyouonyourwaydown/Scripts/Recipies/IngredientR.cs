using Godot;
using System;

[GlobalClass]

public partial class IngredientR : Resource {
    [Export] private PackedScene ingredientScene;

    public PackedScene GetIngredientScene { get => ingredientScene; }

    public override bool Equals(object obj) {
        if (obj is not IngredientR) return false;
        IngredientR other = (IngredientR)obj;
        if (other.ingredientScene != ingredientScene) return false;
        return true;
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }
}
