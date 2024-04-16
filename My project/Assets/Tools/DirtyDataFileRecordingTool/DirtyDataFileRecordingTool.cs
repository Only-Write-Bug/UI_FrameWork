using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using JetBrains.Annotations;
using Util;

namespace Tools.DirtyDataFileRecordingTool
{
    public static class DirtyDataFileRecordingTool
    {
        public static void RecodingDirtyFile(string path, string[] checkType, string[] notCheckType, string xmlSavePath = null)
        {
            var tmpFiles = Directory.GetFiles(path);
            var targetFiles = new List<string>();
            
            if (checkType != null)
            {
                foreach (var file in tmpFiles)
                {
                    if(checkType.Contains(PathUtil.GetFileType(file)))
                        targetFiles.Add(file);
                }
            }
            else if(notCheckType != null)
            {
                foreach (var file in tmpFiles)
                {
                    if(!notCheckType.Contains(PathUtil.GetFileType(file)))
                        targetFiles.Add(file);
                }
            }

            //开始查看编写脏数据记录文件
            var xmlDoc = new XmlDocument();
            WriteDirtyData(xmlDoc, targetFiles);
            xmlDoc.Save(path + "DirtyDataLogger.xml");
        }

        private static void WriteDirtyData(XmlDocument xml, List<string> files)
        {
            
        }
    }
}