using Godot;

namespace WaifuSurvivors;

/**
 * "Scriptable object" like:
 * @link https://ezcha.net/news/3-1-23-custom-resources-are-op-in-godot-4
 */
public partial class ActorMob : ActorBase {
	[Signal]
	public delegate void OnDeathEventHandler(ActorMob mob);

	[Export]
	public float _hits = 2;

	[Export]
	public float _armor = 1;

	[Export]
	public DamageTicker _damageTicker;

	public ActorPlayer _target;

	public ActorPlayer Target {
		set => _target = value;
	}

	public override void _PhysicsProcess(double delta) {
		var distance = _target.Position - Position;
		var targetVelocity = distance.Normalized() * _speed;

		_movement.PhysicsMove(targetVelocity, Friction, delta);
		_UpdateDisplayDirection();
	}

	public void ApplyDamage(float attack, out bool isDead) {
		var damage = Mathf.Max(0.5f, attack - _armor);

		_hits -= damage;

		ControllerDamageNumbers.instance.Create(damage, GlobalPosition);

		isDead = _hits <= 0f;
		if (!isDead) {
			return;
		}

		ControllerExperience.instance.CreateExperience(this);

		EmitSignal(SignalName.OnDeath, this);
	}

	public void OnTouchPlayer(ActorPlayer actorPlayer) {
		_damageTicker.Begin(actorPlayer);
	}

	public void OnUntouchPlayer() {
		_damageTicker.End();
	}
}
