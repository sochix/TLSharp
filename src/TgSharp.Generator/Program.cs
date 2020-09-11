using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TgSharp.Generator.Models;

namespace TgSharp.Generator
{
    class Program
    {
        static List<string> keywords = new List<string>(new string[] { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "add", "alias", "ascending", "async", "await", "descending", "dynamic", "from", "get", "global", "group", "into", "join", "let", "orderby", "partial", "partial", "remove", "select", "set", "value", "var", "where", "where", "yield" });
        static List<string> interfacesList = new List<string>();
        static List<string> classesList = new List<string>();

        static string constructorAbsTemplate = "ConstructorAbs.tmp";
        static string constructorTemplate = "Constructor.tmp";
        static string methodTemplate = "Method.tmp";

        static string rootNamespace = "TgSharp";

        static List<string> templateFiles = new List<string> (new [] {
            constructorAbsTemplate,
            constructorTemplate,
            methodTemplate,
        });

        static bool IsRoot(DirectoryInfo dir)
        {
            return dir.FullName == dir.Root.FullName;
        }

        static DirectoryInfo FindTemplatesDir(DirectoryInfo dir)
        {
            if (templateFiles.All(templateFileName =>
                new FileInfo(Path.Combine(dir.FullName, templateFileName))
                    .Exists)) {
                return dir;
            }

            if (IsRoot(dir))
                return null;

            var folderUp = new DirectoryInfo(Path.Combine(dir.FullName, ".."));
            return FindTemplatesDir(folderUp);
        }

