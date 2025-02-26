using Godot;

public abstract partial class PrepStation : CounterStation {
    [Export] protected PackedScene badDish;

    [ExportGroup("Progress Bar")]
    [Export] protected Node3D progressUI;
    [Export] protected ProgressBar progressBar;

    protected float currStep = 0;
    protected bool hasNewIngredientSpawned = false;


    public override void _Ready() {
        progressUI.Visible = false;
    }

    public override void AddIngredient(Ingredient ingredient) {
        base.AddIngredient(ingredient);
        ResetProgressUI(true, ingredient.GetCurrProgress);
    }

    public override void ProcessIngredient() {
        if (maxStep <= 0) {
            GD.PrintS("Cannot process", ingAdded.Name, "because max step is", maxStep);
            return;
        }

        currStep++;
        ingAdded.IncreaseCurrProgress(1);
        progressBar.Value = currStep / maxStep;
    }

    public override Ingredient RemoveIngredient() {
        ResetProgressUI();
        return base.RemoveIngredient();
    }

    protected void ResetProgressUI(bool isOn = false, float step = 0) {
        currStep = step;
        if (step == 0) {
            maxStep = GetMaxSteps();
            progressBar.Value = step / maxStep;
        }
        progressUI.Visible = isOn;
    }


    protected Ingredient SpawnNewIngredient(PackedScene ingToSpawn) {
        Ingredient newIng = (Ingredient)ingToSpawn.Instantiate();
        ingAdded.QueueFree();
        ingAdded = newIng;
        AddChild(newIng);
        newIng.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
        return newIng;
    }

    protected virtual bool CanAcceptIngredient(Player player) {
        return !player.AreHandsEmpty() && !HasIngredient() && player.GetItem() is Ingredient;
    }

    protected virtual bool CanRemoveIngredient(Player player) {
        return player.AreHandsEmpty() && HasIngredient();
    }

    public override void OnInteract(Node3D body) {
        if (body is Player player) {
            if (CanRemoveIngredient(player))
                player.PickUp(RemoveIngredient());
            else if (CanAcceptIngredient(player))
                AddIngredient((Ingredient)player.PutDown(false));
        }
    }

    public override void OnAltInteract(Node3D body) {
        if (HasIngredient())
            ProcessIngredient();
        base.OnAltInteract(body);
    }
}
