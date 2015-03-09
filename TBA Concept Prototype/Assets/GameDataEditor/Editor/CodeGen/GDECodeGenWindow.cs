using UnityEditor;
using System;
using GameDataEditor;

public class GDECodeGenWindow : EditorWindow
{
    private const string menuItemLocation = GDEManagerWindowBase.rootMenuLocation + "/Generate Custom Extensions";

    [MenuItem(menuItemLocation, false, GDEManagerWindowBase.menuItemStartPriority+54)]
    public static void DoGenerateCustomExtensions()
    {
        GDEItemManager.Load();

		GDECodeGen.GenStaticKeysClass(GDEItemManager.AllSchemas);
        GDECodeGen.GenClasses(GDEItemManager.AllSchemas);
    }
}

