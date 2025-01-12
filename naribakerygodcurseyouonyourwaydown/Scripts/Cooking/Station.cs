using Godot;
using System;

public partial class Station : StaticBody3D, IInteractable {

    public void OnInteract(Node3D body) {
        GD.PrintS(body, "interacting with", Name);
    }
}
