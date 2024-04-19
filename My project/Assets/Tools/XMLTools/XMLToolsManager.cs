using System.IO;


namespace Tools.XMLTools
{
    public static class XMLToolsManager
    {
        private static string _defaultXMLSavePath = null;

        public static string get_defaultXMLSavePath => _defaultXMLSavePath ??= Directory.GetCurrentDirectory() + @"\xml";

        /// <summary>
        /// 刷新指定目录脏数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="checkType"></param>
        /// <param name="notCheckType"></param>
        public static void RefreshSpecifyDirectoryDirtyData(string path, string[] checkType, string[] notCheckType)
        {
           DirtyDataFileRecordingTool.Init.RecodingDirtyFile(path, checkType, notCheckType);
        }
    }
}