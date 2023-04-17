using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureEditorTool : EditorWindow
{
    public Texture2D textureToModify;
    private Color sourceColor;
    private Color targetColor;

    [MenuItem("Tools/Texture Editor Tool")]
    public static void ShowWindow()
    {
        TextureEditorTool window = (TextureEditorTool)EditorWindow.GetWindow(typeof(TextureEditorTool));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Texture Modifier", EditorStyles.boldLabel);

        textureToModify = EditorGUILayout.ObjectField("Texture To Modify", textureToModify, typeof(Texture2D), false) as Texture2D;
        sourceColor = EditorGUILayout.ColorField("Color To Change", sourceColor);
        targetColor = EditorGUILayout.ColorField("New Color", targetColor);

        if (GUILayout.Button("Modify Texture"))
        {
            ModifyTexture();
        }
    }

    void ModifyTexture()
    {
        if (textureToModify == null)
        {
            Debug.LogError("Please select a texture to modify");
            return;
        }

        int width = textureToModify.width;
        int height = textureToModify.height;
        TextureFormat format = TextureFormat.RGBA32;
        bool mipMaps = textureToModify.mipmapCount > 1;

        Texture2D newTexture = new Texture2D(width, height, format, mipMaps);

        newTexture.wrapMode = textureToModify.wrapMode;
        newTexture.filterMode = textureToModify.filterMode;
        newTexture.anisoLevel = textureToModify.anisoLevel;

        Color[] pixels = textureToModify.GetPixels();

        for (int i = 0; i < pixels.Length; i++)
        {
            float distance = Mathf.Sqrt(
                Mathf.Pow(pixels[i].r - sourceColor.r, 2f) +
                Mathf.Pow(pixels[i].g - sourceColor.g, 2f) +
                Mathf.Pow(pixels[i].b - sourceColor.b, 2f));

            if (distance < 0.1f)
            {
                pixels[i] = targetColor;
            }
        }

        newTexture.SetPixels(pixels);
        newTexture.Apply();

        string texturePath = AssetDatabase.GetAssetPath(textureToModify);
        byte[] textureData = newTexture.EncodeToPNG();
        File.WriteAllBytes(texturePath, textureData);

        AssetDatabase.ImportAsset(texturePath);
        textureToModify = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
    }
}