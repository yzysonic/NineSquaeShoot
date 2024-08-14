using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLoader : MonoBehaviour
{
    public static AddressableLoader Instance;

    public enum GetSpriteType { Weapon, Character, CharacterIcon };

    private Dictionary<string, Sprite> CharacterSpriteDic;
    private Dictionary<string, Sprite> CharacterIconSpriteDic;
    private Dictionary<string, Sprite> WeaponSpriteDic;
    private Dictionary<string, GameObject> CharacterPrefabDic;
    private Dictionary<AsyncOperationHandle, string> LoadAssetDic;

    Action AssetLoaded;

    private List<AsyncOperationHandle> OperationHandleList;

    void Awake() {
        Instance = this;
        OperationHandleList = new List<AsyncOperationHandle>();
        CharacterSpriteDic = new Dictionary<string, Sprite>();
        CharacterIconSpriteDic = new Dictionary<string, Sprite>();
        WeaponSpriteDic = new Dictionary<string, Sprite>();
        CharacterPrefabDic = new Dictionary<string, GameObject>();
        LoadAssetDic = new Dictionary<AsyncOperationHandle, string>();
    }

    // Start is called before the first frame update
    void Start() {
        LoadAssets();
    }

    // Update is called once per frame
    void Update() {

    }

    public Sprite GetSprite(GetSpriteType Type, string SpriteName) {
        switch (Type) {
            case GetSpriteType.Weapon:
                if (WeaponSpriteDic.ContainsKey(SpriteName)) {
                    return WeaponSpriteDic[SpriteName];
                }
                return null;

            case GetSpriteType.Character:
                if (CharacterSpriteDic.ContainsKey(SpriteName)) {
                    return CharacterSpriteDic[SpriteName];
                }
                return null;

            case GetSpriteType.CharacterIcon:
                if (CharacterIconSpriteDic.ContainsKey(SpriteName)) {
                    return CharacterIconSpriteDic[SpriteName];
                }
                return null;
        }
        return null;
    }

    void LoadAssets() {
        AsyncOperationHandle<IList<Texture2D>> LoadWeaponTexture = Addressables.LoadAssetsAsync<Texture2D>("Weapon", null);
        AsyncOperationHandle<IList<Texture2D>> LoadCharacterTexture = Addressables.LoadAssetsAsync<Texture2D>("Character", null);
        AsyncOperationHandle<IList<Texture2D>> LoadCharacterIconTexture = Addressables.LoadAssetsAsync<Texture2D>("CharacterIcon", null);
        AsyncOperationHandle<IList<GameObject>> LoadCharacterPrefab = Addressables.LoadAssetsAsync<GameObject>("CharacterPrefab", null);
        OperationHandleList.Add(LoadWeaponTexture);
        OperationHandleList.Add(LoadCharacterTexture);
        OperationHandleList.Add(LoadCharacterIconTexture);
        OperationHandleList.Add(LoadCharacterPrefab);
        LoadAssetDic.Add(LoadWeaponTexture, "Weapon");
        LoadAssetDic.Add(LoadCharacterTexture, "Character");
        LoadAssetDic.Add(LoadCharacterIconTexture, "CharacterIcon");
        LoadAssetDic.Add(LoadCharacterPrefab, "CharacterPrefab");
        LoadWeaponTexture.Completed += OnTextureLoaded;
        LoadCharacterTexture.Completed += OnTextureLoaded;
        LoadCharacterIconTexture.Completed += OnTextureLoaded;
        LoadCharacterPrefab.Completed += OnGameObjectLoaded;
    }

    void OnTextureLoaded(AsyncOperationHandle<IList<Texture2D>> obj) {
        CheckLoadProgress();
        if (LoadAssetDic.ContainsKey(obj)) {
            switch (LoadAssetDic[obj]) {
                case "Weapon":
                    foreach (var texture in obj.Result) {
                        Sprite WeaponSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
                        WeaponSprite.name = texture.name;
                        WeaponSpriteDic.Add(WeaponSprite.name, WeaponSprite);
                    }
                    break;

                case "Character":
                    foreach (var texture in obj.Result) {
                        Sprite CharacterSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
                        CharacterSprite.name = texture.name;
                        CharacterSpriteDic.Add(CharacterSprite.name, CharacterSprite);
                    }
                    break;

                case "CharacterIcon":
                    foreach (var texture in obj.Result) {
                        Sprite CharacterIconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
                        CharacterIconSprite.name = texture.name;
                        CharacterIconSpriteDic.Add(CharacterIconSprite.name, CharacterIconSprite);
                    }
                    break;
            }
        }
    }

    void OnGameObjectLoaded(AsyncOperationHandle<IList<GameObject>> obj) {
        CheckLoadProgress();
    }

    void CheckLoadProgress() {
        if (OperationHandleList.All(handles => handles.IsDone)) {
            AssetLoaded();
        }
    }

    public void RegisterAssetLoaded(Action callback) {
        AssetLoaded += callback;
    }

    public void UnregisterAssetLoaded(Action callback) {
        AssetLoaded -= callback;
    }
}