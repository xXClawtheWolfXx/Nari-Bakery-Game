using Godot;
using System;

public partial class Item : Node3D {
    [Export] private bool canGoOnTheStove = false;

    public bool GetCanGoOnTheStove { get => canGoOnTheStove; }
}
