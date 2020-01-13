using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;
using System.Diagnostics;
using System.Reflection;

public class UnityPackageExporter
{
    public const string DefaultBuildIncludeRootPath = "Assets/UnityCipher";
    public const string DefaultExportUnityPackageFilePath = "UnityCipher.unitypackage";

    public static void ExportUnityPackageFromCommand()
    {
        ExportUnityPackageRoutine(DefaultBuildIncludeRootPath, DefaultExportUnityPackageFilePath);
    }

    public static void ExportUnityPackageRoutine(
        string buildIncludeRootPath,
        string exportFilePath
    )
    {
        List<string> buildPathes = FindFilePathes(buildIncludeRootPath);
        ExportUnityPackage(buildPathes.ToArray(), exportFilePath);
        string[] pathCells = exportFilePath.Split("/".ToCharArray());
        pathCells[pathCells.Length - 1] = "";
        // 保存先フォルダを開く
        Process.Start(string.Join("/", pathCells));
    }

    public static void ExportUnityPackage(string[] buildIncludeRootPathes, string exportFilePath)
    {
        AssetDatabase.ExportPackage(buildIncludeRootPathes, exportFilePath, ExportPackageOptions.Recurse);
    }

    public static List<string> FindFilePathes(string filterName, string extFileName = "")
    {
        List<string> seachedFilePathes = new List<string>();
        string[] pathes = AssetDatabase.GetAllAssetPaths();
        for (int i = 0; i < pathes.Length; ++i)
        {
            string path = pathes[i];
            Match match = Regex.Match(path.ToLower(), @"" + filterName.ToLower() + ".+" + extFileName);
            if (match.Success)
            {
                seachedFilePathes.Add(path);
            }
        }
        return seachedFilePathes;
    }

    public static string FileRootPath(string filePath)
    {
        string[] pathCells = filePath.Split("/".ToCharArray());
        pathCells[pathCells.Length - 1] = "";
        return string.Join("/", pathCells);
    }
}
