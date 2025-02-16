using Godot;
using System;

public partial class Cookbook : Node3D, IInteractable {

    [Export] private MeshInstance3D mesh;
    [Export] private Control cookbookUI;

    public void OnInteract(Node3D body) {
        if (body is not Player) return;
        if (cookbookUI.Visible) {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        } else {
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }

        cookbookUI.Visible = !cookbookUI.Visible;
    }
    public void OnAltInteract(Node3D body) { }

    public void SetMaterial(Material mat) {
        mesh.SetSurfaceOverrideMaterial(0, mat);
    }

    public Material GetMaterial() {
        return mesh.GetActiveMaterial(0);
    }
}
