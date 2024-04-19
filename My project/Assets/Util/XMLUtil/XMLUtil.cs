using System;
using System.Xml;

namespace Util
{
    public static class XMLUtil
    {
        /// <summary>
        /// 将xml文件转换成数据节点形式
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static XMLDataNode ParseXML2DataNode(string xmlPath)
        {
            var root = new XMLDataRoot();

            var xml = new XmlDocument();
            xml.Load(xmlPath);
            //Todo

            return root;
        }

        /// <summary>
        /// 格式化xml文件
        /// </summary>
        /// <param name="xml"></param>
        public static void FormatXML(XmlDocument xml)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "    ", // 设置缩进的空格数
                NewLineChars = "\r\n", // 设置换行符
                NewLineHandling = NewLineHandling.Replace
            };

            var writer = XmlWriter.Create(Console.Out, settings);
            xml.Save(writer);
            writer.Close();
        }

        /// <summary>
        /// 加载xml文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool LoadXML(string path)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                return true;
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine($"{path}不存在，请检查文件路径。");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载 {path} 文件时出现错误：" + ex.Message);
            }

            return false;
        }
    }
}