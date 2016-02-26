using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TLSharp.Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(@"Usage: TLSharp.Compiler <C:\path\to\scheme.tl> (C:\path\to\compiled.src)");
                return;
            }

            if (!File.Exists(args[0])) // if the file doesn't exist,
                args[0] = Path.Combine( // convert it from relative -> full path
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), args[0]);

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File {args[0]} doesn't exist");
                return;
            }

            Console.WriteLine("Tokenizing...");
            var all = new Tokenizer().Tokenize(File.ReadAllText(args[0]));
            Console.WriteLine("Tokenized");

            var compiler = new CSharpCompiler { Namespace = "TLSharp.Core.MTProto" };
            if (args.Length > 1)
            {
                Console.WriteLine("Compiling...");
                File.WriteAllText(args[1], compiler.GetTLObjectsCode(all, 0));
                Console.WriteLine("Compiled");
                return;
            }

            var current = new List<TLObject>(all);
            var search = new StringBuilder();
            int i = 0;
            while (i > -1)
            {
                Console.Clear();

                if (current.Count == 0)
                {
                    Console.WriteLine("No methods with the search: " + search.ToString());
                }
                else
                {
                    if (i >= current.Count)
                        i = 0;
                    
                    Console.WriteLine(compiler.GetTLObjectCode(current[i], 0));
                    Console.WriteLine(new string('_', Console.WindowWidth));
                    var method = $"[Method {(i + 1)}/{current.Count}]";
                    Console.WriteLine(new string(' ', Console.WindowWidth / 2 - method.Length / 2) + method);
                    Console.Write("Left/right: move. ESC: quit. Type to search: " + search.ToString());
                }

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:
                        if (--i < 0)
                            i = current.Count - 1;
                        break;
                        
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.RightArrow:
                        if (++i == current.Count)
                            i = 0;
                        break;

                    case ConsoleKey.Escape:
                        return;

                    case ConsoleKey.Backspace:
                        if (search.Length > 0)
                        {
                            --search.Length;
                            current = new List<TLObject>(all.Where(o => o.Name.IndexOf(search.ToString(), StringComparison.OrdinalIgnoreCase) > -1));
                        }

                        break;
                    default:
                        search.Append(key.KeyChar);
                        current = new List<TLObject>(all.Where(o => o.Name.IndexOf(search.ToString(), StringComparison.OrdinalIgnoreCase) > -1));
                        break;
                }
            }
        }
    }
}
