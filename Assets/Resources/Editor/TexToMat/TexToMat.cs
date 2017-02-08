using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TexToMat : EditorWindow {

    Material curMaterial;
    Texture2D newTexture;
    string materialName = "NewMaterial";

    string materialPath = "Assets/Materials/";
    string texturePath = "Assets/Textures/";

    bool useEmissiveMap = true;

    bool consoleDebug = true;


    private List<string> textureNames = new List<string>(4);

    [MenuItem("Generic Tools/Tex To Mat")]
    private static void showEditor()
    {
        EditorWindow.GetWindow<TexToMat>(false, "Tex To Mat");     //setting this to false will allow us to dock the window
    }


    [MenuItem("Generic Tools/Tex To Mat", true)]        //will enable the dropdown menu item
    private static bool showEditorValidator(){
        return true;
    }

    void OnGUI()
    {
        materialName = EditorGUILayout.TextField("New Material Name", materialName);
        materialPath = EditorGUILayout.TextField("Material Path", materialPath);
        texturePath = EditorGUILayout.TextField("Texture Path", texturePath);


        if (GUILayout.Button("Create Material"))
        {
            AssignMaterial(consoleDebug);


        }

        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        {
            
            if (GUILayout.Button("Console Debug: " + consoleDebug))
            {
                consoleDebug = !consoleDebug;
            }
            if (GUILayout.Button("Use Emissive: " + useEmissiveMap))
            {
                useEmissiveMap = !useEmissiveMap;
            }

        }
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        GUILayout.Label("New Material will be set as...", EditorStyles.boldLabel);

        GUILayout.Label(materialPath + materialName + "_mat");

        GUILayout.Space(5);

        GUILayout.Label("Require Texture Names...", EditorStyles.boldLabel);

        GUILayout.Label(texturePath + materialName + "_AlbedoTransparency");
        GUILayout.Label(texturePath + materialName + "_MetallicaSmoothness");
        GUILayout.Label(texturePath + materialName + "_Normal");

        if(useEmissiveMap)
            GUILayout.Label(texturePath + materialName + "_Emissive");

    }

    void AssignMaterial(bool debugOn){

        int texIndex = 0;

        curMaterial = new Material(Shader.Find("Standard"));
        AssetDatabase.CreateAsset(curMaterial, materialPath + materialName + "_mat.mat");

        if(AssetDatabase.FindAssets(texturePath + materialName + "_AlbedoTransparency").Length < 0);
        {
            newTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(texturePath + materialName + "_AlbedoTransparency.png", typeof(Texture2D));
            curMaterial.SetTexture("_MainTex", newTexture);

            if(debugOn)
                Debug.Log(texturePath + materialName + "_AlbedoTransparency");

            texIndex = texIndex + 1;
        }

        if(AssetDatabase.FindAssets(texturePath + materialName + "_Normal").Length < 0);
        {
            newTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(texturePath + materialName + "_Normal.png", typeof(Texture2D));
            curMaterial.SetTexture("_BumpMap", newTexture);

            if(debugOn)
                Debug.Log(texturePath + materialName + "_Normal");
            
            texIndex = texIndex + 1;

        }

        if(AssetDatabase.FindAssets(texturePath + materialName + "_MetallicSmoothness").Length < 0);
        {
            newTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(texturePath + materialName + "_MetallicSmoothness.png", typeof(Texture2D));
            curMaterial.SetTexture("_MetallicGlossMap", newTexture);


            if(debugOn)
                Debug.Log(texturePath + materialName + "_MetallicSmoothness");
            
            texIndex = texIndex + 1;
        }
            
        if((AssetDatabase.FindAssets(texturePath + materialName + "_Emissive").Length < 0) && useEmissiveMap == true);
        {
            newTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(texturePath + materialName + "_Emissive.png", typeof(Texture2D));
            curMaterial.SetTexture("_EmissionMap", newTexture);


            if(debugOn)
                Debug.Log(texturePath + materialName + "_Emissive");

            texIndex = texIndex + 1;
        }

        Selection.activeObject = curMaterial;

        if (debugOn)
        {
            Debug.Log(texIndex + " Texture were found and assigned");
        }
    }

}
