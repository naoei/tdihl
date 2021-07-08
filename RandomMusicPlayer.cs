using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using peko.Resources;

public class RandomMusicPlayer : AudioStreamPlayer
{
    [Export]
    public List<Tracks> Tracks;
    [Export]
    public NodePath TextLabel;

    private int randomOffset;
    
    public override void _Ready()
    {
        PlayRandomSong();
    }

    public void PlayRandomSong()
    {
        var song = findSong();
        song.Stream.Loop = true;
        Stream = song.Stream;
        
        Play();

        var node = GetNode<MusicTextLabel>(TextLabel);
        node.DisplayText("Now Playing: " + song.Title, 2);
    }

    private Tracks findSong()
    {
        var rng = new Random();
        var tracks = Tracks.Where(t => t.TimesPlayed < randomOffset);
        var array = tracks as Tracks[] ?? tracks.ToArray();

        if (array.Any())
        {
            var song = array[rng.Next(0, array.Length)];
            song.TimesPlayed++;

            return song;
        }
        else
        {
            var song = Tracks[rng.Next(0, Tracks.Count)];
            randomOffset++;
            song.TimesPlayed++;

            return song;
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("music_random"))
            PlayRandomSong();
    }
}