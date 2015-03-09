using UnityEngine;
using UnityEditor;
using GameDataEditor;

using System.IO;

public class GDELinks : EditorWindow {
	
	private const string menuItemLocation = GDEManagerWindowBase.rootMenuLocation;

	[MenuItem(menuItemLocation + "/GDE Forum", false, GDEManagerWindowBase.menuItemStartPriority+100)]
	private static void GDEForumPost()
	{
		Application.OpenURL(GDEConstants.ForumURL);
	}

	[MenuItem(menuItemLocation + "/GDE Documentation", false, GDEManagerWindowBase.menuItemStartPriority+101)]
	private static void GDEFreeDocs()
	{
		Application.OpenURL(GDEConstants.DocURL);
	}

	[MenuItem(menuItemLocation + "/Rate Me", false, GDEManagerWindowBase.menuItemStartPriority+102)]
	private static void GDERateMe()
	{
		Application.OpenURL(GDEConstants.RateMeURL);
	}

	[MenuItem(menuItemLocation + "/Contact", false, GDEManagerWindowBase.menuItemStartPriority+103)]
	private static void GDEContact()
	{
		Application.OpenURL(GDEConstants.Contact);
	}

	[MenuItem("Assets/Game Data Editor/Load Data", true)]
	private static bool GDELoadDataValidation()
	{
		return Selection.activeObject.GetType() == typeof(TextAsset);
	}

	[MenuItem("Assets/Game Data Editor/Load Data")]
	private static void GDELoadData () 
	{
		string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
		string fullPath = Path.GetFullPath(assetPath);

		EditorPrefs.SetString(GDEConstants.DataFileKey, fullPath);
		GDEItemManager.Load(true);
	}

	[MenuItem("Assets/Game Data Editor/Load Data and Generate Data Classes")]
	private static void GDELoadAndGenData () 
	{
		GDELoadData();
		GDECodeGenWindow.DoGenerateCustomExtensions();
	}
  
  [MenuItem("Assets/Game Data Editor/Load Data and Generate Data Classes", true)]
  private static bool GDELoadAndGenDataValidation()
  {
    return GDELoadDataValidation();
  }
}

