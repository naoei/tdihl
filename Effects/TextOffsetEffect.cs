using Godot;

public class TextOffsetEffect : RichTextEffect
{
    [Export]
    public string bbcode = "offset";
    
    public override bool _ProcessCustomFx(CharFXTransform charFx)
    {
        var x = (float) charFx.Env["x"];
        var y = (float) charFx.Env["y"];

        charFx.Offset = new Vector2(x, y);

        return true;
    }
}