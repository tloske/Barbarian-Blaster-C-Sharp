using Godot;
using System;

public partial class TurretManager : Node
{
    [Export] public PackedScene TurretScene { get; set; }

    public void BuildTurret(Vector3 position)
    {
        var new_turret = TurretScene.Instantiate<Node3D>();
        new_turret.GlobalPosition = position;
        AddChild(new_turret);
    }
}
