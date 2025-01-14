using Godot;
using System;

public partial class Ingredient : StaticBody3D {

    [Export] private IngredientR ingredientR;
    [Export] private PackedScene choppedScene;

    private float currProgress;

    public float GetMaxSteps { get => ingredientR.GetMaxSteps; }
    public float GetCurrProgress { get => currProgress; }
    public PackedScene GetChoppedScene { get => choppedScene; }

    //In case player takes an unfinished ing out of OvenStation or ChoppingStation
    public void IncreaseCurrProgress(float amt) {
        currProgress += amt;
        //what if go over
    }

}
