using Godot;
using System;

public partial class EnemyPath : Path3D
{
	[Export] public PackedScene EnemyScene { get; set; }

	public void SpawnEnemy()
	{
		Enemy newEnemy = EnemyScene.Instantiate<Enemy>();
		AddChild(newEnemy);
	}

}
