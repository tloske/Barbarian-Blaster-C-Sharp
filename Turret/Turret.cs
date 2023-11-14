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
	private Area3D Target { get; set; }
	private List<Area3D> Enemies { get; set; } = new List<Area3D>();

	public override void _Ready()
	{
		base._Ready();
		GetNode<Timer>("Timer").WaitTime = FireRate;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (Target == null && Enemies.Count > 0)
		{
			Target = Enemies.FirstOrDefault();
		}
		if (Target != null)
		{
			LookAt(Target.GlobalPosition, Vector3.Up);
            Vector3 rotation = Rotation;
			rotation.X = 0;
			rotation.Z = 0;
			Rotation = rotation;
		}
	}

	public void Fire()
	{
		if (Target == null) return;
		Projectile projectile = ProjectileScene.Instantiate<Projectile>();
		projectile.Direction = -GlobalTransform.Basis.Z;
		AddChild(projectile);
		projectile.GlobalPosition = ProjectileSpawnPosition.GlobalPosition;
	}

	public void OnAreaEntered(Area3D area)
	{
		if (area.IsInGroup("enemy"))
		{
			Enemies.Add(area);
		}
	}
	public void OnAreaExited(Area3D area)
	{
		if (area.IsInGroup("enemy"))
		{
			if (Target == area) { Target = null; }
			Enemies.Remove(area);
		}
	}
}
