using Godot;

namespace WaifuSurvivors.Scripts;

/**
 * "Scriptable object" like:
 * @link https://ezcha.net/news/3-1-23-custom-resources-are-op-in-godot-4
 */
public partial class ActorMob : RigidBody2D {
	public const float Speed = 24.0f;
	public const float Friction = 20;

	[Signal]
	public delegate void OnDeathEventHandler(ActorMob mob);

	[Export]
	public float _hits = 2;

	[Export]
	public float _armor = 1;

	public ActorPlayer _target;

	public readonly Movement _movement;

	public ActorPlayer Target {
		set => _target = value;
	}

	public ActorMob() {
		_movement = new Movement(this);
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state) {
		state.LinearVelocity = _movement.Velocity;
	}

	public override void _PhysicsProcess(double delta) {
		var distance = _target.Position - Position;
		var targetVelocity = distance.Normalized() * Speed;

		_movement.PhysicsMove(targetVelocity, Friction, delta);
	}

	public void ApplyDamage(float attack) {
		var damage = attack - _armor;

		_hits -= damage;
		if (_hits > 0f) {
			return;
		}

		EmitSignal(SignalName.OnDeath, this);
	}
}
