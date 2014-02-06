// --------------------------------------------------------------------------------------------- //
// (c) partial pi 2012-2014
// Creative Commons Attribution-NonCommercial 4.0 International (CC BY-NC 4.0)
// http://creativecommons.org/licenses/by-nc/4.0/deed.en_US
// --------------------------------------------------------------------------------------------- //

using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using pi.unity.util; 
using pi.unity.spriteskeleton;

/// <summary>
/// Editor window used to edit the skins 
/// </summary>
[CanEditMultipleObjects]
public class SpriteSkinEditor : EditorWindow
{	
	[MenuItem("Window/Sprite skin editor")]
	public static void Init()
	{
		EditorWindow w = EditorWindow.GetWindow (typeof (SpriteSkinEditor));
	
		Assert.NotNull( w, "Cannot create Sprite skind editor window.");
	}

	/// <summary>
	/// Current selected sprite skin
	/// </summary>
	private SpriteSkeletonSkinAsset _selectedSpriteSkin;

	/// <summary>
	/// Current editor
	/// </summary>
	private Editor	   _editor;

	/// <summary>
	/// Used so we can scroll in the editor.
	/// </summary>
	private Vector2	   _scrollPosition;

	/// <summary>
	/// Notification from unity the selection has changed.
	/// </summary>
	public void OnSelectionChange()
	{
		if ( Selection.activeObject != null )
		{
			if ( Selection.activeObject is SpriteSkeletonSkinAsset )
			{
				_selectedSpriteSkin = Selection.activeObject as SpriteSkeletonSkinAsset;
				_editor = Editor.CreateEditor( _selectedSpriteSkin );
			}
		}

		Repaint();
	}
	
	public void OnGUI ()
	{
		if ( Selection.activeObject != null && _editor != null )
		{
			// remind myself what skin I selected
			GUILayout.Label( "Skin:       " + _selectedSpriteSkin.name );

			if ( Selection.activeObject is GameObject
			    && ((GameObject) Selection.activeObject).FindComponentByType<Animator>() != null )
			{
				GameObject skeletonRoot =
					((GameObject) Selection.activeObject).FindComponentByType<Animator>().gameObject;

				// remind myself what gameobject has been selected
				GUILayout.Label( "Skeleton: " + Selection.activeObject.name );

				if ( GUILayout.Button( "create sprite skin from selected gameobject" )  )
				{
					_selectedSpriteSkin.CreateFromSkeleton( skeletonRoot );
				}

				if ( GUILayout.Button( "merge sprite skin with selected gameobject" )  )
				{
					_selectedSpriteSkin.Merge( skeletonRoot );
				}

				if ( GUILayout.Button( "apply random sprite skin to selected gameobject" )  )
				{
					_selectedSpriteSkin.ApplyRandom( skeletonRoot, UnityEngine.Random.Range( 0, int.MaxValue ) );
				}
			}
			else
			{
				GUILayout.Label( "No skeleton selected."  );
			}
		}

		if ( _editor != null )
		{
			_scrollPosition = EditorGUILayout.BeginScrollView( _scrollPosition );
				_editor.OnInspectorGUI();
			EditorGUILayout.EndScrollView();
		}
	}
}