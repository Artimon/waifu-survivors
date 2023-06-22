using Godot;

namespace WaifuSurvivors.Scripts;

public partial class WeaponWhip : Node2D {
	[Export]
	public AnimatedSprite2D _animation;

	[Export]
	public AttackArea _attackArea;

	public double _timer;
	public double _cooldown = 1.8d;

	public override void _Ready() {
		_timer = _cooldown;
	}

	public override void _Process(double delta) {
		_timer -= delta;
		if (_timer > 0d) {
			return;
		}

		_timer += _cooldown;

		_animation.Play();

		var mobs = _attackArea.GetMobs();

		GD.Print($"Mobs detected: {mobs.Length}");

		foreach (var actorMob in mobs) {
			actorMob.ApplyDamage(2f);
		}
	}
}
