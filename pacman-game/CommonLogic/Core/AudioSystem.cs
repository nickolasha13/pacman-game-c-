using SFML.Audio;

namespace CommonLogic.Core;

public class AudioSystem
{
    private readonly Dictionary<string, Sound> _sounds = new();
    public bool AudioEnabled = true;

    public AudioSystem()
    {
        var files = Directory.GetFiles("Assets/Audio");
        foreach (var file in files)
        {
            var name = Path.GetFileNameWithoutExtension(file);
            var soundBuffer = new SoundBuffer(file);
            var sound = new Sound(soundBuffer);
            _sounds[name] = sound;
        }
    }

    public void Play(string name)
    {
        if (AudioEnabled)
            _sounds[name].Play();
    }
}