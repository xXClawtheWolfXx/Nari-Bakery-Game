using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

public partial class CookingManager : Node3D {

    private static CookingManager instance;
    public static CookingManager Instance { get => instance; }



    [Export] public Array<RecipieR> AllRecipies { get; private set; }
    [Export] private int maxNumRecipies = 5;

    [Export] public OvenStation OvenStation { get; private set; }
    [Export] public MixStation MixStation { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        SendRandomRecipies();
    }

    public void SendRandomRecipies() {
        GameEvents.RaiseCookingRecipiesChosen(GetRandomRecipies());

    }

    public List<RecipieR> GetRandomRecipies() {
        List<RecipieR> randRecipies = new List<RecipieR>();
        while (randRecipies.Count < maxNumRecipies) {
            int randNum = GD.RandRange(0, AllRecipies.Count - 1);
            if (!randRecipies.Contains(AllRecipies[randNum])) {
                randRecipies.Add(AllRecipies[randNum]);
            }
        }

        return randRecipies;
    }

}
