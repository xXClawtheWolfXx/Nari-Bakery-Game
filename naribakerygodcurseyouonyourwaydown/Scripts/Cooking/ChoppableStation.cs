using Godot;
using System;

public partial class ChoppableStation : CounterStation {

    [Export] private Node3D progressUI;
    [Export] private ProgressBar progressBar;

    private float currStep = 0;
    private float maxStep = 1;
    private bool hasSpawned = false;

    public override void _Ready() {
        progressUI.Visible = false;
    }

    public override void AddIngredient(Ingredient ingredient) {
        progressUI.Visible = true;

        base.AddIngredient(ingredient);
        //spawn ingredient in the world
        ingredient.Reparent(this);
        ingredient.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
        currStep = ingredient.GetCurrProgress;
        maxStep = ingredientAdded.GetMaxSteps;
        if (maxStep > 0)
            progressBar.Value = currStep / maxStep;
    }

    public override void ProcessIngredient() {
        if (maxStep <= 0) {
            GD.PrintS("Cannot chop", ingredientAdded.Name, "because max step is", maxStep);
            return;
        }
        currStep++;
        ingredientAdded.IncreaseCurrProgress(1);
        progressBar.Value = currStep / maxStep;
        //if done, spawn choppable scene
        if (currStep >= maxStep && ingredientAdded.GetChoppedScene != null) {
            Ingredient chopped = (Ingredient)ingredientAdded.GetChoppedScene.Instantiate();
            ingredientAdded.QueueFree();
            ingredientAdded = chopped;
            AddChild(chopped);
            chopped.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
            currStep = 0;
            maxStep = chopped.GetMaxSteps;
            return;
        }
    }

    public override Ingredient RemoveIngredient() {
        progressBar.Value = 0;
        progressUI.Visible = false;
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
