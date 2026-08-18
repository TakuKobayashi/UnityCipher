using System.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using System.Diagnostics;

public class UnityPackageExporter
{
    public const string DefaultBuildIncludeRootPath = "Assets/UnityCipher";
    public const string DefaultExportUnityPackageFilePath = "UnityCipher.unitypackage";

    public static void ExportUnityPackageFromCommand()
    {
        string exportFilePath = Environment.GetEnvironmentVariable("UNITY_PACKAGE_OUTPUT_PATH");
        if (string.IsNullOrEmpty(exportFilePath))
        {
            exportFilePath = DefaultExportUnityPackageFilePath;
        }

        if (!AssetDatabase.IsValidFolder(DefaultBuildIncludeRootPath))
        {
            throw new DirectoryNotFoundException(
                "Unity package source directory was not found: " + DefaultBuildIncludeRootPath
            );
        }

        string absoluteExportFilePath = Path.GetFullPath(exportFilePath);
        string exportDirectory = Path.GetDirectoryName(absoluteExportFilePath);
        if (!string.IsNullOrEmpty(exportDirectory))
        {
            Directory.CreateDirectory(exportDirectory);
        }

        // Export only the installable library. Examples, tests, and this editor
        // tooling live outside Assets/UnityCipher and are intentionally excluded.
        AssetDatabase.ExportPackage(
            DefaultBuildIncludeRootPath,
            absoluteExportFilePath,
            ExportPackageOptions.Recurse
        );

        if (!File.Exists(absoluteExportFilePath))
        {
            throw new InvalidOperationException(
                "Unity did not create the expected package: " + absoluteExportFilePath
            );
        }

        UnityEngine.Debug.Log("Exported Unity package: " + absoluteExportFilePath);
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

    public static void ExportUnityPackage(string buildIncludeRootDirPath, string exportFilePath)
    {
        List<string> buildPathes = UnityPackageExporter.FindFilePathes(buildIncludeRootDirPath);
        AssetDatabase.ExportPackage(buildPathes.ToArray(), exportFilePath, ExportPackageOptions.Recurse);
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
