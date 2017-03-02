using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TEditor.BusinessLogic
{
    public class FileManager: IFileManager
    {
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);

        public string GetContent(string filePath)
        {
            return GetContent(filePath, _defaultEncoding);
        }

        public string GetContent(string filePath, Encoding encoding)
        {
            string str = File.ReadAllText(filePath, encoding);
            return str;
        }

        public void SaveContent(string content, string filePath)
        {

                SaveContent(content, filePath, _defaultEncoding);
           
        }

        public void SaveContent(string content, string filePath, Encoding encoding)
        {
            File.WriteAllText(filePath, content);
        }

        public int GetSymbolCount(string content)
        {
            return content.Length;
        }

        public bool IsExist(string filePath)
        {
            return File.Exists(filePath);
        }
        
    }
}
