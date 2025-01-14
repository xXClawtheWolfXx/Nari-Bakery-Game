using Godot;
using System;

public partial class ChoppableStation : CounterStation {

    [Export] private ProgressBar progressBar;

    private float currStep = 0;
    private float maxStep = 1;

    public override void AddIngredient(Ingredient ingredient) {
        base.AddIngredient(ingredient);
        //spawn ingredient in the world
        ingredient.Reparent(this);
        ingredient.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
        currStep = ingredient.GetCurrProgress;
        maxStep = ingredientAdded.GetMaxSteps;
        progressBar.Value = currStep / maxStep;
    }

    public override void ProcessIngredient() {
        currStep++;
        ingredientAdded.IncreaseCurrProgress(1);
        progressBar.Value = currStep / maxStep;
        if (currStep >= maxStep)
            return;
    }

    public override Ingredient RemoveIngredient() {
        progressBar.Value = 0;
        return base.RemoveIngredient();
    }

    public override void OnInteract(Node3D body) {
        if (body is Player player) {
            if (player.AreHandsEmpty() && HasIngredient())
                player.PickUp(RemoveIngredient());
            else if (!player.AreHandsEmpty() && !HasIngredient()) {
                AddIngredient(player.PutDown(false));
            }
        }

    }

    public override void OnAltInteract(Node3D body) {
        if (HasIngredient())
            ProcessIngredient();
        base.OnAltInteract(body);
    }
}
