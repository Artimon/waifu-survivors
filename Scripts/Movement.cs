using Godot;

namespace WaifuSurvivors.Scripts;

public class Movement {
	public readonly CharacterBody2D _characterBody;

	public Movement(CharacterBody2D characterBody) {
		_characterBody = characterBody;
	}

	public bool PhysicsMove(Vector2 targetVelocity, float friction, double delta) {
		var velocity = _characterBody.Velocity;
		var velocityChange = new Vector2 {
			X = (targetVelocity.X - velocity.X) * friction * (float)delta,
			Y = (targetVelocity.Y - velocity.Y) * friction * (float)delta
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

		_characterBody.Velocity = velocity;
		_characterBody.MoveAndSlide();

		return targetVelocity.Equals(Vector2.Zero);
	}
}