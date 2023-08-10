using Godot;

namespace WaifuSurvivors;

public partial class LevelContainer : Node2D {
	/**
	 * @TODO Think about using Qicktionary cache and also apply for mob spawn.
	 */
	public T Append<T>(PackedScene prefab, Vector2 globalPosition) where T : Node2D {
		var thing = prefab.Instantiate<T>();
		thing.GlobalPosition = globalPosition;

		AddChild(thing);

		return thing;
	}
}