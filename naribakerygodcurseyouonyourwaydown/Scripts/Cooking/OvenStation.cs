using Godot;
using System;

public partial class OvenStation : PrepStation {

    public override void AddIngredient(Ingredient ingredient) {
        //if first ingredient is butter, increase score by 50
        base.AddIngredient(ingredient);
    }

    public override void _Process(double delta) {
        if (HasIngredient()) {
            currStep += (float)delta;
            progressBar.Value = currStep / maxStep;

            ingAdded.IncreaseCurrProgress((float)delta);
        }
    }
    public override void ProcessIngredient() {
        if (currStep > maxStep) {
            SpawnNewIngredient(ingAdded.GetChoppedScene);
            ResetProgressUI();
            return;
        }
        GD.Print(currStep);
        currStep -= 1f;
        GD.Print(currStep);
        ingAdded.IncreaseCurrProgress(-1f);
        progressBar.Value = currStep / maxStep;
    }

    protected override int GetMaxSteps() {
        return GD.RandRange(50, 120);
    }

    protected override bool CanAcceptIngredient(Player player) {
        return base.CanAcceptIngredient(player) && player.GetItem().GetCanGoOnTheStove;
    }
}
