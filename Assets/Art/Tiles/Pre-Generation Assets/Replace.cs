using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Windows;

public class Replace : MonoBehaviour
{
    public string BaseName; //What the output images will be called
    public Texture2D inputTexture; //Texture 2D input of the base texture
    public Texture2D[] overlayTexture; //Texture 2D input of the overlay textures

    void Start()
    {
        int textureIndex; //Current texture number
        string texturePath = AssetDatabase.GetAssetPath(inputTexture); //Path to the base texture
        Texture2D newTexture = new Texture2D(2, 2); //Creating a new texture
        byte[] textureData = File.ReadAllBytes(texturePath); //Storing image data of base texture
        newTexture.LoadImage(textureData); //Loading image data of base texture onto new texture

        string overlayPath; //Path to the overlay textures
        Texture2D newOverlayTexture = new Texture2D(2, 2); //Creating a new texture for the overlay
        byte[] overlayData; //Storing image data of overlay texture

        //Holds color values per channel of both base and overlay texture
        float alpha0;
        float red0;
        float green0;
        float blue0;
        float red1;
        float green1;
        float blue1;

        Color pixelColor; //Creating new color to control color of current pixel being processed

        //Loop to change current pixel if the overlay texture replaces the base texture
        for (textureIndex = 0; textureIndex < overlayTexture.Length; textureIndex++)
        {
            overlayPath = AssetDatabase.GetAssetPath(overlayTexture[textureIndex]);
            overlayData = File.ReadAllBytes(overlayPath);

            newOverlayTexture.LoadImage(overlayData);

            for (int x = 0; x < newTexture.width; x++)
            {
                for (int y = 0; y < newTexture.height; y++)
                {
                    alpha0 = newOverlayTexture.GetPixel(x, y).a;
                    red1 = newTexture.GetPixel(x, y).r;
                    green1 = newTexture.GetPixel(x, y).g;
                    blue1 = newTexture.GetPixel(x, y).b;
                    red0 = newOverlayTexture.GetPixel(x, y).r;
                    green0 = newOverlayTexture.GetPixel(x, y).g;
                    blue0 = newOverlayTexture.GetPixel(x, y).b;
                    //Combining colors based on the overlay alpha value
                    pixelColor = new Color(
                        ((1 - alpha0) * red1 + alpha0 * red0),
                        ((1 - alpha0) * green1 + alpha0 * green0),
                        ((1 - alpha0) * blue1 + alpha0 * blue0),
                        1);
                    newTexture.SetPixel(x, y, pixelColor);
                }
            }
            newTexture.Apply();

            //Generating a PNG file of the newly created texture
            byte[] bytes = newTexture.EncodeToPNG();
            var dirPath = Application.dataPath + "/SaveImages/" + BaseName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            File.WriteAllBytes(dirPath + BaseName + textureIndex + ".png", bytes);
            newTexture.LoadImage(textureData);
        }
    }
}
