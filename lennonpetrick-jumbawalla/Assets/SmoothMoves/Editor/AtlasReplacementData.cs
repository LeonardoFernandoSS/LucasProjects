using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SmoothMoves;

public class AtlasReplacementData : ScriptableObject
{
    [System.Serializable]
    public class TextureReplacementData
    {
        public Texture2D fromTexture;
        public Texture2D toTexture;

        public Rect dragDropRect;
    }

    private class SortTextureReplacementDataAscending : IComparer<TextureReplacementData>
    {
        int IComparer<TextureReplacementData>.Compare(TextureReplacementData a, TextureReplacementData b)
        {
            if (a.fromTexture.name.CompareTo(b.fromTexture.name) > 0)
                return 1;
            if (a.fromTexture.name.CompareTo(b.fromTexture.name) < 0)
                return -1;
            else
                return 0;
        }
    }

    public TextureAtlas textureAtlas;
    public string newTextureName = "";
    public List<TextureReplacementData> textureReplacements = new List<TextureReplacementData>();

    public void ReloadAtlas()
    {
        if (textureAtlas != null)
        {
            // trim out missing textures
            int index = 0;
            string textureGUID;
            while (index < textureReplacements.Count)
            {
                textureGUID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(textureReplacements[index].fromTexture));

                if (!textureAtlas.textureGUIDs.Contains(textureGUID))
                {
                    textureReplacements.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            // add new textures
            TextureReplacementData replacement;
            Texture2D texture;
            for (int tIndex = 0; tIndex < textureAtlas.textureGUIDs.Count; tIndex++)
            {
                texture = (Texture2D)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(textureAtlas.textureGUIDs[tIndex]), typeof(Texture2D));

                replacement = ContainsTexture(texture);

                if (replacement == null)
                {
                    replacement = new TextureReplacementData();
                    replacement.fromTexture = texture;
                    replacement.toTexture = null;
                    replacement.dragDropRect = new Rect();

                    textureReplacements.Add(replacement);
                }
            }

            textureReplacements.Sort(new SortTextureReplacementDataAscending());
        }
    }

    public void SetAtlas(TextureAtlas atlas)
    {
        textureAtlas = atlas;

        textureReplacements.Clear();

        TextureReplacementData replacement;
        for (int tIndex = 0; tIndex < textureAtlas.textureGUIDs.Count; tIndex++)
        {
            replacement = new TextureReplacementData();
            replacement.fromTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(textureAtlas.textureGUIDs[tIndex]), typeof(Texture2D));
            replacement.toTexture = null;
            replacement.dragDropRect = new Rect();

            textureReplacements.Add(replacement);
        }

        textureReplacements.Sort(new SortTextureReplacementDataAscending());
    }

    private TextureReplacementData ContainsTexture(Texture2D texture)
    {
        foreach (TextureReplacementData replacement in textureReplacements)
        {
            if (replacement.fromTexture == texture)
                return replacement;
        }

        return null;
    }
}
