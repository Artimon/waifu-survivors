using System.Linq;
using Godot;
using WaifuSurvivors.Scripts;

public partial class ActorMobSpawner : Node2D {
	public const double SpawnCheckDelay = 0.1d;
	public const int MaxMobCount = 7;

	public ActorPlayer _player;

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
			var mob = _actorMobPrefab.Instantiate<ActorMob>();
			mob.OnDeath += () => {
				_mobCount -= 1;
			};

			// GetTree().Root.AddChild(mob);
			_actorMobContainer.AddChild(mob);

			mob.GlobalPosition = spawnCenter + new Vector2(RandomSpawnRange, RandomSpawnRange);
			mob.Target = _player;

			_mobCount += 1;

			GD.Print("Mob spawned!");
		}
	}
}