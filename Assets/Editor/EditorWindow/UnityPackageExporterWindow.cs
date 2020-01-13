using UnityEngine;
using UnityEditor.Compilation;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class UnityPackageExporterWindow : EditorWindow
{
    private string buildIncludeRootPath = UnityPackageExporter.DefaultBuildIncludeRootPath;
    private string exportUnityPackageFilePath = UnityPackageExporter.DefaultExportUnityPackageFilePath;

    [MenuItem("Tools/ExportUnityPackage")]
    static void Open()
    {
        // メニューのWindow/EditorExを選択するとOpen()が呼ばれる。
        // 表示させたいウィンドウは基本的にGetWindow()で表示＆取得する。
        EditorWindow.GetWindow<UnityPackageExporterWindow>("ExportUnityPackage");
    }

    // オブジェクトがロードされたとき、この関数は呼び出されます。
    void OnEnable()
    {
    }

    // Windowのクライアント領域のGUI処理を記述
    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Include Unity Package Root Path");
        buildIncludeRootPath = (string)EditorGUILayout.TextField(buildIncludeRootPath);
        UnityEngine.Object buildIncludeRoot = AssetDatabase.LoadAssetAtPath(buildIncludeRootPath, typeof(UnityEngine.Object));
        buildIncludeRoot = EditorGUILayout.ObjectField(buildIncludeRoot, typeof(UnityEngine.Object), false);
        if (buildIncludeRoot != null)
        {
            buildIncludeRootPath = AssetDatabase.GetAssetPath(buildIncludeRoot);
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Export Unity Package Path");
        exportUnityPackageFilePath = (string)EditorGUILayout.TextField(exportUnityPackageFilePath);
        GUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("Export Unity Package")))
        {
            List<string> buildPathes = UnityPackageExporter.FindFilePathes(buildIncludeRootPath);
            UnityPackageExporter.ExportUnityPackage(
                buildIncludeRootPathes: buildPathes.ToArray(),
                exportFilePath: exportUnityPackageFilePath
            );
            System.Diagnostics.Process.Start(UnityPackageExporter.FileRootPath(exportUnityPackageFilePath));
        }
        EditorGUILayout.EndHorizontal();
    }
}
