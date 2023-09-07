using Godot;

namespace WaifuSurvivors.Extensions;

public static class NodeExtension {
	public static void Remove(this Node2D node) {
		node.GetParent().RemoveChild(node);
		node.QueueFree();
	}
}