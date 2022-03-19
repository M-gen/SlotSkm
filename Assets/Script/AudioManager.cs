using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    class AudioItem
    {
        public GameObject gameObject;
        public AudioSource AudioSource;
        public bool IsLoop;
        public string[] tags;

        public bool IsTag( string tag )
        {
            foreach( var v in tags )
            {
                if (tag == v) return true;
            }
            return false;
        }
    }

    List<AudioItem> audioItems = new List<AudioItem>();

    [SerializeField]
    GameObject SourceGameObject;

    class SoundVolumeByTag
    {
        public string tag;
        public float volume;
    }

    List<SoundVolumeByTag> soundVolumeByTags = new List<SoundVolumeByTag>();

    // Start is called before the first frame update
    void Start()
    {
        soundVolumeByTags.Add(new SoundVolumeByTag() { tag = "SE", volume = 0.5f });
        soundVolumeByTags.Add(new SoundVolumeByTag() { tag = "BGM", volume = 0.5f });
        //PlaySE(null, 1f, new string[] { "SE" });
    }

    // Update is called once per frame
    void Update()
    {
        var tmp = new List<AudioItem>();
        foreach ( var i in audioItems)
        {
            if ( !i.AudioSource.isPlaying )
            {
                Destroy(i.gameObject);
            }
            else
            {
                tmp.Add(i);
            }
        }
        audioItems = tmp;
    }

    struct _PlayAudioStatus
    {
        public AudioClip Clip;
        public float Volume;
        public string[] Tags;
        public bool IsLoop;
        public float Delay;
    }

    public void PlayAudio( AudioClip clip, float volume, string[] tags, bool IsLoop = false, float delay = 0f)
    {
        if (delay == 0)
        {
            _PlayAudiCore(clip, volume, tags, IsLoop);
        }
        else
        {
            var o = new _PlayAudioStatus(){ Clip=clip, Volume=volume, Tags=tags, IsLoop=IsLoop, Delay=delay };
            StartCoroutine("_PlayAudioDelay", o);
        }
    }

    IEnumerator _PlayAudioDelay( object o)
    {
        var v = (_PlayAudioStatus)o;

        yield return new WaitForSeconds(v.Delay);
        _PlayAudiCore(v.Clip, v.Volume, v.Tags, v.IsLoop);
    }

    void _PlayAudiCore(AudioClip clip, float volume, string[] tags, bool IsLoop = false)
    {
        foreach (var v in tags)
        {
            if (v == "BGM")
            {
                var isOK = false;       // BGMÇ™ë∂ç›ÇµÇƒÇ¢ÇÈ
                var isContinue = false; // ç°Ç†ÇÈBGMÇ™àÍívÇµÇƒÇ¢ÇÈÇÃÇ≈åpë±Ç≥ÇπÇΩÇ¢
                foreach (var i in audioItems)
                {
                    if (i.IsTag("BGM")) {
                        isOK = true;
                        if (i.AudioSource.clip == clip)
                        {
                            isContinue = true;
                        }
                    }
                }

                Debug.Log($"_PlayAudiCore {isOK} {isContinue} {clip.name}");
                if (isOK)
                {
                    if (isContinue)
                    {
                        return;
                    }
                    DeleteAudios("BGM");
                }
                break;
            }
        }

        var item = new AudioItem();
        var o = Instantiate<GameObject>(SourceGameObject, this.transform);
        o.name = "Audio";
        item.gameObject = o;
        item.AudioSource = o.AddComponent<AudioSource>();
        item.IsLoop = IsLoop;
        item.tags = tags;
        audioItems.Add(item);

        item.AudioSource.clip = clip;
        item.AudioSource.volume = GetVolumeByTag(tags) * volume;
        item.AudioSource.Play();
        if (IsLoop) item.AudioSource.loop = true;
    }

    public void DeleteAudios( string tag )
    {
        var tmp = new List<AudioItem>();
        foreach (var i in audioItems)
        {
            var isOK = false;
            foreach( var j in i.tags)
            {
                if(j == tag)
                {
                    isOK = true;
                    break;
                }
            }

            if (isOK)
            {
                Destroy(i.gameObject);
            }
            else
            {
                tmp.Add(i);
            }
        }
        audioItems = tmp;
    }

    private float GetVolumeByTag( string[] tags)
    {
        foreach( var i in soundVolumeByTags)
        {
            foreach( var tag in tags)
            {
                if( tag == i.tag )
                {
                    return i.volume;
                }
            }
        }

        return 1;
    }
}
