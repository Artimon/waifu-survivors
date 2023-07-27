using System.Collections.Generic;
using Godot;

namespace WaifuSurvivors.Scripts;

public partial class ControllerMobSpawn : Node2D {
	public const double SpawnCheckDelay = 0.1d;
	public const int MaxMobCount = 7;

	public ActorMob _actorMobBlueprint;

	public readonly Queue<ActorMob> _mobs = new();

	[Export]
	public Node2D _actorContainer;

	[Export]
	public ActorPlayer _actorPlayer;

	[Export]
	public PackedScene _actorMobPrefab;

	[Export]
	public float _spawnDistance = 15f * 16f;

	[Export]
	public float _spawnSize = 4f * 16f;

	public double _timer;

	public int _mobCount;

	public float RandomSpawnRange => GD.Randf() * 2f * _spawnSize - _spawnSize;

	public bool CanSpawnMob => _mobCount < MaxMobCount;

	public Vector2 PlayerGlobalPosition => _actorPlayer.GlobalPosition;

	public override void _Process(double delta) {
		_timer -= delta;
		if (_timer > 0d) {
			return;
		}

		_timer += SpawnCheckDelay;

		_SpawnMobs();
	}

	public void _SpawnMobs() {
		if (!CanSpawnMob) {
			return;
		}

		var angle = Mathf.Tau * GD.Randf();
		var spawnCenter = PlayerGlobalPosition + Vector2.FromAngle(angle) * _spawnDistance;

		// GD.Print($"{PlayerGlobalPosition} => {spawnCenter}");

		while (CanSpawnMob) {
			var wasCached = TryAcquireMob(out var mob);
			if (!wasCached) {
				mob.OnDeath += (deadMob) => {
					_mobCount -= 1;

					_actorContainer.RemoveChild(deadMob);
					_mobs.Enqueue(mob);
				};
			}

			// GetTree().Root.AddChild(mob);
			_actorContainer.AddChild(mob);

			mob.GlobalPosition = spawnCenter + new Vector2(RandomSpawnRange, RandomSpawnRange);
			mob.Target = _actorPlayer;

			_mobCount += 1;
		}
	}

	public bool TryAcquireMob(out ActorMob mob) {
		var success = _mobs.TryDequeue(out mob);
		if (success) {
			mob._hits = _actorMobBlueprint._hits;

			return true;
		}

		GD.Print("Instantiating Mob!");

		mob = _actorMobPrefab.Instantiate<ActorMob>();

		return false;
	}
}