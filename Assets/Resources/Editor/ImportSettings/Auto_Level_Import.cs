using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class AutoImportSettings : EditorWindow {

    //This script will change the import settings for the images used by the ZeroG Level Design team so that the GridMap script can read them and create the maps.

    Texture2D newLevel;
    string imageName = "LevelImage";
    string imagePath = "Assets/Art/2D/Level Layouts/";
    string newImagePath = "Assets/Art/2D/Level Layouts/";
    public bool isReadable = true;

    [MenuItem("Level Design/Auto Import Settings")]
    private static void showEditor()
    {
        EditorWindow.GetWindow<AutoImportSettings>(false, "Auto Import Settings");
    }

    [MenuItem("Level Design/Auto Import Settings", true)]
    private static bool showEditorValidator()
    {
        return true;
    }

    void OnGUI()
    {
        imageName = EditorGUILayout.TextField("Level Name", imageName);
        imagePath = EditorGUILayout.TextField("Original Image Path", imagePath);
        newImagePath = EditorGUILayout.TextField("New Image Path", newImagePath);

        GUILayout.BeginHorizontal();
        {

            if (GUILayout.Button("Apply Import Settings"))
            {
                ApplyImportSettings(true);
            }

            GUILayout.Space(15);

            GUILayout.EndHorizontal();
            GUILayout.Space(15);

            GUILayout.Label("The image will be imported with the name:", EditorStyles.boldLabel);
            GUILayout.Label(imageName);

            }
        }

    void ApplyImportSettings(bool yep){

       // if (AssetDatabase.FindAssets(imagePath + imageName + ".png").Length < 0)
        //{
        newLevel = (Texture2D)AssetDatabase.LoadAssetAtPath(imagePath + imageName, typeof(Texture2D));
        var importSettings = AssetImporter.GetAtPath(imagePath + imageName) as TextureImporter;
        importSettings.textureType = TextureImporterType.Advanced;
        importSettings.mipmapEnabled = false;
        importSettings.isReadable = true;
        importSettings.npotScale = TextureImporterNPOTScale.None;

        AssetDatabase.ImportAsset(imagePath + imageName);
        AssetDatabase.Refresh();
        Debug.Log(imageName + "'s import settings were changed and the image can now be read by the level creation script");
       // }
    }
}
