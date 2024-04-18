using System.IO;
using Tools.XMLTools;
using UnityEditor;
using UnityEngine;
using Util;

namespace UIFrameWork
{
    public static class UIFrameToolMethods
    {
        private static readonly string[] checkType = new[] { "prefab" };

        private static string _prefabsPath = null;
        private static string _uiFrameWorkRootPath = null;
        private static string _modelsPath = null;
        private static string _viewModelPath = null;

        [MenuItem("UIFrameWork/ExportUI")]
        public static void ExportUI()
        {
            CheckUIDirectories();
            XMLToolsManager.RefreshSpecifyDirectoryDirtyData(_prefabsPath, checkType, null);
        }

        /// <summary>
        /// 检查UI相关文件夹
        /// </summary>
        private static void CheckUIDirectories()
        {
            var rootPath = Directory.GetDirectories(Directory.GetCurrentDirectory() + "/Assets");
            foreach (var path in rootPath)
            {
                switch (PathUtil.GetSpecifiedLevelLeaf2Path(path))
                {
                    case "Prefabs":
                        _prefabsPath = path;
                        break;
                    case "UIFrameWork":
                        _uiFrameWorkRootPath = path;
                        break;
                    default:
                        break;
                }
            }

            if (_prefabsPath == null)
                Debug.LogError("UIFrameWork :: Export is Error :: Prefabs Directory is not found");
            if (_uiFrameWorkRootPath != null)
            {
                CreateUIDirectories();
                return;
            }

            Debug.LogError("UIFrameWork :: Export is Error :: _uiFrameWorkRootPath Directory is not found");
        }

        /// <summary>
        /// 检查UI结构层的文件夹
        /// </summary>
        private static void CreateUIDirectories()
        {
            if (_uiFrameWorkRootPath == null)
                return;

            _modelsPath ??= Directory.CreateDirectory(_uiFrameWorkRootPath + "\\Models").FullName;
            _viewModelPath ??= Directory.CreateDirectory(_uiFrameWorkRootPath + "\\ViewModels").FullName;
        }
        
        
    }
}