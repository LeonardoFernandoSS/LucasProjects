using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using SmoothMoves;
using System.IO;

public class AtlasReplacer : EditorWindow
{
    #region Menu

    [MenuItem("SmoothMoves/Tools/Atlas Replacer")]
    static public void OpenAtlasReplacer()
    {
        AtlasReplacer.ShowWindow();
    }

    [MenuItem("SmoothMoves/Create/Atlas Replacement Data")]
    [MenuItem("Assets/Create/SmoothMoves/Atlas Replacement Data")]
    static public void CreateAtlasReplacementData()
    {
        CreateAsset<AtlasReplacementData>("SmoothMoves Atlas Replacement Data");
    }

    #endregion

    #region Rsources

    static public void CreateAsset<T>(string assetName) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + assetName + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

    #endregion

    #region Static Access

    static private AtlasReplacer _instance;

    static public AtlasReplacer Instance { get { return _instance; } }

    static public AtlasReplacer ShowWindow()
    {
        if (_instance == null)
        {
            _instance = (AtlasReplacer)EditorWindow.GetWindow(typeof(AtlasReplacer), false, "Atlas Replacer");
            _instance.ShowUtility();
        }

        _instance.ShowUtility();

        return _instance;
    }

    #endregion

    #region Members

    private AtlasReplacementData _replacementData = null;
    private List<AtlasReplacementData> _replaceDataList = new List<AtlasReplacementData>();
    private List<string> _replaceNameList = new List<string>();
    private int _replaceSelectedIndex = -1;

    private Rect _textureAtlasRect;
    private Rect _scrollRect;
    private Vector2 _scrollPosition = Vector2.zero;

    #endregion

    #region GUI

    void OnEnable()
    {
        GetAtlasReplacementDataList();
    }

