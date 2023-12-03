using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Turret : Node3D
{
	[Export] private PackedScene ProjectileScene { get; set; }
	[Export] private float FireRate { get; set; } = 0.5f;
	[Export] private Node3D ProjectileSpawnPosition { get; set; }
	public Path3D EnemyPath { get; set; }
	private Enemy Target { get; set; }
	private List<Area3D> Enemies { get; set; } = new List<Area3D>();
	[Export] private float TurretRange { get; set; } = 10.0f;
	public AnimationPlayer AnimationPlayer { get; set; }

	public override void _Ready()
	{
		base._Ready();
		GetNode<Timer>("Timer").WaitTime = FireRate;
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		Target = GetNewTarget();

		if (Target != null)
		{
			LookAt(Target.GlobalPosition, Vector3.Up, true);
			Vector3 rotation = Rotation;
			rotation.X = 0;
			rotation.Z = 0;
			Rotation = rotation;
		}
	}

	public void Fire()
	{
		if (Target != null)
		{
			if (AnimationPlayer.IsPlaying()) { AnimationPlayer.Stop(); }
			AnimationPlayer.Play("Fire");
			Projectile projectile = ProjectileScene.Instantiate<Projectile>();
			projectile.Direction = GlobalTransform.Basis.Z;
			AddChild(projectile);
			projectile.GlobalPosition = ProjectileSpawnPosition.GlobalPosition;
		}
	}

	public Enemy GetNewTarget()
	{
		Enemy bestTarget = null;
		foreach (Enemy enemy in GetTree().GetNodesInGroup("enemy"))
		{
			if (Position.DistanceTo(enemy.Position) < TurretRange)
			{
				if (bestTarget == null || enemy.Progress > bestTarget.Progress)
				{
					bestTarget = enemy;
				}
			}
		}
		return bestTarget;
	}
}
