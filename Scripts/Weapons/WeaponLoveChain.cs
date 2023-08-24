using Godot;

namespace WaifuSurvivors;

public partial class WeaponLoveChain : Node2D {
	[Export]
	public ActorPlayer _actor; // @TODO Use Setup(...) in interface for all weapons.

	[Export]
	public Timer _weaponTimer;

	[Export]
	public Timer _slashTimer;

	[Export]
	public PackedScene _weaponLoveChainSlashPrefab;

	public float _slashMirror;
	public int _slashCount;

	public override void _Ready() {
		_weaponTimer.WaitTime = 1.8d;
		_weaponTimer.Timeout += _OnTimeout;
		_weaponTimer.Start();

		_slashTimer.WaitTime = 0.15d;
		_slashTimer.Timeout += _CreateSlash;
	}

	public void _OnTimeout() {
		_slashMirror = 1f;
		_slashCount = 3;
		_slashTimer.Start();
	}

	public void _CreateSlash() {
		var loveChainSlash = LevelContainer.Instance.Append<WeaponLoveChainSlash>(_weaponLoveChainSlashPrefab, GlobalPosition);

		var scale = loveChainSlash.Scale;
		scale.X = _actor.scaleX * _slashMirror; // * aoeScale
		// scale.Y = aoeScale;

		loveChainSlash.Scale = scale;

		_slashMirror *= -1f;
		_slashCount -= 1;

		if (_slashCount <= 0) {
			_slashTimer.Stop();
		}
	}
}