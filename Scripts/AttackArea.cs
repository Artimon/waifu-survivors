using System.Linq;
using Godot;

namespace WaifuSurvivors;

public partial class AttackArea : Area2D {
	public ActorMob[] GetMobs() {
		var mobs = GetOverlappingBodies()
			.OfType<ActorMob>()
			.ToArray();

		return mobs;
	}
}