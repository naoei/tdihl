using Godot;
using peko.Utils;

public class MusicTextLabel : RichTextLabel
{
    /// <summary>
    /// Displays the specified text.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <param name="fancy">Whether or not it should animate.</param>
    public void DisplayText(string text, float duration = 1.5f, float fadeIn = 0.2f, float delay = 0.2f)
    {
        SetPhysicsProcess(false);

        BbcodeEnabled = true;

        GetTree().CreateTimer(0.8f);

        timer = new Timer
        {
            Text = text,
            Delay = delay + fadeIn,
            FadeIn = fadeIn,  
            Count = duration,
            Duration = duration
        };
        
        SetPhysicsProcess(true);
    }

    private Timer timer;
    
    private class Timer
    {
        public string Text { get; set; }
        public float Delay { get; set; }
        public float FadeIn { get; set; }
        public float Count { get; set; }
        public float Duration { get; set; }
    }

    public override void _PhysicsProcess(float delta)
    {
        var isActive = false;

        var color = new Color(1, 1, 1, 0);
        var offset = 0f;

        if (timer.Delay > 0.0f)
        {
            isActive = true;
            timer.Delay -= delta;
            color.a = Interpolation.ValueAt(timer.Delay, 1.0f, 0.0f, 0, timer.FadeIn);
        }
        else if (timer.Count > 0.0f)
        {
            isActive = true;
            timer.Count -= delta;
            color.a = Interpolation.ValueAt(timer.Count, 0.0f, 1.0f, 0, timer.Duration, Easing.OutQuint);
            offset = Interpolation.ValueAt(timer.Count, -15, 0, 0, timer.Duration, Easing.OutQuint);
        }
        
        BbcodeText = $"[color=#{color.ToHtml()}][offset x={offset} y=0]{timer.Text}[/offset][/color]";

        SetPhysicsProcess(isActive);
    }
}
