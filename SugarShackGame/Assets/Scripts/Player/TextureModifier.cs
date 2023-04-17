using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureModifier : MonoBehaviour
{
    public void Start()
    {
    }

    public void ChangeColorOfMat(Color colorToChange, Color newColor)
    {
        // Get the renderer component that is attached to the object
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        // Get the current material
        Material material = renderer.material;

        // Get the texture from the material
        Texture2D texture = (Texture2D)material.GetTexture("Lumber");

        // Loop through each pixel in the texture
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                // Get the color of the pixel
                Color color = texture.GetPixel(x, y);

                // Check if the blue component of the color is greater than 0.5
                if (color == colorToChange)
                {
                    // Update the color of the pixel in the texture
                    texture.SetPixel(x, y, newColor);
                }
            }
        }

        // Apply the changes to the texture
        texture.Apply();

        // Set the modified texture back to the material
        material.SetTexture("Lumber", texture);
    }
}
