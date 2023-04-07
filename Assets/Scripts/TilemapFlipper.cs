using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

//[CustomEditor(typeof(Tilemap))]
//public class LevelScriptEditor : Editor
//{
//	private Tilemap tilemapReference;

//	public override void OnInspectorGUI()
//	{
//		base.OnInspectorGUI();

//		var tilemap = (Tilemap)target;

//		tilemapReference = EditorGUILayout.ObjectField(
//			"Tilemap",
//			tilemapReference,
//			typeof(Tilemap),
//			true
//		) as Tilemap;

//		if (GUILayout.Button("Flip"))
//		{
//			for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
//			{
//				for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
//				{
//					var mirroredPosition = new Vector3Int(x, -y, 0);
//					var tile = tilemap.GetTile(new Vector3Int(x, y, 0));//.GetTile(tilemap.WorldToCell(mirroredPosition));
//					tilemapReference.SetTile(mirroredPosition, tile);
//				}
//			}
//		}
//	}
//}
