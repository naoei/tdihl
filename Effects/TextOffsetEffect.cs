using Godot;

public class TextOffsetEffect : RichTextEffect
{
    public string bbcode = "peko";
    
    public override bool _ProcessCustomFx(CharFXTransform charFx)
    {
        var x = (float) charFx.Env["x"];
        var y = (float) charFx.Env["y"];

        charFx.Offset = new Vector2(x, y);

        return true;
    }
}