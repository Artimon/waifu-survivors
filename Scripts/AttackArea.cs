using System.Linq;
using Godot;

namespace WaifuSurvivors;

public partial class AttackArea : Area2D {
	public override void _Ready() {
		SetProcess(false);
	}

	public ActorMob[] GetMobs() {
		SetProcess(true);

		var mobs = GetOverlappingBodies()
			.OfType<ActorMob>()
			.ToArray();

		SetProcess(false);

		return mobs;
	}
}