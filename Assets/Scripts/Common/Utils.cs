using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Options;

public class Utils : MonoBehaviour
{
    public static (string, string) GetMusicFile(string musicName)
    {
        string musicPath = FILEPATH.MUSIC_PATH + musicName + "/" + musicName + ".sm";
        string folderPath = FILEPATH.MUSIC_PATH + musicName;
        string textAsset = File.ReadAllText("Assets/Resources/" + musicPath);

        if (textAsset != null) return (textAsset, folderPath);
        else return (null, null);
    }

    public static void SetSMFileData(SMFile smFile, string s1, string s2)
    {
        switch (s1)
        {
            case "TITLE":
                {
                    smFile.title = s2;
                    break;
                }

            case "SUBTITLE":
                {
                    smFile.subTitle = s2;
                    break;
                }

            case "ARTIST":
                {
                    smFile.artist = s2;
                    break;
                }

            case "TITLETRANSLIT":
                {
                    smFile.titleTranslit = s2;
                    break;
                }

            case "SUBTITLETRANSLIT":
                {
                    smFile.subtitleTranslit = s2;
                    break;
                }

            case "ARTISTTRANSLIT":
                {
                    smFile.artistTranslit = s2;
                    break;
                }

            case "GENRE":
                {
                    smFile.genre = s2;
                    break;
                }

            case "CREDIT":
                {
                    smFile.credit = s2;
                    break;
                }

            case "BANNER":
                {
                    smFile.banner = FileReader.Instance.LoadFile<Sprite>(s2, smFile.curSaveAddr);
                    break;
                }

            case "BACKGROUND":
                {
                    smFile.background = FileReader.Instance.LoadFile<Sprite>(s2, smFile.curSaveAddr);
                    break;
                }

            case "LYRICSPATH":
                {
                    smFile.lyricsPath = s2;
                    break;
                }

            case "CDTITLE":
                {
                    smFile.cdtitle = FileReader.Instance.LoadFile<Sprite>(s2, smFile.curSaveAddr);
                    break;
                }

            case "MUSIC":
                {
                    string pathWithoutExt = Path.ChangeExtension(s2, null);
                    smFile.music = FileReader.Instance.LoadFile<AudioClip>(pathWithoutExt, smFile.curSaveAddr);
                    break;
                }

            case "OFFSET":
                {
                    smFile.offset = float.Parse(s2);
                    break;
                }

            case "SAMPLESTART":
                {
                    smFile.sampleStart = float.Parse(s2);
                    break;
                }

            case "SAMPLELENGTH":
                {
                    smFile.sampleLength = float.Parse(s2);
                    break;
                }

            case "SELECTABLE":
                {
                    smFile.selectable = s2 == "YES";
                    break;
                }

            case "BPMS":
                {
                    if (smFile.bpms == null)
                        smFile.bpms = new List<Dictionary<float, float>>();

                    string[] splitBpms = s2.Split(',');
                    if (splitBpms.Length == 1)
                    {
                        string[] bpms = splitBpms[0].Split('=');
                        Dictionary<float, float> bpm = new Dictionary<float, float>()
                        {
                            { float.Parse(bpms[0]), float.Parse(bpms[1]) }
                        };
                        smFile.bpms.Add(bpm);
                    }
                    else
                    {
                        for (int i = 0; i < splitBpms.Length; i++)
                        {
                            string[] bpms = splitBpms[i].Split('=');
                            Dictionary<float, float> bpm = new Dictionary<float, float>()
                        {
                            {
                                float.Parse(bpms[0]), float.Parse(bpms[1])
                            }
                        };
                            smFile.bpms.Add(bpm);
                        }
                    }
                    break;
                }

            case "STOPS":
                {
                    break;
                }

            case "BGCHANGES":
                {
                    break;
                }

            case "KEYSOUNDS":
                {
                    break;
                }
        }
    }

    public static Coroutine InitializeCoroutine(Coroutine cor)
    {
        if (cor == null) return null;
        FileReader.Instance.StopCoroutine(cor);
        cor = null;
        return cor;
    }
}
