using Godot;

namespace peko.Resources
{
    public class Shape : Resource
    {
        [Export]
        public Vector2[] Polygon { get; set; }
        [Export]
        public Color BaseColor { get; set; }
        [Export]
        public Color OutlineColor { get; set; }
    }
}