using System;
using System.IO;
using System.Collections.Specialized;
//using FsTree;

namespace TreeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int depth = 0;
            bool error = false;

            string rootDir = Directory.GetCurrentDirectory();
            Tree tree = new Tree(rootDir);

            for (int i = 0; i < args.Length; i++)
            {   
                if (args[i] == "--help" || args[i] == "-?")
                {
                    Tree.ShowHelp();
                    error = true;
                    break;
                }

                if (args[i] == "--depth" || args[i] == "-d")
                {
                    if (i + 1 >= args.Length)
                    {
                        Console.WriteLine("Нет глубины вложенности!");
                        error = true;
                        break;
                    }
                    else
                    {
                        if (!int.TryParse(args[i + 1], out depth))
                        {

                            Console.WriteLine("Глубина вложенности должна быть натуральным числом!");
                            error = true;
                            break;
                        }
                        else
                        {
                            if (depth < 0)
                            {
                                Console.WriteLine("Глубина вложенности должна быть натуральным числом!");
                                error = true;
                                break;
                            }
                            tree.MaxDepth = depth;
                        }
                    }
                }

                if (args[i] == "--size" || args[i] == "-s")
                {
                    tree.Size = true;
                    if (i + 1 < args.Length)
                    {
                        if (args[i + 1] == "--human-readable" || args[i + 1] == "-h")
                        {
                            tree.HumanReadable = true;
                        }
                    }
                }
            }

            if (error == false)
            {
                tree.Print();
            }

        }
    }
}

