using UnityEngine.Audio;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ")]
[System.Serializable]
public class Sound
{
    [Header("SOUND")]
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private string _tag;

    [Header("VALUES")]
    [Range(0f, 1f)]
    [SerializeField] private float _volume = 1;
    [Range(.1f, 3f)]
    [SerializeField] private float _pitch = 0.5f;

    [Header("STATES")]
    [SerializeField] private bool _loop = false;

    [HideInInspector]
    [SerializeField] private AudioSource _source;

    /// <summary>
    /// Name assigned from this sound
    /// </summary>
    public string Name { get { return _name; } }
    /// <summary>
    /// Tag assigned from this sound
    /// </summary>
    public string Tag { get { return _tag; } }
    /// <summary>
    /// The audio of this sound
    /// </summary>
    public AudioClip Clip { get { return _clip; } }
    /// <summary>
    /// If is loop
    /// </summary>
    public bool Loop { get { return _loop; } set { _loop = value; } }
    /// <summary>
    /// Actual volume of the sound
    /// </summary>
    public float Volume { get { return _volume; } set { _volume = value; } }
    /// <summary>
    /// The degree of highness or lowness of a tone.
    /// </summary>
    public float Pitch { get { return _pitch; } set { _pitch = value; } }
    /// <summary>
    /// Object of this sound in scene
    /// </summary>
    public AudioSource Source { get { return _source; } }

    /// <summary>
    /// Set the AudioSource
    /// </summary>
    /// <param name="source">the source set</param>
    public void SetSource(AudioSource source) {
        if (source == null) { return; }

        _source = source;
    }

    public void UpdateName()
    {
        if (Clip != null && Name == "")
        {
            _name = Clip.name;
        }
    }

    public void UpdateSource()
    {
        if (Source != null)
        {
            Source.volume = _volume;
            Source.pitch = _pitch;
        }
    }

}