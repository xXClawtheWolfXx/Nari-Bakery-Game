using Godot;
using System;

public partial class OvenStation : CounterStation {

    [Export] private Node3D progressUI;
    [Export] private ProgressBar progressBar;

    private float currStep = 0;

    public override void _Ready() {
        progressUI.Visible = false;
    }

    public override void AddIngredient(Ingredient ingredient) {
        base.AddIngredient(ingredient);
        progressUI.Visible = true;

        //spawn ingredient in the world
        ingredient.Reparent(this);
        ingredient.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
        currStep = ingredient.GetCurrProgress;
        maxStep = GetMaxSteps();
        progressBar.Value = currStep / maxStep;
    }

    public override void _Process(double delta) {
        if (HasIngredient()) {
            progressBar.Value += delta;
            ingAdded.IncreaseCurrProgress((float)delta);
        }
    }

    public override Item RemoveIngredient() {
        progressBar.Value = 0;
        progressUI.Visible = false;
        return base.RemoveIngredient();
    }

    public override void OnInteract(Node3D body) {
        if (body is Player player) {
            if (player.AreHandsEmpty() && HasIngredient())
                player.PickUp(RemoveIngredient());
            else if (!player.AreHandsEmpty() && !HasIngredient() && player.GetItem() is Ingredient) {
                AddIngredient((Ingredient)player.PutDown(false));
            }
        }
    }
}
