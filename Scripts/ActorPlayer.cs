using Godot;

namespace WaifuSurvivors.Scripts;

public partial class ActorPlayer : CharacterBody2D {
	public const float Speed = 64.0f;
	public const float Friction = 20;

	[Export]
	public Node2D _actorDisplay;

	[Export]
	public AnimatedSprite2D _animatedSprite;

	public Vector2 InputDirection => Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

	public readonly Movement _movement;

	public ActorPlayer() {
		_movement = new Movement(this);
	}

	public override void _PhysicsProcess(double delta) {
		var targetVelocity = InputDirection * Speed;

		var isStopping = _movement.PhysicsMove(targetVelocity, Friction, delta);
		if (isStopping) {
			_StopAnimation();
		}
		else {
			_UpdateAnimation(Velocity);
		}
	}

	public void _StopAnimation() {
		_animatedSprite.Animation = "Idle";
	}

	public void _UpdateAnimation(Vector2 velocity) {
		_animatedSprite.Animation = "Walk";

		if (velocity.X == 0f) {
			return;
		}

		var scale = _actorDisplay.Scale;
		scale.X = -Mathf.Sign(velocity.X);

		_actorDisplay.Scale = scale;
	}
}
