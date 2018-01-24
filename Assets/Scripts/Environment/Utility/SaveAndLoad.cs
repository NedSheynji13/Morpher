using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class SaveAndLoad
{
    public static void Save()
    {
        string pathForms = Path.Combine(Application.persistentDataPath, "forms.txt");
        using (BinaryWriter writer = new BinaryWriter(File.Open(pathForms, FileMode.Create)))
            writer.Write(Convert.ToInt32(Morphing.unlocked));
    }

    public static void Load()
    {
        string pathForms = Path.Combine(Application.persistentDataPath, "forms.txt");
        using (BinaryReader reader = new BinaryReader(File.OpenRead(pathForms)))
            Morphing.unlocked = (Morphing.abilities)reader.ReadInt32();
    }

    public static void SavePosition(Vector3 _position)
    {
        SaveX(Convert.ToInt32(Mathf.Round(_position.x)));
        SaveY(Convert.ToInt32(Mathf.Round(_position.y)));
        SaveZ(Convert.ToInt32(Mathf.Round(_position.z)));
        SaveScene();
    }
    
    public static Vector3 LoadPosition()
    {
        return new Vector3(LoadX(), LoadY(), LoadZ());
    }

    public static void SaveX(int x)
    {
        string pathPosition = Path.Combine(Application.persistentDataPath, "positionx.txt");
        using (BinaryWriter writer = new BinaryWriter(File.Open(pathPosition, FileMode.Create)))
            writer.Write(x);
    }
    public static void SaveY(int y)
    {
        string pathPosition = Path.Combine(Application.persistentDataPath, "positiony.txt");
        using (BinaryWriter writer = new BinaryWriter(File.Open(pathPosition, FileMode.Create)))
            writer.Write(y);
    }
    public static void SaveZ(int z)
    {
        string pathPosition = Path.Combine(Application.persistentDataPath, "positionz.txt");
        using (BinaryWriter writer = new BinaryWriter(File.Open(pathPosition, FileMode.Create)))
            writer.Write(z);
    }
    public static void SaveScene()
    {
        string pathScene = Path.Combine(Application.persistentDataPath, "positionscene.txt");
        using (BinaryWriter writer = new BinaryWriter(File.Open(pathScene, FileMode.Create)))
            writer.Write(SceneManager.GetActiveScene().buildIndex);
    }
    public static int LoadX()
    {
        string pathPosition = Path.Combine(Application.persistentDataPath, "positionx.txt");
        using (BinaryReader reader = new BinaryReader(File.OpenRead(pathPosition)))
            return reader.Read();
    }
    public static int LoadY()
    {
        string pathPosition = Path.Combine(Application.persistentDataPath, "positiony.txt");
        using (BinaryReader reader = new BinaryReader(File.OpenRead(pathPosition)))
            return reader.Read();
    }
    public static int LoadZ()
    {
        string pathPosition = Path.Combine(Application.persistentDataPath, "positionz.txt");
        using (BinaryReader reader = new BinaryReader(File.OpenRead(pathPosition)))
            return reader.Read();
    }
    public static int LoadScene()
    {
        string pathScene = Path.Combine(Application.persistentDataPath, "positionscene.txt");
        using (BinaryReader reader = new BinaryReader(File.OpenRead(pathScene)))
            return reader.Read();
    }
}
