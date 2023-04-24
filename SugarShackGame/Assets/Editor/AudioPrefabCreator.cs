using UnityEngine;
using UnityEditor;
using System.IO;

public class AudioPrefabCreator : EditorWindow
{
    private string audioFolderPath = "";
    private string prefabFolderPath = "";

    [MenuItem("Custom Tools/Audio Prefab Creator")]
    static void Init()
    {
        AudioPrefabCreator window = (AudioPrefabCreator)EditorWindow.GetWindow(typeof(AudioPrefabCreator));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Select Folder Containing Audio Clips(In Resources): ", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Folder Path: ", GUILayout.Width(100));
        audioFolderPath = EditorGUILayout.TextField(audioFolderPath);
        if (GUILayout.Button("Select", GUILayout.Width(60)))
        {
            audioFolderPath = EditorUtility.OpenFolderPanel("Select Folder Containing Audio Clips(In Resources): ", "", "");
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.Label("Select Folder to Save Prefabs(In Resources): ", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Prefab Folder Path: ", GUILayout.Width(130));
        prefabFolderPath = EditorGUILayout.TextField(prefabFolderPath);
        if (GUILayout.Button("Select", GUILayout.Width(60)))
        {
            prefabFolderPath = EditorUtility.OpenFolderPanel("Select Folder to Save Prefabs(In Resources): ", "", "");
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        if (GUILayout.Button("Create Audio Prefabs"))
        {
            if (audioFolderPath.Contains("Resources") && prefabFolderPath.Contains("Resources"))
            {
                CreateAudioPrefabs();
            }
            else
            {
                Debug.Log("The selected folders are not in the Resources folder");
            }
        }
    }

    bool prefabsCreated = false;

    void CreateAudioPrefabs()
    {
        if (prefabsCreated)
        {
            Debug.LogWarning("Prefabs have already been created.");
            return;
        }

        string[] audioFiles = Directory.GetFiles(audioFolderPath);

        foreach (string audioFile in audioFiles)
        {
            if (!audioFile.Contains(".meta"))
            {
                string clipName = Path.GetFileNameWithoutExtension(audioFile);

                // Load the audio clip using Resources.Load()
                AudioClip clip = Resources.Load<AudioClip>("Audio/" + clipName);

                // Create the prefab
                GameObject prefab = new GameObject(clipName);
                AudioSource audioSource = prefab.AddComponent<AudioSource>();
                audioSource.clip = clip;

                // Save the prefab
                string prefabPath = prefabFolderPath + "/" + clipName + ".prefab";
                PrefabUtility.SaveAsPrefabAsset(prefab, prefabPath);
            }
        }

        prefabsCreated = true;
    }
}