using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class GameSoundsEnumGenerator
{
    private const string SOUNDS_FOLDER_PATH = "Assets/Resources/Sounds/AudioPrefabs/";

    [MenuItem("Custom Tools/Add Sounds To Enum")]
    public static void GenerateGameSoundsEnum()
    {
        string[] prefabPaths = Directory.GetFiles(SOUNDS_FOLDER_PATH, "*.prefab");
        string enumFilePath = $"{Application.dataPath}/Scripts/Sound/SoundListEnum.cs";

        //Gathers a list of all the names that are already present inside the enum
        List<string> existingNames = new List<string>();
        if (File.Exists(enumFilePath))
        {
            string existingEnumContent = File.ReadAllText(enumFilePath);
            MatchCollection matches = Regex.Matches(existingEnumContent, @"\b([A-Za-z0-9_]+)\b");
            foreach (Match match in matches)
            {
                existingNames.Add(match.Groups[1].Value);
            }
        }

        //Adds the sounds to the enum checking the previous list
        string enumFileContent = "public enum SoundListEnum {\n";
        for (int i = 0; i < prefabPaths.Length; i++)
        {
            string prefabPath = prefabPaths[i];
            string prefabName = Path.GetFileNameWithoutExtension(prefabPath);
            string enumValue = Regex.Replace(prefabName, @"[^a-zA-Z0-9_]", "");
            enumFileContent += $"\t{enumValue}";

            if (i < prefabPaths.Length - 1)
            {
                enumFileContent += ",";
            }
            enumFileContent += "\n";
        }
        enumFileContent += "}";

        File.WriteAllText(enumFilePath, enumFileContent);
        AssetDatabase.Refresh();
    }
}
