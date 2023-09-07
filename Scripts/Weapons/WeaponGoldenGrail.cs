using System.Collections.Generic;
using System.Linq;
using Godot;

namespace WaifuSurvivors;

public partial class WeaponGoldenGrail : Node2D {
	[Export]
	public Timer _tickTimer;

	[Export]
	public float _tickTime;

	public readonly Dictionary<ActorMob, ulong> _mobTicks = new();

	public ulong _millisecondsBetweenTicks;

	public override void _Ready() {
		_millisecondsBetweenTicks = (ulong)(_tickTime * 1000f);

		_tickTimer.WaitTime = 0.1d; // This is just the base tick to check for damage, the actual duration is longer.
		_tickTimer.Timeout += _OnTick;
		_tickTimer.Start();
	}

	public void _OnTick() {
		var microTime = Time.GetTicksMsec();
		var mobTicks = _mobTicks.ToArray();

		foreach (var (actor, tickTime) in mobTicks) {
			_TryDamage(actor, tickTime, microTime);
		}
	}

	public bool _TryDamage(ActorMob actor, ulong tickTime, ulong microTime) {
		if (tickTime > microTime) {
			return false;
		}

		var nextTickTime = tickTime + _millisecondsBetweenTicks;

		actor.ApplyDamage(5f, out var isDead);
		if (!isDead) {
			_mobTicks[actor] = nextTickTime;
		}


		return true;
	}

	public void _OnBodyEntered(Node otherBody) {
		if (otherBody is not ActorMob actor) {
			return;
		}

		ControllerDefer.Delay += () => {
			var microTime = Time.GetTicksMsec();

			// Automatically add the mob to the dictionary.
			_TryDamage(actor, microTime, microTime);
		};
	}

	public void _OnBodyExited(Node otherBody) {
		if (otherBody is not ActorMob actor) {
			return;
		}

		_mobTicks.Remove(actor);
	}
}