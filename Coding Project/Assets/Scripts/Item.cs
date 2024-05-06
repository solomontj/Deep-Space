using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject {

    [Header("Only gameplay")]
    public TileBase tile;
    public ItemType type;
    public string itemName;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5,4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;

}

public enum ItemType {
    BuildingBlock,
    Tool
}

public enum ActionType {
    Dig,
    Mine,
    Light,
    Key,
    Map,
    Charge,
    Encrypted,
    Decrypted
}