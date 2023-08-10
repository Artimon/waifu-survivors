using Godot;

namespace WaifuSurvivors;

public partial class DamageTicker : Timer {
	public ActorPlayer _actorPlayer;

	[Export]
	public ActorMob _actorMob;

	public override void _Ready() {
		WaitTime = 1d;
		Timeout += _OnTimeout;
	}

	public void Begin(ActorPlayer actorPlayer) {
		_actorPlayer = actorPlayer;
		_actorPlayer.ApplyDamage(1f);

		Start();
	}

	public void _OnTimeout() {
		_actorPlayer.ApplyDamage(1f);
	}

	public void End() {
		_actorPlayer = null;

		Stop();
	}
}