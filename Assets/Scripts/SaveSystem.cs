using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static string _scoreDataPath = Application.persistentDataPath + "/score.data";
    private static BinaryFormatter _binaryFormatter = new BinaryFormatter();

    public static void SaveScore(int[,] score)
    {

        FileStream stream = new FileStream(_scoreDataPath, FileMode.Create);
        try
        {
            _binaryFormatter.Serialize(stream, score);
        }
        finally
        {
            stream.Close();
        }
    }

    public static int[,] LoadScore()
    {
        if (File.Exists(_scoreDataPath))
        {
            FileStream stream = new FileStream(_scoreDataPath, FileMode.Open);
            try
            {
                int[,] score = _binaryFormatter.Deserialize(stream) as int[,];
                return score;
            }
            finally
            {
                stream.Close();
            }
        }
        else
        {
            Debug.Log("Score file not found in " + _scoreDataPath + ". Creating new empty score.");
            ClearScore();
            return LoadScore();
        }
    }

    public static void ClearScore()
    {
        int[,] score = new int[3, 10];
        SaveScore(score);
    }
}
