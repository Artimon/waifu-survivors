using Godot;

namespace WaifuSurvivors;

public abstract partial class ActorBase : RigidBody2D {
	public const float Friction = 20f;

	[Export]
	public Node2D _actorDisplay;

	[Export]
	public float _speed;

	public readonly Movement _movement = new ();

	public override void _IntegrateForces(PhysicsDirectBodyState2D state) {
		state.LinearVelocity = _movement.Velocity;
	}

	public void _UpdateDisplayDirection() {
		var horizontalVelocity = _movement.Velocity.X;
		if (horizontalVelocity == 0f) {
			return;
		}

		var scale = _actorDisplay.Scale;
		scale.X = -Mathf.Sign(horizontalVelocity);

		_actorDisplay.Scale = scale;
	}
}