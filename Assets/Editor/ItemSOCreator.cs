#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
public class ItemSOCreator
{
    [MenuItem("Assets/Create/ItemSOBronze", false, 20)]
    public static void ItemSOBronze()
    {
        foreach (Object obj in Selection.objects)
        {
            if (obj is Sprite sprite)
            {
                ItemSO itemSO = ScriptableObject.CreateInstance<ItemSO>();

                SerializedObject serializedObject = new SerializedObject(itemSO);
                serializedObject.FindProperty("ItemName").stringValue = sprite.name;
                serializedObject.FindProperty("ItemSprite").objectReferenceValue = sprite;
                serializedObject.FindProperty("ItemQuantity").intValue = 10;
                serializedObject.ApplyModifiedProperties();

                string savePath = AssetDatabase.GenerateUniqueAssetPath($"Assets/ScriptableObjects/BronzeWheelItemSO/{sprite.name}_ItemSO.asset");

                AssetDatabase.CreateAsset(itemSO, savePath);

                AssetDatabase.SaveAssets();

            }
        }
    }
    [MenuItem("Assets/Create/ItemSOSilver", false, 20)]
    public static void ItemSOSilver()
    {
        foreach (Object obj in Selection.objects)
        {
            if (obj is Sprite sprite)
            {
                ItemSO itemSO = ScriptableObject.CreateInstance<ItemSO>();

                SerializedObject serializedObject = new SerializedObject(itemSO);
                serializedObject.FindProperty("ItemName").stringValue = sprite.name;
                serializedObject.FindProperty("ItemSprite").objectReferenceValue = sprite;
                serializedObject.FindProperty("ItemQuantity").intValue = 10;
                serializedObject.ApplyModifiedProperties();

                string savePath = AssetDatabase.GenerateUniqueAssetPath($"Assets/ScriptableObjects/SilverWheelItemSO/{sprite.name}_ItemSO.asset");

                AssetDatabase.CreateAsset(itemSO, savePath);

                AssetDatabase.SaveAssets();

            }
        }
    }
    [MenuItem("Assets/Create/ItemSOGold", false, 20)]
    public static void ItemSOGold()
    {
        foreach (Object obj in Selection.objects)
        {
            if (obj is Sprite sprite)
            {
                ItemSO itemSO = ScriptableObject.CreateInstance<ItemSO>();

                SerializedObject serializedObject = new SerializedObject(itemSO);
                serializedObject.FindProperty("ItemName").stringValue = sprite.name;
                serializedObject.FindProperty("ItemSprite").objectReferenceValue = sprite;
                serializedObject.FindProperty("ItemQuantity").intValue = 10;
                serializedObject.ApplyModifiedProperties();

                string savePath = AssetDatabase.GenerateUniqueAssetPath($"Assets/ScriptableObjects/GoldWheelItemSO/{sprite.name}_ItemSO.asset");

                AssetDatabase.CreateAsset(itemSO, savePath);

                AssetDatabase.SaveAssets();

            }
        }
    }
}
#endif