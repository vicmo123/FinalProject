using UnityEditor;
using UnityEngine;

public class TextureColorEditor : EditorWindow
{
    private Texture2D texture;
    private Color sourceColor;
    private Color targetColor;

    [MenuItem("Custom Tools/Texture Color Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TextureColorEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Modify Texture Colors", EditorStyles.boldLabel);

        // Add a field to select the texture
        texture = (Texture2D)EditorGUILayout.ObjectField("Texture", texture, typeof(Texture2D), false);

        // Add a color field to select the source color
        sourceColor = EditorGUILayout.ColorField("Source Color", sourceColor);

        // Add a color field to select the target color
        targetColor = EditorGUILayout.ColorField("Target Color", targetColor);

        // Add a button to apply the color change
        if (GUILayout.Button("Apply Color Change"))
        {
            ChangeTextureColors(texture, sourceColor, targetColor);
        }
    }

    private void ChangeTextureColors(Texture2D texture, Color sourceColor, Color targetColor)
    {
        // Loop through each pixel in the texture
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                // Get the color of the pixel
                Color color = texture.GetPixel(x, y);

                // Check if the color matches the source color
                if (color == sourceColor)
                {
                    // Update the color of the pixel in the texture
                    texture.SetPixel(x, y, targetColor);
                }
            }
        }

        // Apply the changes to the texture
        texture.Apply();

        // Save the changes to the asset
        EditorUtility.SetDirty(texture);
    }
}