using Godot;
using System;
using Godot.Collections;

public partial class CookingManager : Node3D {

    private static CookingManager instance;
    public static CookingManager Instance { get => instance; }

    [Export] private Array<RecipieR> allRecipies;

    public Array<RecipieR> GetAllRecipies { get => allRecipies; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        instance = this;
    }


}
