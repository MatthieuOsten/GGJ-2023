using System;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ")]
public class AudioManager : MonoBehaviour
{

    #region SINGLETON

    /// <summary>
    /// Force a avoir qu'un seul LevelManager
    /// </summary>
    [SerializeField] private static AudioManager _instance = null;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
                // Si vrai, l'instance va etre cree
                if (_instance == null)
                {
                    var newObjectInstance = new GameObject();
                    newObjectInstance.name = typeof(AudioManager).ToString();
                    _instance = newObjectInstance.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    /// <summary>
    /// Active debugLog of this script for debug
    /// </summary>
    [SerializeField] private bool _debugLog;

    /// <summary>
    /// List of sounds in audioManager
    /// </summary>
    [SerializeField] public Sound[] sounds = new Sound[0];

    void Awake()
    {
        Generate();
    }

    private void OnValidate()
    {
        foreach (var sound in sounds)
        {
            sound.UpdateName();
        }
    }

    private void Update()
    {
        foreach(var sound in sounds)
        {
            sound.UpdateSource();
        }
    }

    /// <summary>
    /// Get the 'Sound' of this 'AudioManager'
    /// </summary>
    /// <param name="name">'String' Name of the sound in array</param>
    /// <returns>Return the 'Sound'</returns>
    public Sound GetSound(string name)
    {
        if (name == null) { name = "NULL"; }
        if (sounds.Length == 0) { return null; }

        return Array.Find(sounds, sound => sound.Name == name);
    }

    /// <summary>
    /// Generate all audiosource for lanch sound
    /// </summary>
    public void Generate()
    {
        foreach (var component in gameObject.GetComponents<AudioSource>())
        {
            if (component == null) { continue; }

            #if UNITY_EDITOR
                DestroyImmediate(component);
            #else
                Destroy(component);
            #endif
        }

        foreach (Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    /// <summary>
    /// Get number of sound share the name
    /// </summary>
    /// <param name="name">Name shared of sounds</param>
    /// <returns>Return number of sounds with this name</returns>
    private int GetNbrSound(string name)
    {
        // If Name is null dont execute the method
        if (name == null) { name = "NULL"; }

        if (AudioManager.Instance != null)
        {
            int nbrIterationSound = 1;

            while (AudioManager.Instance.IsExist(name + nbrIterationSound))
            {
                nbrIterationSound++;
            }

            if (nbrIterationSound <= 1) { return 0; }
            else return nbrIterationSound;
        }
        else
        {
            return 0;
        }

    }

    /// <summary>
    /// Get random sound who shared the name
    /// </summary>
    /// <param name="name">Name shared in sound</param>
    /// <returns>Return name of the sound selected</returns>
    private string GetRandomSoundFromName(string name)
    {
        if (name == null) { name = "NULL"; }

        int nbrSound = GetNbrSound(name);

        if (nbrSound > 0)
        {
            return name + UnityEngine.Random.Range(1, nbrSound);
        }
        else
        {
            return name;
        }

    }

    /// <summary>
    /// Play the sound if is exist
    /// </summary>
    /// <param name="name">The name of sound seek</param>
    public void Play(string name)
    {
        Debug.Log("Play Function");

        if (name == null) { 
            name = "NULL";
            Debug.Log("Name is null");
        }

        if (sounds.Length == 0)
        {
            Debug.Log("Have noting sound");
            return;
        }

        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s != null)
        {
            if (_debugLog) { Debug.Log("Sound " + s.Name + " is played"); }
            s.Source.Play();
        }
        else
        {
            if (_debugLog) { Debug.Log("Sound " + name + " is not found"); }
        }

    }

    /// <summary>
    /// Play sound random on shared name
    /// </summary>
    /// <param name="name">The name shared with sound</param>
    public void PlayRandom(string name)
    {
        Play(GetRandomSoundFromName(name));
    }

    public void CheckTransitionBetweenTwoSound(float timeFirstSound, float timeSecondSound,string nameFirstSound, string nameSecondSound)
    {
        Sound firstSound = GetSound(nameFirstSound);
        Sound secondSound = GetSound(nameSecondSound);

        if (firstSound == null || secondSound == null) { return; }

        if (firstSound.Source.isPlaying)
        {
            if (firstSound.Source.time >= timeFirstSound && !secondSound.Source.isPlaying)
            {
                firstSound.Source.Stop();
                PlayToTime(nameSecondSound, timeSecondSound);
            }
        }


    }

    /// <summary>
    /// Play sound in a definie time
    /// </summary>
    /// <param name="name">name of the sound</param>
    /// <param name="time">time of start</param>
    public void PlayToTime(string name, float time)
    {
        Play(name);

        Sound actualSound = GetSound(name);

        if (actualSound == null) { return; }

        if (time < 0)
        {
            time = 0;
        }
        else if (time > actualSound.Clip.length)
        {
            time = actualSound.Clip.length;
        }

        GetSound(name).Source.time = time;

    }

    /// <summary>
    /// Verify if this 'Sound' exist in 'AudioManager'
    /// </summary>
    /// <param name="name">Name of the sound</param>
    /// <returns>Return true if exist in 'AudioManager' else return false</returns>
    public bool IsExist(string name)
    {
        if (name == null) { name = "NULL"; }
        if (sounds.Length == 0) { return false; }

        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s != null)
            return true;
        else
            return false;
    }
}

