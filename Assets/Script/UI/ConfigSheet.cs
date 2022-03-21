using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConfigSheet : MonoBehaviour
{
    [SerializeField]
    AudioManager AudioManager;

    [SerializeField]
    LineScript LineScript;

    [SerializeField]
    UnityEngine.UI.Slider SliderBGM;

    [SerializeField]
    UnityEngine.UI.Slider SliderSE;

    [SerializeField]
    UnityEngine.UI.Slider SliderReel;

    [SerializeField]
    AudioClip OnClickSE;

    string configFilePath = "config.txt";

    private void Start()
    {

        if (File.Exists(configFilePath))
        {
            var fs = new FileStream( configFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(fs, System.Text.Encoding.UTF8);
            Debug.Log("StreamReader");
            while (!sr.EndOfStream)
            {
                var tmp = sr.ReadLine();
                Debug.Log(tmp);
                var line = tmp.Split(' ');
                if (line.Length == 0) continue;

                Debug.Log($"StreamReader {line[0]} {line[1]}");
                switch (line[0])
                {
                    case "BGM":
                        {
                            var value = float.Parse(line[1]);
                            AudioManager.SetVolume(value, "BGM");
                            SliderBGM.value = value;
                        }
                        break;
                    case "SE":
                        {
                            var value = float.Parse(line[1]);
                            AudioManager.SetVolume(value, "SE");
                            SliderSE.value = value;
                        }
                        break;
                    case "REEL":
                        {
                            var value = float.Parse(line[1]);
                            SliderReel.value = value;
                        }
                        break;
                }
            }

            sr.Close();
            fs.Close();
        }

        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        gameObject.SetActive(false);
        AudioManager.PlayAudio(OnClickSE, 0.3f, new string[] { "SE" });
    }

    public void UpdateConfig()
    {
        using (var f = new System.IO.StreamWriter(configFilePath, false, System.Text.Encoding.UTF8))
        {
            f.WriteLine($"BGM {SliderBGM.value}");
            f.WriteLine($"SE {SliderSE.value}");
            f.WriteLine($"REEL {SliderReel.value}");
        }

        Debug.Log("UpdateConfig");
    }
}
