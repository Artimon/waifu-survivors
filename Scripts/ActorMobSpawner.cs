using System.Collections.Generic;
using System.Linq;
using Godot;
using WaifuSurvivors.Scripts;

public partial class ActorMobSpawner : Node2D {
	public const double SpawnCheckDelay = 0.1d;
	public const int MaxMobCount = 7;

	public ActorPlayer _player;
	public ActorMob _actorMobBlueprint;

	public readonly Queue<ActorMob> _mobs = new();

	[Export]
	public Node2D _actorMobContainer;
	
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

	public override void _Ready() {
		_player = GetTree()
			.GetNodesInGroup("Player")
			.OfType<ActorPlayer>()
			.First();

		TryAcquireMob(out _actorMobBlueprint);
	}

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
		var spawnCenter = GlobalPosition + Vector2.FromAngle(angle) * _spawnDistance;

		while (CanSpawnMob) {
			var wasCached = TryAcquireMob(out var mob);
			if (!wasCached) {
				mob.OnDeath += (deadMob) => {
					_mobCount -= 1;

					_actorMobContainer.RemoveChild(deadMob);
					_mobs.Enqueue(mob);
				};
			}

			// GetTree().Root.AddChild(mob);
			_actorMobContainer.AddChild(mob);

			mob.GlobalPosition = spawnCenter + new Vector2(RandomSpawnRange, RandomSpawnRange);
			mob.Target = _player;

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