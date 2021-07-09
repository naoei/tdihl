using System.Collections.Generic;
using System.Linq;
using ClipperLib;
using Godot;
using Shape = peko.Resources.Shape;

public class ShapeBuilder : Node2D
{
    [Export]
    public Shape Shape;
    [Export(PropertyHint.Range, "0.0,10.0,or_greater,or_lesser")]
    public double StrokeWidth = 3;
    
    public override void _Ready()
    {
        var @base = (Polygon2D) GetNode("base");
        var outline = (Polygon2D) GetNode("outline");

        @base.Polygon = outline.Polygon = Shape.Polygon;
        @base.Color = Shape.BaseColor;
        outline.Color = Shape.OutlineColor;

        // gay
        var points = Shape.Polygon.Select(point => new IntPoint((long) point.x, (long) point.y));

        var offset = new ClipperOffset();
        var newPoints = new List<List<IntPoint>>();

        offset.AddPath(points.ToList(), JoinType.jtSquare, EndType.etClosedPolygon);
        offset.Execute(ref newPoints, -StrokeWidth);

        if (StrokeWidth >= 0)
        {
            @base.Polygon = newPoints[0].Select(point => new Vector2(point.X, point.Y)).ToArray();
        }
        else
        {
            outline.Polygon = newPoints[0].Select(point => new Vector2(point.X, point.Y)).ToArray();
        }
    }
}