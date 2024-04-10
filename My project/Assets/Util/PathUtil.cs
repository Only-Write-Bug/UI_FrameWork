using System.Text;
using UnityEngine;

namespace Util
{
    public static class PathUtil
    {
        /// <summary>
        /// 获取路径指定层级叶子
        /// </summary>
        /// <returns></returns>
        public static string GetSpecifiedLevelLeaf2Path(string path, int levelNum = 1)
        {
            string leaf = null;
            var levelFlag = 0;
            for (var i = path.Length - 1; i > 0; i--)
            {
                if (path[i] != '\\')
                    continue;
                else
                {
                    levelFlag++;
                    if (levelFlag != levelNum) continue;
                    leaf = path.Substring(i + 1, path.Length - i - 1);
                    break;
                }
            }

            return leaf;
        }
    }
}