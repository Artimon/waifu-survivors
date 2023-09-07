using Godot;

namespace WaifuSurvivors;

public partial class PixelPerfectNumber : Node2D {
	[Export]
	public Control _control1;

	[Export]
	public Control _control2;

	[Export]
	public Control _control3;

	[Export]
	public AnimatedSprite2D _sprite1;

	[Export]
	public AnimatedSprite2D _sprite2;

	[Export]
	public AnimatedSprite2D _sprite3;

	public Control[] _controls;
	public AnimatedSprite2D[] _sprites;

	public override void _Ready() {
		_controls = new[] { _control1, _control2, _control3 };
		_sprites = new[] { _sprite1, _sprite2, _sprite3 };
	}

	public void SetNumber(int number) {
		foreach (var control in _controls) {
			control.Visible = false;
		}

		var digits = number.ToString();
		var index = 0;

		foreach (var digit in digits) {
			var numberIndex = (int)char.GetNumericValue(digit);

			_controls[index].Visible = true;
			_sprites[index].Frame = numberIndex;

			index += 1;
		}
	}
}
