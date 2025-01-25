using Godot;
using System;

public partial class Ingredient : Item {

    [Export] private IngredientR ingredientR;
    [Export] private PackedScene choppedScene;

    private float currProgress;

    public IngredientR GetIngredientR { get => ingredientR; }
    public float GetCurrProgress { get => currProgress; }
    public PackedScene GetChoppedScene { get => choppedScene; }

    public float GetMaxSteps() {
        if (ingredientR == null)
            return 0;
        return ingredientR.GetMaxSteps;
    }
    //In case player takes an unfinished ing out of OvenStation or ChoppingStation
    public void IncreaseCurrProgress(float amt) {
        currProgress += amt;
        //what if go over
    }

}
