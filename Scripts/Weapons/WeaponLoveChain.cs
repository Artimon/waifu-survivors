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

	public override void _Ready() {
		_weaponTimer.WaitTime = 1.8d;
		_weaponTimer.Timeout += _OnTimeout;
		_weaponTimer.Start();
	}

	public void _OnTimeout() {
		var loveChainSlash = LevelContainer.Instance.Append<WeaponLoveChainSlash>(_weaponLoveChainSlashPrefab, GlobalPosition);

		var scale = loveChainSlash.Scale;
		scale.X = _actor.scaleX;

		loveChainSlash.Scale = scale;
	}
}