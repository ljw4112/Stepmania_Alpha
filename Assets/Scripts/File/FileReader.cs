using Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileReader : MonoSingleton<FileReader>
{
    public SMFile ReadGameFile(string songName)
    {
        var data = Utils.GetMusicFile(songName);
        SMFile file = new SMFile(data);

        return file;
    }

    public T LoadFile<T>(string fileName, string path) where T : Object
    {
        string filePath = path.ToString() + "/" + fileName;
        T data = Resources.Load<T>(filePath);
        return data;
    }
}