        static void Main(string[] args)
        {
            var currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            var possibleTemplatesDir = FindTemplatesDir(currentDir);
            if (possibleTemplatesDir == null) {
                var assDir =
                    System.Reflection.Assembly.GetExecutingAssembly().Location;
                possibleTemplatesDir = FindTemplatesDir(new DirectoryInfo(assDir));
            }
            if (possibleTemplatesDir == null) {
                Console.Error.WriteLine("Couldn't find template *.tmp files");
                Environment.Exit(1);
            }

            string absStyle = File.ReadAllText(
                                  Path.Combine(possibleTemplatesDir.FullName,
                                               constructorAbsTemplate)
                              );
            string normalStyle = File.ReadAllText(
                                     Path.Combine (possibleTemplatesDir.FullName,
                                                   constructorTemplate)
                                 );
            string methodStyle = File.ReadAllText (
                                     Path.Combine (possibleTemplatesDir.FullName,
                                                   methodTemplate)
                                 );

            string Json = "";

            string url;
            if (!args.Any())
                url = "json";
            else
                url = args[0];

            try
            {
                Json = File.ReadAllText(url);
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception ("Couldn't find schema JSON file, did you download it first e.g. with `wget https://core.telegram.org/schema/json`?", ex);
            }
            FileStream file = File.OpenWrite("Result.cs");
            StreamWriter sw = new StreamWriter(file);
            Schema schema = JsonConvert.DeserializeObject<Schema>(Json);
            foreach (var c in schema.Constructors)
            {
                interfacesList.Add(c.Type);
                classesList.Add(c.Predicate);
            }
            foreach (var c in schema.Constructors)
            {
                var list = schema.Constructors.Where(x => x.Type == c.Type);
                if (list.Count() > 1)
                {
                    string path =
                        (GetNameSpace(c.Type)
                            .Replace(rootNamespace + ".TL", "TL" + Path.DirectorySeparatorChar)
                            .Replace(".", "") + Path.DirectorySeparatorChar +
                            GetNameofClass(c.Type, true) + ".cs")
                                .Replace("\\\\", Path.DirectorySeparatorChar.ToString());
                    FileStream classFile = MakeFile(path);
                    using (StreamWriter writer = new StreamWriter(classFile))
                    {
                        string nspace =
                            (GetNameSpace(c.Type)
                                .Replace(rootNamespace + ".TL", "TL" + Path.DirectorySeparatorChar)
                                .Replace(".", ""))
                            .Replace("\\\\", Path.DirectorySeparatorChar.ToString())
                            .Replace(Path.DirectorySeparatorChar, '.');
                        if (nspace.EndsWith("."))
                            nspace = nspace.Remove(nspace.Length - 1, 1);
                        string temp = absStyle.Replace("/* NAMESPACE */", rootNamespace + "." + nspace);
                        temp = temp.Replace("/* NAME */", GetNameofClass(c.Type, true));
                        writer.Write(temp);
                        writer.Close();
                        classFile.Close();
                    }
                }
                else
                {
                    interfacesList.Remove(list.First().Type);
                    list.First().Type = "himself";
                }
            }
            foreach (var c in schema.Constructors)
            {
                string path =
                    (GetNameSpace(c.Predicate)
                        .Replace(rootNamespace + ".TL", "TL" + Path.DirectorySeparatorChar)
                        .Replace(".", "") + Path.DirectorySeparatorChar +
                        GetNameofClass(c.Predicate, false) + ".cs")
                    .Replace("\\\\", Path.DirectorySeparatorChar.ToString());
                FileStream classFile = MakeFile(path);
                using (StreamWriter writer = new StreamWriter(classFile))
                {
                    #region About Class
                    string nspace =
                        (GetNameSpace(c.Predicate)
                            .Replace(rootNamespace + ".TL", "TL" + Path.DirectorySeparatorChar)
                            .Replace(".", ""))
                        .Replace("\\\\", Path.DirectorySeparatorChar.ToString())
                        .Replace(Path.DirectorySeparatorChar, '.');
                    if (nspace.EndsWith("."))
                        nspace = nspace.Remove(nspace.Length - 1, 1);
                    string temp = normalStyle.Replace("/* NAMESPACE */", rootNamespace + "." + nspace);
                    temp = (c.Type == "himself") ? temp.Replace("/* PARENT */", "TLObject") : temp.Replace("/* PARENT */", GetNameofClass(c.Type, true));
                    temp = temp.Replace("/*Constructor*/", c.Id.ToString());
                    temp = temp.Replace("/* NAME */", GetNameofClass(c.Predicate, false));
                    #endregion
                    #region Fields
                    string fields = String.Empty;
                    bool first = true;
                    foreach (var tmp in c.Params)
                    {
                        if (!first) {
                            fields += Environment.NewLine + "        ";
                        } else {
                            first = false;
                        }
                        fields += $"public {CheckForFlagBase (tmp.Type, GetTypeName (tmp.Type))} {CheckForKeywordAndPascalCase (tmp.Name)}" + " { get; set; }";
                    }
                    if (fields == String.Empty)
                        fields = "// no fields";
                    temp = temp.Replace("/* PARAMS */", fields);
                    #endregion
                    #region ComputeFlagFunc
                    if (!c.Params.Any(x => x.Name == "Flags")) temp = temp.Replace("/* COMPUTE */", "// do nothing");
                    else
                    {
                        var compute = "Flags = 0;" + Environment.NewLine;
                        foreach (var param in c.Params.Where(x => IsFlagBase(x.Type)))
                        {
                            if (IsTrueFlag(param.Type))
                            {
                                compute += $"Flags = {CheckForKeywordAndPascalCase(param.Name)} ? (Flags | {GetBitMask(param.Type)}) : (Flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                            else
                            {
                                compute += $"Flags = {CheckForKeywordAndPascalCase(param.Name)} != null ? (Flags | {GetBitMask(param.Type)}) : (Flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                        }
                        temp = temp.Replace("/* COMPUTE */", compute);
                    }
                    #endregion
                    #region SerializeFunc
                    var serialize = String.Empty;
                    first = true;
                    if (c.Params.Any (x => x.Name == "Flags")) {
                        serialize += "ComputeFlags();" +
                                    Environment.NewLine + "            " +
                                     "bw.Write(Flags);";
                        first = false;
                    }
                    foreach (var p in c.Params.Where(x => x.Name != "Flags"))
                    {
                        var code = WriteWriteCode (p);
                        if (String.IsNullOrEmpty(code))
                            continue;

                        if (!first) {
                            serialize += Environment.NewLine + "            ";
                        } else {
                            first = false;
                        }
                        serialize += code;
                    }
                    if (serialize == String.Empty)
                        serialize = "// do nothing";
                    temp = temp.Replace("/* SERIALIZE */", serialize);
                    #endregion
                    #region DeSerializeFunc
                    var deserialize = String.Empty;
                    first = true;
                    foreach (var p in c.Params)
                    {
                        if (!first) {
                            deserialize += Environment.NewLine + "            ";
                        } else {
                            first = false;
                        }
                        deserialize += WriteReadCode(p);
                    }
                    if (deserialize == String.Empty)
                        deserialize = "// do nothing";
                    temp = temp.Replace("/* DESERIALIZE */", deserialize);
                    #endregion
                    writer.Write(temp);
                    writer.Close();
                    classFile.Close();
                }
            }
            foreach (var c in schema.Methods)
            {
                string path =
                    (GetNameSpace(c.Method)
                        .Replace(rootNamespace + ".TL", "TL" + Path.DirectorySeparatorChar)
                        .Replace(".", "") + Path.DirectorySeparatorChar +
                        GetNameofClass(c.Method, false, true) + ".cs")
                            .Replace("\\\\", Path.DirectorySeparatorChar.ToString());
                FileStream classFile = MakeFile(path);
                using (StreamWriter writer = new StreamWriter(classFile))
                {
                    #region About Class
                    string nspace =
                        (GetNameSpace(c.Method)
                            .Replace(rootNamespace + ".TL", "TL" + Path.DirectorySeparatorChar)
                            .Replace(".", ""))
                        .Replace("\\\\", Path.DirectorySeparatorChar.ToString())
                        .Replace(Path.DirectorySeparatorChar, '.');
                    if (nspace.EndsWith("."))
                        nspace = nspace.Remove(nspace.Length - 1, 1);
                    string temp = methodStyle.Replace("/* NAMESPACE */", rootNamespace + "." + nspace);
                    temp = temp.Replace("/* PARENT */", "TLMethod");
                    temp = temp.Replace("/*Constructor*/", c.Id.ToString());
                    temp = temp.Replace("/* NAME */", GetNameofClass(c.Method, false, true));
                    #endregion
                    #region Fields
                    string fields = "";
                    bool first = true;
                    foreach (var tmp in c.Params)
                    {
                        if (!first) {
                            fields += Environment.NewLine + "        ";
                        } else {
                            first = false;
                        }
                        fields += $"public {CheckForFlagBase(tmp.Type, GetTypeName(tmp.Type))} {CheckForKeywordAndPascalCase(tmp.Name)}" + " { get; set; }";
                    }
                    if (!first) {
                        fields += Environment.NewLine + "        ";
                    } else {
                        first = false;
                    }
                    fields += $"public {CheckForFlagBase(c.Type, GetTypeName(c.Type))} Response" + " { get; set; }";
                    temp = temp.Replace("/* PARAMS */", fields);
                    #endregion
                    #region ComputeFlagFunc
                    if (!c.Params.Any(x => x.Name == "Flags")) temp = temp.Replace("/* COMPUTE */", "// do nothing");
                    else
                    {
                        var compute = "Flags = 0;" + Environment.NewLine;
                        foreach (var param in c.Params.Where(x => IsFlagBase(x.Type)))
                        {
                            if (IsTrueFlag(param.Type))
                            {
                                compute += $"Flags = {CheckForKeywordAndPascalCase(param.Name)} ? (Flags | {GetBitMask(param.Type)}) : (Flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                            else
                            {
                                compute += $"Flags = {CheckForKeywordAndPascalCase(param.Name)} != null ? (Flags | {GetBitMask(param.Type)}) : (Flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                        }
                        temp = temp.Replace("/* COMPUTE */", compute);
                    }
                    #endregion
                    #region SerializeFunc
                    var serialize = String.Empty;
                    first = true;
                    if (c.Params.Any (x => x.Name == "Flags")) {
                        serialize += "ComputeFlags();" + Environment.NewLine +
                                     "            " + "bw.Write(Flags);";
                        first = false;
                    }
                    foreach (var p in c.Params.Where(x => x.Name != "Flags"))
                    {
                        var code = WriteWriteCode (p);
                        if (String.IsNullOrEmpty (code))
                            continue;

                        if (!first) {
                            serialize += Environment.NewLine + "            ";
                        } else {
                            first = false;
                        }
                        serialize += code;
                    }
                    if (serialize == String.Empty)
                        serialize = "// do nothing else";
                    temp = temp.Replace("/* SERIALIZE */", serialize);
                    #endregion
                    #region DeSerializeFunc
                    var deserialize = String.Empty;
                    first = true;
                    foreach (var p in c.Params)
                    {
                        if (!first) {
                            deserialize += Environment.NewLine + "            ";
                        } else {
                            first = false;
                        }
                        deserialize += WriteReadCode (p);
                    }
                    if (deserialize == String.Empty)
                        deserialize = "// do nothing";
                    temp = temp.Replace("/* DESERIALIZE */", deserialize);
                    #endregion
                    #region DeSerializeRespFunc
                    var deserializeResp = "";
                    var p2 = new Param() { Name = "Response", Type = c.Type };
                    deserializeResp += WriteReadCode(p2);
                    temp = temp.Replace("/* DESERIALIZEResp */", deserializeResp);
                    #endregion
                    writer.Write(temp);
                    writer.Close();
                    classFile.Close();
                }
            }
        }

        public static string FormatName(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            if (input.IndexOf('.') != -1)
            {
                input = input.Replace(".", " ");
                var temp = "";
                foreach (var s in input.Split(' '))
                {
                    temp += FormatName(s) + " ";
                }
                input = temp.Trim();
            }
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string CheckForKeywordAndPascalCase(string name)
        {
            name = name.Replace("_", " ");
            name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            name = name.Replace(" ", "");

            if (keywords.Contains(name)) return "@" + name;
            return name;
        }

        public static string GetNameofClass(string type, bool isinterface = false, bool ismethod = false)
        {
            if (!ismethod)
            {
                if (type.IndexOf('.') != -1 && type.IndexOf('?') == -1)
                    return isinterface ? "TLAbs" + FormatName(type.Split('.')[1]) : "TL" + FormatName(type.Split('.')[1]);
                else if (type.IndexOf('.') != -1 && type.IndexOf('?') != -1)
                    return isinterface ? "TLAbs" + FormatName(type.Split('?')[1]) : "TL" + FormatName(type.Split('?')[1]);
                else
                    return isinterface ? "TLAbs" + FormatName(type) : "TL" + FormatName(type);
            }
            else
            {
                if (type.IndexOf('.') != -1 && type.IndexOf('?') == -1)
                    return "TLRequest" + FormatName(type.Split('.')[1]);
                else if (type.IndexOf('.') != -1 && type.IndexOf('?') != -1)
                    return "TLRequest" + FormatName(type.Split('?')[1]);
                else
                    return "TLRequest" + FormatName(type);
            }
        }

        private static bool IsFlagBase(string type)
        {
            return type.IndexOf("?") != -1;
        }

        private static int GetBitMask(string type)
        {
            return (int)Math.Pow((double)2, (double)int.Parse(type.Split('?')[0].Split('.')[1]));
        }

        private static bool IsTrueFlag(string type)
        {
            return type.Split('?')[1] == "true";
        }

        public static string GetNameSpace(string type)
        {
            var baseNamespace = rootNamespace + ".TL";
            if (type.IndexOf('.') != -1)
                return baseNamespace + FormatName(type.Split('.')[0]);
            else
                return baseNamespace;
        }

        public static string CheckForFlagBase(string type, string result)
        {
            if (type.IndexOf('?') == -1)
                return result;
            else
            {
                string innerType = type.Split('?')[1];
                if (innerType == "true") return result;
                else if ((new string[] { "bool", "int", "uint", "long", "double" }).Contains(result)) return result + "?";
                else return result;
            }
        }

        public static string GetTypeName(string type)
        {
            switch (type.ToLower())
            {
                case "#":
                case "int":
                    return "int";
                case "uint":
                    return "uint";
                case "long":
                    return "long";
                case "double":
                    return "double";
                case "string":
                    return "string";
                case "bytes":
                    return "byte[]";
                case "true":
                case "bool":
                    return "bool";
                case "!x":
                    return "TLObject";
                case "x":
                    return "TLObject";
            }

            if (type.StartsWith("Vector"))
                return "TLVector<" + GetTypeName(type.Replace("Vector<", "").Replace(">", "")) + ">";

            if (type.ToLower().Contains("inputcontact"))
                return "TLInputPhoneContact";


            if (type.IndexOf('.') != -1 && type.IndexOf('?') == -1)
            {
                if (interfacesList.Any(x => x.ToLower() == (type).ToLower()))
                    return FormatName(type.Split('.')[0]) + "." + "TLAbs" + type.Split('.')[1];
                else if (classesList.Any(x => x.ToLower() == (type).ToLower()))
                    return FormatName(type.Split('.')[0]) + "." + "TL" + type.Split('.')[1];
                else
                    return FormatName(type.Split('.')[1]);
            }
            else if (type.IndexOf('?') == -1)
            {
                if (interfacesList.Any(x => x.ToLower() == type.ToLower()))
                    return "TLAbs" + type;
                else if (classesList.Any(x => x.ToLower() == type.ToLower()))
                    return "TL" + type;
                else
                    return type;
            }
            else
            {
                return GetTypeName(type.Split('?')[1]);
            }
        }

        public static string LookTypeInLists(string src)
        {
            if (interfacesList.Any(x => x.ToLower() == src.ToLower()))
                return "TLAbs" + FormatName(src);
            else if (classesList.Any(x => x.ToLower() == src.ToLower()))
                return "TL" + FormatName(src);
            else
                return src;
        }

        public static string WriteWriteCode(Param p, bool flag = false)
        {
            switch (p.Type.ToLower())
            {
                case "#":
                case "int":
                    return flag ? $"bw.Write({CheckForKeywordAndPascalCase(p.Name)}.Value);" : $"bw.Write({CheckForKeywordAndPascalCase(p.Name)});";
                case "long":
                    return flag ? $"bw.Write({CheckForKeywordAndPascalCase(p.Name)}.Value);" : $"bw.Write({CheckForKeywordAndPascalCase(p.Name)});";
                case "string":
                    return $"StringUtil.Serialize({CheckForKeywordAndPascalCase(p.Name)}, bw);";
                case "bool":
                    return flag ? $"BoolUtil.Serialize({CheckForKeywordAndPascalCase(p.Name)}.Value, bw);" : $"BoolUtil.Serialize({CheckForKeywordAndPascalCase(p.Name)}, bw);";
                case "true":
                    return $"BoolUtil.Serialize({CheckForKeywordAndPascalCase(p.Name)}, bw);";
                case "bytes":
                    return $"BytesUtil.Serialize({CheckForKeywordAndPascalCase(p.Name)}, bw);";
                case "double":
                    return flag ? $"bw.Write({CheckForKeywordAndPascalCase(p.Name)}.Value);" : $"bw.Write({CheckForKeywordAndPascalCase(p.Name)});";
                default:
                    if (!IsFlagBase(p.Type))
                        return $"ObjectUtils.SerializeObject({CheckForKeywordAndPascalCase(p.Name)}, bw);";
                    else
                    {
                        if (IsTrueFlag(p.Type))
                            return $"";
                        else
                        {
                            Param p2 = new Param() { Name = p.Name, Type = p.Type.Split('?')[1] };
                            return $"if ((Flags & {GetBitMask(p.Type).ToString()}) != 0)" +
                                Environment.NewLine + "                " +
                                WriteWriteCode(p2, true);
                        }
                    }
            }
        }
        public static string WriteReadCode(Param p)
        {
            switch (p.Type.ToLower())
            {
                case "#":
                case "int":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = br.ReadInt32();";
                case "long":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = br.ReadInt64();";
                case "string":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = StringUtil.Deserialize(br);";
                case "bool":
                case "true":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = BoolUtil.Deserialize(br);";
                case "bytes":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = BytesUtil.Deserialize(br);";
                case "double":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = br.ReadDouble();";
                default:
                    if (!IsFlagBase(p.Type))
                    {
                        if (p.Type.ToLower().Contains("vector"))
                        {
                            return $"{CheckForKeywordAndPascalCase(p.Name)} = ({GetTypeName(p.Type)})ObjectUtils.DeserializeVector<{GetTypeName(p.Type).Replace("TLVector<", "").Replace(">", "")}>(br);";
                        }
                        else return $"{CheckForKeywordAndPascalCase(p.Name)} = ({GetTypeName(p.Type)})ObjectUtils.DeserializeObject(br);";
                    }
                    else
                    {
                        if (IsTrueFlag(p.Type))
                            return $"{CheckForKeywordAndPascalCase(p.Name)} = (Flags & {GetBitMask(p.Type).ToString()}) != 0;";
                        else
                        {
                            Param p2 = new Param() { Name = p.Name, Type = p.Type.Split('?')[1] };
                            return $"if ((Flags & {GetBitMask(p.Type).ToString()}) != 0)" +
                                Environment.NewLine + "                " +
                                WriteReadCode(p2) + Environment.NewLine + "            " +
                                "else" + Environment.NewLine + "                " +
                                $"{CheckForKeywordAndPascalCase(p.Name)} = null;" + Environment.NewLine;
                        }
                    }
            }
        }
        public static FileStream MakeFile(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            if (File.Exists(path))
                File.Delete(path);
            return File.OpenWrite(path);
        }
    }

}