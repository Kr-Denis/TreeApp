using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Linq;

namespace TreeApp
{
    public class Tree
    {
        static readonly string[] SizeType = { "B", "KB", "MB", "GB" };

        private string _dirName;
        private int _maxDepth = int.MaxValue;
        private bool _size = false;
        private bool _humanReadable = false;
        
        public int MaxDepth { set { if (value > 0 && value <= int.MaxValue) { _maxDepth = value; } } }
        public bool Size { set { _size = value; } }
        public bool HumanReadable { set { _humanReadable = value; } }
        
        public Tree(string dirName)
        {
            _dirName = dirName;
        }

        public void Print()
        {
            Console.WriteLine(_dirName);
            PrintTree(_dirName);
        }

        //Рекурсивный метод перебора объектов
        private void PrintTree(string startDir, string prefix = "", int depth = 0)
        {
            if (depth >= _maxDepth)
            {
                return;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(startDir);
            var fsItems = directoryInfo.GetFileSystemInfos()
                .OrderBy(f => f.Name)
                .ToList();

            for (int i = 0; i < fsItems.Count; i++)
            {
                if (i != fsItems.Count - 1)
                {
                    Writeline(prefix, "├── ", fsItems[i]);
                    if (fsItems[i].Attributes == FileAttributes.Directory)
                    {
                        PrintTree(fsItems[i].FullName, prefix + "│   ", depth + 1);
                    }
                }
                else
                {
                    Writeline(prefix, "└── ", fsItems[i]);
                    if (fsItems[i].Attributes == FileAttributes.Directory)
                    {
                        PrintTree(fsItems[i].FullName, prefix + "    ", depth + 1);
                    }
                }
                
               
            }
        }

        private void Writeline(string prefix, string symbol, FileSystemInfo fsItem)
        {
            Console.Write(prefix + symbol);
            Console.Write(fsItem.Name);
            WriteSize(fsItem);
            Console.WriteLine();
        }

        private void WriteSize(FileSystemInfo fsItem)
        {
            if (_size == true)
            {
                FileInfo fileInfo = fsItem as FileInfo;
                if (fileInfo != null)
                {
                    if (_humanReadable == true)
                    {
                        Console.Write(" (" + SizeFormat(+ fileInfo.Length) + ")");
                    }
                    else
                    {
                        Console.Write(" (" + fileInfo.Length + " B)");
                    }
                }
            }
        }

        private static string SizeFormat(Int64 value, int decimalPlaces = 1)
        {
            if (value == 0) { return "empty"; } 
            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeType[i]);
        }

        public static void ShowHelp()
        {
            Console.WriteLine("ИСПОЛЬЗОВАНИЕ:");
            Console.WriteLine("     tree.exe [--help | -?] | [-d <глубина_вложенности> | --depth <глубина_вложенности>] [-s | --size [-h | --human-readable]] ");
            Console.WriteLine("Параметры:");
            Console.WriteLine("     -? или --help  Вывод данного справочного сообщения");
            Console.WriteLine("     -d или --depth Задает глубину вложенности");
            Console.WriteLine("     -s или --size  Отображение размера объектов");
            Console.WriteLine("     -h или --human-readable  Отображение размера объектов в удобном для восприятия виде");
        }
    }
}
