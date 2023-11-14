using Godot;
using System;

public partial class Enemy : PathFollow3D
{
	[Export] public float MoveSpeed { get; set; } = 5f;
	public Base Base { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Base = GetTree().GetFirstNodeInGroup("base") as Base;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += (float)delta * MoveSpeed;
		if (ProgressRatio >= 1)
		{
			Base.TakeDamage();
			ProgressRatio = 0;
		}
	}
}
