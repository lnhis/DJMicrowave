using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SoundSystem : MonoBehaviour
{
    public enum SoundTypes { WaveCharge, WaveShoot, WaveHit, WaveHit2 }

    public enum MusicTypes { Menu }

    public enum AmbientTypes { RainingInside, RainingOutside }
    
    public SoundData[] soundDatas;
    public MusicData[] musicDatas;
    public AmbientData[] ambientDatas;

    [Range(0.0f, 1.0f)]
    public float soundVolume = 1f;
    [Range(0.0f, 1.0f)]
    public float musicVolume = 1f;
    [Range(0.0f, 1.0f)]
    public float ambientVolume = 1f;

    private AudioSource Music { get; set; }
    private AudioSource Ambient { get; set; }

    private Dictionary<int, AudioSource> Sounds = new Dictionary<int, AudioSource>();

    private Transform musicContainer;
    private Transform soundContainer;
    private Transform ambientContainer;

    private float lastSoundVolume = -1f;
    private float lastMusicVolume = -1f;
    private float lastAmbientVolume = -1f;

    public bool MuteMusic = false;
    public bool MuteAmbient = false;

    public bool MuteAllSound = false;
    private bool lastMuteAllSound = false;

    void Awake()
    {
        musicContainer = this.transform.FindChild("MusicContainer");
        soundContainer = this.transform.FindChild("SoundContainer");
        ambientContainer = this.transform.FindChild("AmbientContainer");
    }

    /*
    public void PlayAmbient(string n)
    {
        AudioClip clip = GetTemplate(ambientTemplates, n);
        if (clip == null) return;

        if (Ambient != null)
        {
            GameObject.Destroy(Ambient.gameObject);
            Ambient = null;
        }

        GameObject prefab = new GameObject();
        prefab.name = "Ambient-" + n;
        AudioSource ass = prefab.AddComponent<AudioSource>();
        ass.clip = clip;
        ass.loop = true;
        ass.volume = ambientVolume;
        ass.mute = MuteAllSound;
        prefab.transform.SetParent(ambientContainer);
        prefab.transform.position = Vector2.zero;

        Ambient = ass;
        ass.Play();
    }
    */
    public void PlayMusic(MusicTypes mt)
    {
        AudioClip clip = GetQuickMusic(mt);
        if (clip == null) return;

        if(Music != null)
        {
            GameObject.Destroy(Music.gameObject);
            Music = null;
        }

        GameObject prefab = new GameObject();
        prefab.name = "Music-" + mt;
        AudioSource ass = prefab.AddComponent<AudioSource>();
        ass.clip = clip;
        ass.loop = true;
        ass.volume = musicVolume;
        ass.mute = MuteAllSound;
        prefab.transform.SetParent(musicContainer);
        prefab.transform.position = Vector2.zero;

        Music = ass;
        ass.Play();
    }
    public int PlaySound(SoundTypes st, Vector2? pos = null, bool muteMusic = false, float playDelay = -1f, float endDelay = -1f)
    {
        AudioClip clip = GetQuickSound(st);
        if (clip == null) return -1;

        GameObject prefab = new GameObject();
        
        prefab.name = "Sound-" + st;
        AudioSource ass = prefab.AddComponent<AudioSource>();
        ass.loop = false;
        ass.volume = soundVolume;
        ass.clip = clip;
        ass.mute = MuteAllSound;
        prefab.transform.SetParent(soundContainer);
        if (pos.HasValue)
            prefab.transform.position = pos.Value;
        else
            prefab.transform.position = Vector2.zero;

        SoundStopper ss = prefab.AddComponent<SoundStopper>();
        ss.MuteMusic = muteMusic;
        ss.SoundEndDuration = endDelay;
        ss.SoundStartDelay = playDelay;
        Sounds.Add(ass.GetInstanceID(), ass);
        return ass.GetInstanceID();
    }
    public void RemoveSound(int instanceId)
    {
        if(Sounds.ContainsKey(instanceId))
        {
            GameObject.Destroy(Sounds[instanceId].gameObject);
            Sounds.Remove(instanceId);
        }
    }
    private void UpdateVolume()
    {
        if (Music != null)
        {
            Music.volume = musicVolume;
            Music.mute = MuteAllSound;
        }

        if(Ambient != null)
        {
            Ambient.volume = ambientVolume;
            Ambient.mute = MuteAllSound;
        }

        foreach (AudioSource ass in Sounds.Values)
        {
            ass.volume = soundVolume;
            ass.mute = MuteAllSound;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(lastSoundVolume!=soundVolume || lastMusicVolume != musicVolume || lastAmbientVolume != ambientVolume || lastMuteAllSound != MuteAllSound)
        {
            lastSoundVolume = soundVolume;
            lastMusicVolume = musicVolume;
            lastAmbientVolume = ambientVolume;
            lastMuteAllSound = MuteAllSound;

            UpdateVolume();
        }
        
        if(!MuteAllSound && Music != null)
        {
            Music.mute = MuteMusic;
        }
        
	}
    private Dictionary<SoundTypes, SoundData> quickSound = null;
    public AudioClip GetQuickSound(SoundTypes st)
    {
        if (quickSound == null)
        {
            quickSound = new Dictionary<SoundTypes, SoundData>();
            foreach (SoundData sd in soundDatas)
            {
                quickSound.Add(sd.atype, sd);
            }
        }

        return quickSound[st].GetRandom();
    }
    private Dictionary<MusicTypes, MusicData> quickMusic = null;
    public AudioClip GetQuickMusic(MusicTypes st)
    {
        if (quickMusic == null)
        {
            quickMusic = new Dictionary<MusicTypes, MusicData>();
            foreach (MusicData sd in musicDatas)
            {
                quickMusic.Add(sd.atype, sd);
            }
        }

        return quickMusic[st].GetRandom();
    }
    private Dictionary<AmbientTypes, AmbientData> quickAmbient = null;
    public AudioClip GetQuickAmbient(AmbientTypes st)
    {
        if (quickAmbient == null)
        {
            quickAmbient = new Dictionary<AmbientTypes, AmbientData>();
            foreach (AmbientData sd in ambientDatas)
            {
                quickAmbient.Add(sd.atype, sd);
            }
        }

        return quickAmbient[st].GetRandom();
    }

    [Serializable]
    public class SoundData
    {
        public string name;
        public SoundTypes atype;
        public AudioClip[] audio;

        public void Refresh()
        {
            name = "" + atype;
        }
        public AudioClip GetRandom()
        {
            return audio[UnityEngine.Random.Range(0, audio.Length - 1)];
        }
    }

    [Serializable]
    public class MusicData
    {
        public string name;
        public MusicTypes atype;
        public AudioClip[] audio;

        public void Refresh()
        {
            name = "" + atype;
        }
        public AudioClip GetRandom()
        {
            return audio[UnityEngine.Random.Range(0, audio.Length - 1)];
        }
    }

    [Serializable]
    public class AmbientData
    {
        public string name;
        public AmbientTypes atype;
        public AudioClip[] audio;

        public void Refresh()
        {
            name = "" + atype;
        }
        public AudioClip GetRandom()
        {
            return audio[UnityEngine.Random.Range(0, audio.Length - 1)];
        }
    }
    public void Refresh()
    {
        foreach (SoundData ti in soundDatas) ti.Refresh();

        foreach (MusicData ti in musicDatas) ti.Refresh();

        foreach (AmbientData ti in ambientDatas) ti.Refresh();

    }
    private static SoundSystem instance = null;
    public static SoundSystem Instance
    {
        get 
        {
            if (instance != null)
                return instance;

            GameObject goTemplate = Resources.Load ("SoundSystem") as GameObject;
            GameObject go = GameObject.Instantiate (goTemplate);
            go.transform.name = "SoundSystem";
            instance = go.GetComponent<SoundSystem>();
            return instance;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SoundSystem))]
    public class UpdateTextures : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SoundSystem myScript = (SoundSystem)target;

            if (GUILayout.Button("Refresh Names"))
            {
                myScript.Refresh();
            }
        }
    }
#endif
}
