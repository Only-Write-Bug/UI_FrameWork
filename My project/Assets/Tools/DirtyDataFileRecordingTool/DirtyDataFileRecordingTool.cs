using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using JetBrains.Annotations;
using Util;

namespace Tools.DirtyDataFileRecordingTool
{
    public static class DirtyDataFileRecordingTool
    {
        public static void RecodingDirtyFile(string path,
            [CanBeNull] string[] checkType,
            [CanBeNull] string[] notCheckType)
        {
            var tmpFiles = Directory.GetFiles(path);
            var targetFiles = new List<string>();
            
            if (checkType != null)
            {
                foreach (var file in tmpFiles)
                {
                    if(checkType.Equals(PathUtil.GetFileType(file)))
                        targetFiles.Add(file);
                }
            }
            else if(notCheckType != null)
            {
                foreach (var file in tmpFiles)
                {
                    if(!notCheckType.Equals(PathUtil.GetFileType(file)))
                        targetFiles.Add(file);
                }
            }

            //开始查看编写脏数据记录文件
            var xmlDoc = new XmlDocument();
            var targetXml = Directory.GetFiles("DirtyDataLogger.xml");
            if (targetXml.Length > 0)
                xmlDoc.Load(targetXml[0]);
            else
                xmlDoc.Save(path + "DirtyDataLogger.xml");
                
        }
    }
}