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
        ingAdded = ingredient;
        itemAdded = ingredient;

        //spawn ingredient in the world
        ingAdded.Reparent(this);
        ingAdded.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
        currStep = ingAdded.GetCurrProgress;
        maxStep = ingAdded.GetMaxSteps();
        if (maxStep > 0)
            progressBar.Value = currStep / maxStep;

    }

    public override void ProcessIngredient() {
        if (maxStep <= 0) {
            GD.PrintS("Cannot chop", itemAdded.Name, "because max step is", maxStep);
            return;
        }
        currStep++;
        ingAdded.IncreaseCurrProgress(1);
        progressBar.Value = currStep / maxStep;
        //if done, spawn choppable scene
        if (currStep >= maxStep && ingAdded.GetChoppedScene != null) {
            Ingredient chopped = (Ingredient)ingAdded.GetChoppedScene.Instantiate();
            itemAdded.QueueFree();
            itemAdded = chopped;
            AddChild(chopped);
            chopped.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
            currStep = 0;
            maxStep = chopped.GetMaxSteps();
            return;
        }
    }

    public override Item RemoveIngredient() {
        progressBar.Value = 0;
        progressUI.Visible = false;
        ingAdded = null;
        return base.RemoveIngredient();
    }

    public override void OnInteract(Node3D body) {
        if (body is Player player) {
            if (player.AreHandsEmpty() && HasIngredient())
                player.PickUp(RemoveIngredient());
            else if (!player.AreHandsEmpty() && !HasIngredient() && player.GetItem() is Ingredient ing) {
                GD.PrintS(player.GetItem() is Ingredient, "ing");
                player.PutDown(false);
                AddIngredient(ing);
            }
        }

    }

    public override void OnAltInteract(Node3D body) {
        GD.PrintS(Name, "has ingredient", HasIngredient());
        if (HasIngredient())
            ProcessIngredient();
        base.OnAltInteract(body);
    }
}
