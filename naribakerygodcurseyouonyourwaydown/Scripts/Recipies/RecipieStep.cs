using Godot;
using System;

public enum StepType { CHOP, MIX, HEAT }

[GlobalClass]

public partial class RecipieStep : Resource {
    [Export] private StepType stepType;
    [Export] private IngredientR ingredientR;
    [Export] private int stepDuration;

    public StepType GetStepType { get => stepType; }
    public IngredientR GetIngredientR { get => ingredientR; }
    public int GetStepDuration { get => stepDuration; }

    public override bool Equals(object obj) {
        if (obj is not RecipieStep) return false;
        RecipieStep other = (RecipieStep)obj;
        if (stepType != other.stepType) return false;
        if (!ingredientR.Equals(other.ingredientR)) return false;
        if (stepDuration != other.stepDuration) return false;
        return true;
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }
}
