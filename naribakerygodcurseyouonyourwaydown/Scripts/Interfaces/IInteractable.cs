using Godot;
using System;

public interface IInteractable {
    public void OnInteract(Node3D body);
    public void OnAltInteract(Node3D body);
    public void SetMaterial(Material mat);
    public Material GetMaterial();
}
