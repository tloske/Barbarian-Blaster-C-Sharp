using Godot;
using System;

public partial class DifficultyManager : Node
{
    [Signal] public delegate void StopSpawningEnemiesEventHandler();
    [Export] public float GameLength { get; set; } = 180.0f;
    [Export] public Curve SpawnTimeCurve { get; set; }
    [Export] public Curve EnemyHealthCurve { get; set; }
    private Timer Timer { get; set; }

    public override void _Ready()
    {
        Timer = GetNode<Timer>("Timer");
        Timer.Start(GameLength);
    }

    private float GameProgressRatio() => 1.0f - (float)Timer.TimeLeft / GameLength;

    public float GetSpawnTime() => SpawnTimeCurve.Sample(GameProgressRatio());

    public float GetEnemyHealth() => EnemyHealthCurve.Sample(GameProgressRatio());

    public void OnTimerTimeout() => EmitSignal(SignalName.StopSpawningEnemies);
}
