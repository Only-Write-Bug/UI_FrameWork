using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using JetBrains.Annotations;
using Tools.XMLTools;
using UnityEngine;
using Util;

namespace Tools
{
    public class DirtyDataFileRecordingTool
    {
        private static DirtyDataFileRecordingTool _init = null;
        public static DirtyDataFileRecordingTool Init => _init ??= new DirtyDataFileRecordingTool();

        private static DirectoryInfo _dirtyDataFileXMLDirectory = null;

        public static DirectoryInfo DirtyDataFileXMLDirectory => _dirtyDataFileXMLDirectory ??=
            Directory.CreateDirectory(XMLToolsManager.get_defaultXMLSavePath + @"\DirtyDataFileXML");

        /// <summary>
        /// 重新编码脏数据文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="checkType"></param>
        /// <param name="notCheckType"></param>
        public void RecodingDirtyFile(string path, string[] checkType, string[] notCheckType)
        {
            var savePath =
                $"{DirtyDataFileXMLDirectory.FullName}\\{PathUtil.GetSpecifiedLevelLeaf2Path(path)}_DirtyDataLogger.xml";
            
            var tmpFiles = Directory.GetFiles(path);
            var targetFiles = new List<string>();

            if (checkType != null)
            {
                foreach (var file in tmpFiles)
                {
                    if (checkType.Contains(PathUtil.GetFileType(file)))
                        targetFiles.Add(file);
                }
            }
            else if (notCheckType != null)
            {
                foreach (var file in tmpFiles)
                {
                    if (!notCheckType.Contains(PathUtil.GetFileType(file)))
                        targetFiles.Add(file);
                }
            }

            //开始查看编写脏数据记录文件
            var xmlDoc = new XmlDocument();
            RefreshDirtyData(xmlDoc, targetFiles, savePath);
            XMLUtil.FormatXML(xmlDoc);
            xmlDoc.Save(savePath);
        }

        /// <summary>
        /// 刷新脏数据
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="files"></param>
        private static void RefreshDirtyData(XmlDocument xml, List<string> files, string savePath)
        {
            
        }
    }
}