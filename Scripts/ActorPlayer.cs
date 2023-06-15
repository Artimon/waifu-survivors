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

	public override void _PhysicsProcess(double delta) {
		var velocity = Velocity;

		var targetVelocity = InputDirection * Speed;
		var velocityChange = new Vector2 {
			X = (targetVelocity.X - velocity.X) * Friction * (float)delta,
			Y = (targetVelocity.Y - velocity.Y) * Friction * (float)delta
		};

		velocity += velocityChange;

		var isZeroX = Mathf.IsEqualApprox(velocity.X, 0f, 0.01f);
		if (isZeroX) {
			velocity.X = 0f;
		}

		var isZeroY = Mathf.IsEqualApprox(velocity.Y, 0, 0.01f);
		if (isZeroY) {
			velocity.Y = 0f;
		}

		var isStanding = velocity.Equals(Vector2.Zero);
		if (isStanding) {
			velocity = Vector2.Zero;
		}

		var isStopping = targetVelocity.Equals(Vector2.Zero);
		if (isStopping) {
			_StopAnimation();
		}
		else {
			_UpdateAnimation(velocity);
		}

		Velocity = velocity;

		MoveAndSlide();
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
