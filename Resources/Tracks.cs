using Godot;

namespace peko.Resources
{
    public class Tracks : Resource
    {
        [Export]
        public string Title { get; set; }
        [Export]
        public AudioStreamMP3 Stream { get; set; }

        public int TimesPlayed { get; set; }
    }
}