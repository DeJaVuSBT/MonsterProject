using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Windows;

public class Replace : MonoBehaviour
{
    public string BaseName;
    public Texture2D inputTexture;
    public Texture2D[] overlayTexture;

    // Start is called before the first frame update
    void Start()
    {
        int textureIndex;
        string texturePath = AssetDatabase.GetAssetPath(inputTexture);
        Texture2D newTexture = new Texture2D(2, 2);
        byte[] textureData = File.ReadAllBytes(texturePath);
        newTexture.LoadImage(textureData);

        string overlayPath;
        Texture2D newOverlayTexture = new Texture2D(2, 2);
        byte[] overlayData;

        float alpha0;
        float red0;
        float green0;
        float blue0;
        float red1;
        float green1;
        float blue1;

        Color pixelColor;
        for (textureIndex = 0;  textureIndex < overlayTexture.Length; textureIndex++)
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
                    pixelColor = new Color(
                        ((1 - alpha0) * red1 + alpha0 * red0),
                        ((1 - alpha0) * green1 + alpha0 * green0),
                        ((1 - alpha0) * blue1 + alpha0 * blue0),
                        1);
                    newTexture.SetPixel(x, y, pixelColor);
                }
            }
            newTexture.Apply();

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
