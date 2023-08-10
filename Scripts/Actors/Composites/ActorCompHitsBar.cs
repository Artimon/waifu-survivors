using Godot;

namespace WaifuSurvivors;

public partial class ActorCompHitsBar : TextureProgressBar {
	public void SetHits(float hits, float maxHits) {
		MaxValue = maxHits;
		Value = hits;
	}
}