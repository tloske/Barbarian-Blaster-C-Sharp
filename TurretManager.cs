using Godot;
using System;

public partial class TurretManager : Node
{
    [Export] public PackedScene TurretScene { get; set; }
    [Export] public Path3D EnemyPath { get; set; }

    public void BuildTurret(Vector3 position)
    {
        Turret newTurret = TurretScene.Instantiate<Turret>();
        newTurret.EnemyPath = EnemyPath;
        AddChild(newTurret);
        newTurret.GlobalPosition = position;
    }
}
