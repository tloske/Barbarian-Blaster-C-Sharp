using Godot;
using System;

public partial class EnemyPath : Path3D
{
	[Export] public PackedScene EnemyScene { get; set; }
	[Export] public DifficultyManager DifficultyManager { get; set; }
	[Export] public VictoryLayer VictoryLayer { get; set; }
	public Timer SpawnTimer { get; set; }

	public override void _Ready() => SpawnTimer = GetNode<Timer>("SpawnTimer");

	public void SpawnEnemy()
	{
		Enemy newEnemy = EnemyScene.Instantiate<Enemy>();
		newEnemy.MaxHealth = DifficultyManager.GetEnemyHealth();
		AddChild(newEnemy);
		SpawnTimer.WaitTime = DifficultyManager.GetSpawnTime();
		newEnemy.TreeExited += EnemyDefeated;
	}

	public void EnemyDefeated()
	{
		if (SpawnTimer.IsStopped())
		{
			foreach (var child in GetChildren())
			{
				if (child is Enemy)
				{
					return;
				}
			}
			VictoryLayer.Victory();
		}
	}

	public void OnDifficultyManagerStopSpawningEnemies() => SpawnTimer.Stop();
}
