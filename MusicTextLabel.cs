using System;
using System.Collections.Generic;
using Godot;
using peko.Utils;

public class MusicTextLabel : RichTextLabel
{
    [Export]
    public string Message { get; set; }

    [Export]
    public float MessageDelay { get; set; }

    [Export]
    public float CascadingDelay { get; set; }

    [Export]
    public float StartDelay { get; set; }

    public override async void _Ready()
    {
        SetPhysicsProcess(false);

        BbcodeEnabled = true;
        BbcodeText = Message;
        
        InstallEffect(new TextOffsetEffect());

        await ToSignal(GetTree().CreateTimer(StartDelay), "timeout");

        FadeText(Message, MessageDelay, CascadingDelay);
    }

    private readonly List<Timer> charTimers = new List<Timer>();

    public void FadeText(string text, float duration, float cascadingDelay)
    {
        var i = 0;

        foreach (var c in text)
        {
            charTimers.Add(new Timer
            {
                Letter = c,
                Delay = cascadingDelay * i,
                Count = duration,
                Duration = duration
            });

            i++;
        }

        SetPhysicsProcess(true);
    }

    private class Timer
    {
        public char Letter { get; set; }
        public float Delay { get; set; }
        public float Count { get; set; }
        public float Duration { get; set; }
    }

    public override void _PhysicsProcess(float delta)
    {
        var isActive = false;

        BbcodeText = string.Empty;

        foreach (var cTimer in charTimers)
        {
            var color = new Color(1, 1, 1, 0);
            var y = 0;

            if (cTimer.Delay > 0.0f)
            {
                isActive = true;
                cTimer.Delay -= delta;
                color.a = 1.0f;
                y = 0;
            }
            else if (cTimer.Count > 0.0f)
            {
                isActive = true;
                cTimer.Count -= delta;
                color.a = Math.Max(cTimer.Count / cTimer.Duration, 0.0f);
                y = Interpolation.ValueAt(Math.Max(cTimer.Count / cTimer.Duration, 0.0f), 20, 0, 0f, 1f, Easing.OutQuint);
            }

            BbcodeText += $"[color=#{color.ToHtml()}][peko x=0 y={y}]{cTimer.Letter}[/peko][/color]";
        }

        SetPhysicsProcess(isActive);
    }
}