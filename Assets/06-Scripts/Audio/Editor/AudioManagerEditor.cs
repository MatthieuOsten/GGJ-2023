using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    new string name = "";
    List<Sound> listSounds = new List<Sound>();

    Texture2D cursor;

    private void OnValidate()
    {
        if (cursor == null)
        {
            cursor = CreateTextureCircle(64);
        }

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AudioManager myScript = (AudioManager)target;

            name = EditorGUILayout.TextField(name);
            if (GUILayout.Button("Play")) { myScript.Play(name); }

            // Display playing sounds
            foreach (var sound in myScript.sounds)
            {
                if (sound.Source == null) { continue; } 
                else if (sound.Source.time <= 0) { continue; }
            
                GUILayout.BeginVertical(GUI.skin.box);

                GUILayout.BeginHorizontal();
                GUILayout.Label(sound.Name);
                GUILayout.EndHorizontal();

                EditorGUI.BeginDisabledGroup(false);
                if (sound.Source != null && sound.Clip != null)
                {

                    BeautifulSlider(0, sound.Source.time, sound.Clip.length,Color.blue,Color.red);

                }
                EditorGUI.EndDisabledGroup();

                GUILayout.EndVertical();

                GUILayout.Space(10f);
            }

        // Display other sound don't playing
        foreach (var sound in myScript.sounds)
        {
            if (sound.Source == null) { }
            else if (sound.Source.time > 0) { continue; }

            GUILayout.BeginVertical(GUI.skin.box);

            GUILayout.BeginHorizontal();
            GUILayout.Label(sound.Name);
            GUILayout.EndHorizontal();

            EditorGUI.BeginDisabledGroup(true);
            if (sound.Clip != null)
            {
                BeautifulSlider(0, 0, sound.Clip.length);
            }
            EditorGUI.EndDisabledGroup();

            GUILayout.EndVertical();

            GUILayout.Space(10f);
        }

    }

    private void OnSceneGUI()
    {
        //Repaint();
    }

    private void BeautifulSlider(float valueMin, float valueActual, float valueMax)
    {
        // Create Style min GUIStyle
        GUIStyle styleMin = new GUIStyle();
        styleMin.alignment = TextAnchor.MiddleRight;
        styleMin.fontSize = 12;
        styleMin.normal.textColor = Color.white;

        // Create Style max GUIStyle
        GUIStyle styleMax = new GUIStyle();
        styleMax.alignment = TextAnchor.MiddleLeft;
        styleMax.fontSize = 12;
        styleMax.normal.textColor = Color.white;

        Color baseColor = GUI.color;

        // Draw slider, value min and max
        GUILayout.BeginHorizontal();    
            GUILayout.Label(valueMin.ToString(), styleMin,GUILayout.Width(20));
        GUILayout.Space(5f);
            GUILayout.HorizontalSlider(valueActual, valueMin, valueMax);
        GUILayout.Space(5f);
            GUILayout.Label(Mathf.Round(valueMax).ToString(), styleMax, GUILayout.Width(20));
        GUILayout.EndHorizontal();

        // Create Style actual GUIStyle
        GUIStyle styleActual = new GUIStyle();
        styleActual.alignment = TextAnchor.MiddleCenter;
        styleActual.fontSize = 14;
        styleActual.normal.textColor = Color.white;

        GUILayout.BeginHorizontal();
            GUILayout.Label(valueActual.ToString(), styleActual);
        GUILayout.EndHorizontal();
    }

    private void BeautifulSlider(float valueMin, float valueActual, float valueMax, Color colorMin,Color colorMax)
    {
        // ---- Get color of cursor slider ---- //
        Color colorActual = Color.Lerp(colorMin, colorMax, Mathf.Lerp(0,1,valueActual));

        // ---- Create Style min GUIStyle ---- //
        GUIStyle styleMin = new GUIStyle();
        styleMin.alignment = TextAnchor.MiddleRight;
        styleMin.fontSize = 12;

        styleMin.normal.textColor = colorMin;

        // ---- Create Style max GUIStyle ---- //
        GUIStyle styleMax = new GUIStyle();
        styleMax.alignment = TextAnchor.MiddleLeft;
        styleMax.fontSize = 12;

        styleMax.normal.textColor = colorMax;

        // ---- Draw slider, value min and max ---- //

        // Draw Min value
        GUILayout.BeginHorizontal();
            GUILayout.Label(valueMin.ToString(), styleMin, GUILayout.Width(20));
        GUILayout.Space(5f);

        // Get image of cursor slider and modifie the image
        Texture2D thumbTex = CreateTextureCircle(512);
        if (thumbTex != null)
        {
            // Set the color
            thumbTex = SetColorTexture(thumbTex,colorActual);

            // Create style of slider
            GUIStyle thumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
            thumbStyle.normal.background = thumbTex;

            // Draw slider
            GUILayout.HorizontalSlider(valueActual, valueMin, valueMax,GUI.skin.horizontalSlider, thumb: thumbStyle);
        }
        
        // Draw Max value
        GUILayout.Space(5f);
            GUILayout.Label(Mathf.Round(valueMax).ToString(), styleMax, GUILayout.Width(20));
        GUILayout.EndHorizontal();

        // ---- Create Style actual GUIStyle ---- //
        GUIStyle styleActual = new GUIStyle();
        styleActual.alignment = TextAnchor.MiddleCenter;
        styleActual.fontSize = 14;

        styleActual.normal.textColor = colorActual; 

        // ---- Draw actual value of slider ---- //
        GUILayout.BeginHorizontal();
            GUILayout.Label(valueActual.ToString(), styleActual);
        GUILayout.EndHorizontal();
    }

    private float EditorBeautifulSlider(float valueMin, float valueActual, float valueMax)
    {
        // Create Style min GUIStyle
        GUIStyle styleMin = new GUIStyle();
        styleMin.alignment = TextAnchor.MiddleRight;
        styleMin.fontSize = 12;
        styleMin.normal.textColor = Color.white;

        // Create Style max GUIStyle
        GUIStyle styleMax = new GUIStyle();
        styleMax.alignment = TextAnchor.MiddleLeft;
        styleMax.fontSize = 12;
        styleMax.normal.textColor = Color.white;

        Color baseColor = GUI.color;

        // Draw slider, value min and max
        GUILayout.BeginHorizontal();
        GUILayout.Label(valueMin.ToString(), styleMin, GUILayout.Width(20));
        GUILayout.Space(5f);
        GUILayout.HorizontalSlider(valueActual, valueMin, valueMax);
        GUILayout.Space(5f);
        GUILayout.Label(Mathf.Round(valueMax).ToString(), styleMax, GUILayout.Width(20));
        GUILayout.EndHorizontal();

        // Create Style actual GUIStyle
        GUIStyle styleActual = new GUIStyle();
        styleActual.alignment = TextAnchor.MiddleCenter;
        styleActual.fontSize = 14;
        styleActual.normal.textColor = Color.white;

        GUILayout.BeginHorizontal();
        GUILayout.Label(valueActual.ToString(), styleActual);
        GUILayout.EndHorizontal();

        return valueActual;
    }

    private float EditorBeautifulSlider(float valueMin, float valueActual, float valueMax, Color colorMin, Color colorMax)
    {
        // ---- Get color of cursor slider ---- //
        Color colorActual = Color.Lerp(colorMin, colorMax, Mathf.Lerp(0, 1, valueActual));

        // ---- Create Style min GUIStyle ---- //
        GUIStyle styleMin = new GUIStyle();
        styleMin.alignment = TextAnchor.MiddleRight;
        styleMin.fontSize = 12;

        styleMin.normal.textColor = colorMin;

        // ---- Create Style max GUIStyle ---- //
        GUIStyle styleMax = new GUIStyle();
        styleMax.alignment = TextAnchor.MiddleLeft;
        styleMax.fontSize = 12;

        styleMax.normal.textColor = colorMax;

        // ---- Draw slider, value min and max ---- //

        // Draw Min value
        GUILayout.BeginHorizontal();
        GUILayout.Label(valueMin.ToString(), styleMin, GUILayout.Width(20));
        GUILayout.Space(5f);

        // Get image of cursor slider and modifie the image
        Texture2D thumbTex = CreateTextureCircle(512);
        if (thumbTex != null)
        {
            // Set the color
            thumbTex = SetColorTexture(thumbTex, colorActual);

            // Create style of slider
            GUIStyle thumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
            thumbStyle.normal.background = thumbTex;

            // Draw slider
            valueActual = GUILayout.HorizontalSlider(valueActual, valueMin, valueMax, GUI.skin.horizontalSlider, thumb: thumbStyle);
        }

        // Draw Max value
        GUILayout.Space(5f);
        GUILayout.Label(Mathf.Round(valueMax).ToString(), styleMax, GUILayout.Width(20));
        GUILayout.EndHorizontal();

        // ---- Create Style actual GUIStyle ---- //
        GUIStyle styleActual = new GUIStyle();
        styleActual.alignment = TextAnchor.MiddleCenter;
        styleActual.fontSize = 14;

        styleActual.normal.textColor = colorActual;

        // ---- Draw actual value of slider ---- //
        GUILayout.BeginHorizontal();
        GUILayout.Label(valueActual.ToString(), styleActual);
        GUILayout.EndHorizontal();

        return valueActual;
    }

    private float EditorBeautifulSlider(float valueMin, float valueActual, float valueMax, Color colorMin, Color colorMax, Texture2D thumbTex)
    {
        // ---- Get color of cursor slider ---- //
        Color colorActual = Color.Lerp(colorMin, colorMax, Mathf.Clamp(valueActual, 0, 1));

        // ---- Create Style min GUIStyle ---- //
        GUIStyle styleMin = new GUIStyle();
        styleMin.alignment = TextAnchor.MiddleRight;
        styleMin.fontSize = 12;

        styleMin.normal.textColor = colorMin;

        // ---- Create Style max GUIStyle ---- //
        GUIStyle styleMax = new GUIStyle();
        styleMax.alignment = TextAnchor.MiddleLeft;
        styleMax.fontSize = 12;

        styleMax.normal.textColor = colorMax;

        // ---- Draw slider, value min and max ---- //

        // Draw Min value
        GUILayout.BeginHorizontal();
        GUILayout.Label(valueMin.ToString(), styleMin, GUILayout.Width(20));
        GUILayout.Space(5f);

        // Get image of cursor slider and modifie the image
        if (thumbTex != null)
        {
            // Set the color
            if (thumbTex.GetPixel(thumbTex.width/2,thumbTex.height/2) != colorActual ) {
                thumbTex = SetColorTexture(thumbTex, colorActual);
            }

            // Create style of slider
            GUIStyle thumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
            thumbStyle.normal.background = thumbTex;

            // Draw slider
            valueActual = GUILayout.HorizontalSlider(valueActual, valueMin, valueMax, GUI.skin.horizontalSlider, thumb: thumbStyle);
        }

        // Draw Max value
        GUILayout.Space(5f);
        GUILayout.Label(Mathf.Round(valueMax).ToString(), styleMax, GUILayout.Width(20));
        GUILayout.EndHorizontal();

        // ---- Create Style actual GUIStyle ---- //
        GUIStyle styleActual = new GUIStyle();
        styleActual.alignment = TextAnchor.MiddleCenter;
        styleActual.fontSize = 14;

        styleActual.normal.textColor = colorActual;

        // ---- Draw actual value of slider ---- //
        GUILayout.BeginHorizontal();
        GUILayout.Label(valueActual.ToString(), styleActual);
        GUILayout.EndHorizontal();

        return valueActual;
    }

    private Texture2D CreateTextureCircle(int size)
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



    private Texture2D SetColorTexture(Texture2D texture, Color newColor)
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

    private void DisplayTexture(Texture2D texture)
    {
        EditorGUILayout.LabelField("Circle Texture Preview");
        GUILayout.Box(texture, GUILayout.Width(100), GUILayout.Height(100));
    }
}

#endif