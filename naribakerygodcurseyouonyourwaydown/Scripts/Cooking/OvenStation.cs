using Godot;
using System;

public partial class OvenStation : MixStation {

    public override void AddIngredient(Ingredient ingredient) {
        //if first ingredient is butter, increase score by 50
        base.AddIngredient(ingredient);
    }

    public override void _Process(double delta) {
        if (HasIngredient()) {
            currStep += (float)delta;
            progressBar.Value = currStep / maxStep;
            ingAdded.IncreaseCurrProgress((float)delta);
            FinishIngredient();
        }
    }

    public override void ProcessIngredient() {
        base.ProcessIngredient();

        currStep += 10f;

        ingAdded.IncreaseCurrProgress(10f);
        progressBar.Value = currStep / maxStep;
        FinishIngredient();
    }

    protected override bool CanSpawnDish(RecipieR recipie) {
        return !recipie.IsMixRecipie;
    }

    protected override int GetMaxSteps() {
        return GD.RandRange(50, 120);
    }

}
