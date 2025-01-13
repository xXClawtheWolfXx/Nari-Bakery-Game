using Godot;
using System;

public partial class Ingredient : StaticBody3D {

    [Export] private IngredientR ingredientR;

    private float currProgress;

    //In case player takes an unfinished ing out of OvenStation or ChoppingStation
    public void IncreaseCurrProgress(float amt) {
        currProgress += amt;
        //what if go over
    }

}
