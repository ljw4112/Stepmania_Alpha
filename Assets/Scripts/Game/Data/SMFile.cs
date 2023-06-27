using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SMFile
{
    public string title;
    public string subTitle;
    public string artist;
    public string titleTranslit;
    public string subtitleTranslit;
    public string artistTranslit;
    public string genre;
    public string credit;
    public Sprite banner;
    public Sprite background;
    public string lyricsPath;
    public Sprite cdtitle;
    public AudioClip music;
    public float offset;
    public float sampleStart;
    public float sampleLength;
    public bool selectable;
    public List<Dictionary<float, float>> bpms;
    public List<Dictionary<float, float>> stops;
    public string bgChanges;
    public string keySounds;
    public string curSaveAddr;
    public float beat;

    public SMFile((string, string) asset)
    {
        curSaveAddr = asset.Item2;
        var reader = new StringReader(asset.Item1);
        while (true)
        {
            string rawData = reader.ReadLine();
            if (string.IsNullOrEmpty(rawData)) break;
            
            // 필요없는 데이터 삭제
            rawData = rawData.Remove(0, 1);
            rawData = rawData.Remove(rawData.Length - 1, 1);
            string[] split = rawData.Split(':');
            Utils.SetSMFileData(this, split[0], split[1]);
        }
        beat = 1;
    }

    public override string ToString()
    {
        return curSaveAddr;
    }
}
