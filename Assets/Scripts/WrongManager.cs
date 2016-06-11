using UnityEngine;
using System.Collections;
using System.IO;
using ProtoBuf;

public class WrongManager
{
    private static Wrong wrong;

    public static void Load()
    {
        string file = Application.persistentDataPath + "/wrong";
        if (File.Exists(file))
        {
            var f = File.OpenRead(file);
            wrong = Serializer.Deserialize<Wrong>(f);
            f.Close();
        }
        else
        {
            wrong = new Wrong();
        }
    }

    public static void Save()
    {
        var f = File.Create(Application.persistentDataPath + "/wrong");
        Serializer.Serialize(f, wrong);
        f.Close();
    }
}