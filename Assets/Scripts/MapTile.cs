using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class MapTile : TileBase
{
    public GameObject Prefab;
    public bool Walkable;

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		tileData.gameObject = Prefab;
		if (Prefab == null) return;


#if UNITY_EDITOR
		var texture = AssetPreview.GetAssetPreview(Prefab);
		if (texture == null) return;
		tileData.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
#else
		return;
#endif
	}
}