    void OnGUI()
    {
        bool nothingSelected = false;

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Select Atlas Replacement Data:");

        int newReplacementSelectedIndex = EditorGUILayout.Popup(_replaceSelectedIndex, _replaceNameList.ToArray());
        if (newReplacementSelectedIndex != _replaceSelectedIndex)
        {
            _replaceSelectedIndex = newReplacementSelectedIndex;
            _replacementData = _replaceDataList[_replaceSelectedIndex];
            _replacementData.ReloadAtlas();            
        }

        GUILayout.EndHorizontal();

        if (_replacementData != null)
        {
            if (_replacementData.textureAtlas == null)
            {
                nothingSelected = true;
            }
            else
            {
                if (_replacementData.textureAtlas.material == null)
                {
                    nothingSelected = true;
                }
                else
                {
                    if (_replacementData.textureAtlas.material.mainTexture == null)
                    {
                        nothingSelected = true;
                    }
                }
            }

            if (nothingSelected)
            {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();

                GUILayout.Box(new GUIContent("Drag & Drop Atlas Here", ""), GUILayout.Width(100.0f), GUILayout.Height(100.0f));
                _textureAtlasRect = GUILayoutUtility.GetLastRect();

                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.BeginVertical();

                GUILayout.BeginHorizontal(GUI.skin.box);

                GUILayout.Box(new GUIContent(_replacementData.textureAtlas.material.mainTexture), GUILayout.Width(100.0f), GUILayout.Height(100.0f));
                _textureAtlasRect = GUILayoutUtility.GetLastRect();

                GUILayout.FlexibleSpace();

                GUILayout.Label("New Texture Name:");
                _replacementData.newTextureName = GUILayout.TextField(_replacementData.newTextureName, GUILayout.MinWidth(100.0f));

                if (GUILayout.Button("Create Atlas Texture"))
                {
                    CreateAtlasTexture();
                }

                GUILayout.EndHorizontal();

                GUILayout.BeginVertical();

                GUILayout.Label("Textures");

                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUI.skin.box);

                Texture2D texture;

                for (int replaceIndex = 0; replaceIndex < _replacementData.textureReplacements.Count; replaceIndex++)
                {
                    GUILayout.BeginHorizontal();

                    GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(140.0f), GUILayout.Height(140.0f));

                    texture = _replacementData.textureReplacements[replaceIndex].fromTexture;

                    GUILayout.Label(texture.name, GUI.skin.box);
                    GUILayout.Label(texture, GUI.skin.box, GUILayout.Width(100.0f), GUILayout.Height(100.0f));
                    GUILayout.Label(texture.width.ToString() + " x " + texture.height.ToString());

                    GUILayout.EndVertical();

                    GUILayout.BeginVertical();

                    GUILayout.Space(40.0f);

                    GUILayout.Label("Replace With >");

                    GUILayout.EndVertical();

                    GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(140.0f), GUILayout.Height(140.0f));

                    bool textureWillBeNull = false;

                    if (_replacementData.textureReplacements[replaceIndex].toTexture == null)
                    {
                        GUILayout.Label("");

                        GUILayout.Label("Drag & Drop Texture Here", GUI.skin.box, GUILayout.Width(100.0f), GUILayout.Height(100.0f));
                        _replacementData.textureReplacements[replaceIndex] .dragDropRect = GUILayoutUtility.GetLastRect();
                    }
                    else
                    {
                        texture = _replacementData.textureReplacements[replaceIndex].toTexture;

                        GUILayout.BeginHorizontal();
                        GUILayout.Label(texture.name, GUI.skin.box);
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("X", GUILayout.Width(20.0f)))
                        {
                            textureWillBeNull = true;
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.Label(texture, GUI.skin.box, GUILayout.Width(100.0f), GUILayout.Height(100.0f));
                        _replacementData.textureReplacements[replaceIndex].dragDropRect = GUILayoutUtility.GetLastRect();

                        GUILayout.Label(texture.width.ToString() + " x " + texture.height.ToString());
                    }

                    if (textureWillBeNull)
                    {
                        _replacementData.textureReplacements[replaceIndex].toTexture = null;
                    }

                    GUILayout.EndVertical();

                    GUILayout.FlexibleSpace();

                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();

                _scrollRect = GUILayoutUtility.GetLastRect();

                GUILayout.EndVertical();

                GUILayout.EndVertical();
            }
        }

        GUILayout.EndVertical();

        ProcessInput();
    }

    #endregion

    #region Input

    private void ProcessInput()
    {
        if (_replacementData == null)
            return;

        switch (Event.current.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:

                if (_textureAtlasRect.Contains(Event.current.mousePosition))
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (Event.current.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        SetAtlas(DragAndDrop.objectReferences);
                    }

                    Event.current.Use();
                }
                else
                {
                    int replaceIndex = -1;

                    for (int index = 0; index < _replacementData.textureReplacements.Count; index++)
                    {
                        if (_replacementData.textureReplacements[index].dragDropRect.Contains(Event.current.mousePosition + _scrollPosition - UpperLeftCorner(_scrollRect)))
                        {
                            replaceIndex = index;
                            break;
                        }
                    }

                    if (replaceIndex > -1)
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                        if (Event.current.type == EventType.DragPerform)
                        {
                            DragAndDrop.AcceptDrag();

                            SetTexture(DragAndDrop.objectReferences, replaceIndex);
                        }

                        Event.current.Use();
                    }
                }
                break;

            case EventType.DragExited:
                break;
        }
    }

    #endregion

    #region Functions

    private void GetAtlasReplacementDataList()
    {
        DirectoryInfo di;
        FileInfo[] allFiles;
        int assetPathIndex;
        string assetPath;
        AtlasReplacementData replacementData;

        _replaceDataList.Clear();
        _replaceNameList.Clear();

        di = new DirectoryInfo(Application.dataPath);
        allFiles = di.GetFiles("*.asset", SearchOption.AllDirectories);

        foreach (FileInfo file in allFiles)
        {
            assetPathIndex = file.FullName.IndexOf("Assets/");

            if (assetPathIndex == -1)
            {
                assetPathIndex = file.FullName.IndexOf(@"Assets\");
            }

            if (assetPathIndex >= 0)
            {
                assetPath = file.FullName.Substring(assetPathIndex, file.FullName.Length - assetPathIndex);

                replacementData = (AtlasReplacementData)AssetDatabase.LoadAssetAtPath(assetPath, typeof(AtlasReplacementData));

                if (replacementData != null)
                {
                    Debug.Log("added " + replacementData.name);

                    _replaceDataList.Add(replacementData);
                    _replaceNameList.Add(replacementData.name);
                }
            }
        }
    }

    public void SetAtlasReplacementData(Object obj)
    {
        if (obj == null)
            return;

        _replacementData = (AtlasReplacementData)obj;

        GetAtlasReplacementDataList();

        _replaceSelectedIndex = _replaceDataList.IndexOf(_replacementData);
        _replacementData.ReloadAtlas();            
    }

    private void SetAtlas(Object[] objs)
    {
        for (int i = 0; i < objs.Length; ++i)
        {
            if (objs[i] is TextureAtlas)
            {
                SetAtlas((TextureAtlas)objs[i]);
            }
        }
    }

    private void SetAtlas(TextureAtlas textureAtlas)
    {
        if (_replacementData == null)
            return;

        _replacementData.SetAtlas(textureAtlas);

        EditorUtility.SetDirty(_replacementData);
    }

    private void SetTexture(Object[] objs, int replaceIndex)
    {
        for (int i = 0; i < objs.Length; ++i)
        {
            if (objs[i] is Texture2D)
            {
                Texture2D texture = (Texture2D)objs[i];

                if (texture.width == _replacementData.textureReplacements[replaceIndex].fromTexture.width
                    &&
                    texture.height == _replacementData.textureReplacements[replaceIndex].fromTexture.height
                    )
                {
                    _replacementData.textureReplacements[replaceIndex].toTexture = texture;

                    EditorUtility.SetDirty(_replacementData);

                    VerifyTextureImportSettings(texture);
                }
                else
                {
                    this.ShowNotification(new GUIContent("The new texture does not match the dimensions of the old texture."));
                }

                return;
            }
        }
    }

    private bool VerifyTextureImportSettings(Texture2D texture)
    {
        string texturePath;

        // Re-import so we can make sure it will be ready for atlas
        // generation:
        texturePath = AssetDatabase.GetAssetPath(texture);
        // Get the texture's importer:
        TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(texturePath);
        if (!importer.isReadable
            ||
            importer.textureFormat != TextureImporterFormat.ARGB32
            ||
            importer.npotScale != TextureImporterNPOTScale.None
            ||
            importer.mipmapEnabled
            ||
            importer.wrapMode != TextureWrapMode.Clamp
            ||
            importer.filterMode != FilterMode.Bilinear
            )
        {
            // Reimport it with isReadable set to true and ARGB32:
            importer.isReadable = true;
            importer.textureFormat = TextureImporterFormat.ARGB32;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.mipmapEnabled = false;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.filterMode = FilterMode.Bilinear;
            AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceSynchronousImport);
            AssetDatabase.Refresh();

            return true;
        }

        return false;
    }

    private int VerifyOriginalAtlasTextureImportSettings(Texture2D texture)
    {
        string texturePath;

        // Re-import so we can make sure it will be ready for atlas
        // generation:
        texturePath = AssetDatabase.GetAssetPath(texture);
        // Get the texture's importer:
        TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(texturePath);
        if (!importer.isReadable
            )
        {
            // Reimport it with isReadable set to true and ARGB32:
            importer.isReadable = true;
            AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceSynchronousImport);
            AssetDatabase.Refresh();

            return importer.maxTextureSize;
        }

        return importer.maxTextureSize;
    }

    static public bool VerifyNewAtlasImportSettings(Texture2D texture, int maxAtlasSize)
    {
        string texturePath;

        // Re-import so we can make sure it will be ready for atlas
        // generation:
        texturePath = AssetDatabase.GetAssetPath(texture);

        // Get the texture's importer:
        TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(texturePath);
        if (importer.maxTextureSize != maxAtlasSize
            ||
            importer.textureFormat != TextureImporterFormat.ARGB32
            ||
            importer.npotScale != TextureImporterNPOTScale.None
            ||
            importer.mipmapEnabled
            ||
            importer.wrapMode != TextureWrapMode.Clamp
            ||
            importer.filterMode != FilterMode.Bilinear
        )
        {
            importer.maxTextureSize = maxAtlasSize;
            importer.textureFormat = TextureImporterFormat.ARGB32;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.mipmapEnabled = false;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.filterMode = FilterMode.Bilinear;
            AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceSynchronousImport);
            AssetDatabase.Refresh();

            return true;
        }

        return false;
    }

    private Vector2 UpperLeftCorner(Rect rect)
    {
        return new Vector2(rect.x, rect.y);
    }

    private void CreateAtlasTexture()
    {
        if (_replacementData == null)
        {
            this.ShowNotification(new GUIContent("Please supply replacement data", ""));
            return;
        }

        if (_replacementData.textureAtlas == null)
        {
            this.ShowNotification(new GUIContent("Please set the texture atlas for the replacement data", ""));
            return;
        }

        if (_replacementData.textureAtlas.material == null)
        {
            this.ShowNotification(new GUIContent("There is no material assigned to the selected atlas", ""));
            return;
        }

        if (_replacementData.textureAtlas.material.mainTexture == null)
        {
            this.ShowNotification(new GUIContent("There is no texture assigned to the selected atlas' material", ""));
            return;
        }

        if (string.IsNullOrEmpty(_replacementData.newTextureName))
        {
            this.ShowNotification(new GUIContent("Please supply a name for the new atlas texture", ""));
            return;
        }

        TextureAtlas atlas = _replacementData.textureAtlas;
        Texture2D originalAtlasTexture = (Texture2D)atlas.material.mainTexture;
        Texture2D newAtlasTexture = new Texture2D(originalAtlasTexture.width, originalAtlasTexture.height, TextureFormat.ARGB32 , false);

        int maxTextureSize = VerifyOriginalAtlasTextureImportSettings(originalAtlasTexture);

        string path = AssetDatabase.GetAssetPath(originalAtlasTexture);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(originalAtlasTexture)), "");
        }
        string textureFileName = path + _replacementData.newTextureName + ".png";

        Color[] pixels;
        Rect uv;
        int x, y, width, height;
        Texture2D toTexture;
        Color blankColor = new Color(0, 0, 0, 0);
        int foundReplacementIndex;

        pixels = originalAtlasTexture.GetPixels();
        for (int cIndex = 0; cIndex < pixels.Length; cIndex++)
        {
            pixels[cIndex] = blankColor;
        }
        newAtlasTexture.SetPixels(pixels);

        for (int index = 0; index < atlas.uvs.Count; index++)
        {
            foundReplacementIndex = -1;
            for (int replacementIndex = 0; replacementIndex < _replacementData.textureReplacements.Count; replacementIndex++)
            {
                if (atlas.textureGUIDs[index] == AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_replacementData.textureReplacements[replacementIndex].fromTexture)))
                {
                    foundReplacementIndex = replacementIndex;
                    break;
                }
            }

            if (foundReplacementIndex != -1)
            {
                uv = atlas.uvs[index];
                x = Mathf.FloorToInt(uv.xMin * originalAtlasTexture.width);
                y = Mathf.FloorToInt(uv.yMin * originalAtlasTexture.height);
                width = Mathf.FloorToInt(uv.width * originalAtlasTexture.width);
                height = Mathf.FloorToInt(uv.height * originalAtlasTexture.height);

                toTexture = _replacementData.textureReplacements[foundReplacementIndex].toTexture;

                if (toTexture != null)
                {
                    pixels = toTexture.GetPixels(0, 0, toTexture.width, toTexture.height);
                    newAtlasTexture.SetPixels(x, y, width, height, pixels);
                }
            }
        }

        newAtlasTexture.Apply();

        byte[] bytes;

        bytes = newAtlasTexture.EncodeToPNG();
        Directory.CreateDirectory(path);
        using (FileStream fs = File.Create(textureFileName))
        {
            fs.Write(bytes, 0, bytes.Length);
        }
        bytes = null;
        AssetDatabase.Refresh();

        newAtlasTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(textureFileName, typeof(Texture2D));
        VerifyNewAtlasImportSettings(newAtlasTexture, maxTextureSize);
        AssetDatabase.Refresh();

        AssetDatabase.SaveAssets();
    }

    #endregion
}
