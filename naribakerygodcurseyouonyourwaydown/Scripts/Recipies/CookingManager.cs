using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

public partial class CookingManager : Node3D {

    private static CookingManager instance;
    public static CookingManager Instance { get => instance; }

    [Export] private Array<RecipieR> allRecipies;
    [Export] private int maxNumRecipies = 5;
    public Array<RecipieR> GetAllRecipies { get => allRecipies; }

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
            int randNum = GD.RandRange(0, allRecipies.Count - 1);
            if (!randRecipies.Contains(allRecipies[randNum])) {
                randRecipies.Add(allRecipies[randNum]);
                GD.PrintS(allRecipies[randNum].GetRecipieName);
            }
        }

        return randRecipies;
    }

}
