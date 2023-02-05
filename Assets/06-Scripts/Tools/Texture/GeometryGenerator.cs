using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryGenerator
{
    static public Texture2D CreateTextureCircle(int size)
    {
        Texture2D circleTex = new Texture2D(size, size);
        Color[] pixels = circleTex.GetPixels();
        int centerX = circleTex.width / 2;
        int centerY = circleTex.height / 2;
        int radius = circleTex.width / 2 - 1;
        for (int x = 0; x < circleTex.width; x++)
        {
            for (int y = 0; y < circleTex.height; y++)
            {
                int dist = (int)Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY));
                if (dist < radius)
                {
                    pixels[y * circleTex.width + x] = Color.white;
                }
                else
                {
                    pixels[y * circleTex.width + x] = Color.clear;
                }
            }
        }
        circleTex.SetPixels(pixels);
        circleTex.Apply();

        return circleTex;
    }

    public static Texture2D CreateTextureCircle(int size, Color color)
    {
        Texture2D texture = new Texture2D(size, size);
        float radius = size / 2.0f;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2(radius, radius));

                if (distance > radius)
                {
                    texture.SetPixel(x, y, Color.clear);
                }
                else
                {
                    texture.SetPixel(x, y, color);
                }
            }
        }

        texture.Apply();
        return texture;
    }

    static public Texture2D CreateTextureCircle(ref Texture2D circleTex, int size)
    {
        Color[] pixels = circleTex.GetPixels();
        int centerX = circleTex.width / 2;
        int centerY = circleTex.height / 2;
        int radius = circleTex.width / 2 - 1;
        for (int x = 0; x < circleTex.width; x++)
        {
            for (int y = 0; y < circleTex.height; y++)
            {
                int dist = (int)Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY));
                if (dist < radius)
                {
                    pixels[y * circleTex.width + x] = Color.white;
                }
            }
        }
        circleTex.SetPixels(pixels);
        circleTex.Apply();

        return circleTex;
    }

    public static Texture2D CreateTextureCircle(ref Texture2D texture, int size, Color color)
    {
        float radius = size / 2.0f;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2(radius, radius));

                if (distance < radius)
                {
                    texture.SetPixel(x, y, color);
                }
            }
        }

        texture.Apply();
        return texture;
    }

    static public Texture2D SetColorTexture(Texture2D texture, Color newColor)
    {
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (texture.GetPixel(x, y) == Color.white)
                {
                    texture.SetPixel(x, y, newColor);
                }
            }
        }

        texture.Apply();

        return texture;
    }
}