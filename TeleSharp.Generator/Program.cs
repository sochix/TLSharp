using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.Reflection;
using System.Text.RegularExpressions;
using NDesk.Options;

namespace TeleSharp.Generator
{
    class Program
    {
        static List<String> keywords = new List<string>(new string[] { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "add", "alias", "ascending", "async", "await", "descending", "dynamic", "from", "get", "global", "group", "into", "join", "let", "orderby", "partial", "partial", "remove", "select", "set", "value", "var", "where", "where", "yield" });
        static List<String> interfacesList = new List<string>();
        static List<String> classesList = new List<string>();

        static void DisplayHelp(bool full = true)
        {
            Console.WriteLine("TLSharp TL Parser v1.0, a TL schema to C# transcompiler");
            Console.WriteLine("usage: TeleSharp.Generator [OPTIONS] INPUT [OUTPUT]");
            Console.WriteLine();
            if (full)
            {
                Console.WriteLine("Options:");
                Console.WriteLine("  -f,  --format                Sets input format.");
                Console.WriteLine("                               Accepted formats are \"tl\" and \"json\".");
                Console.WriteLine("       --template-abstract     Sets the abstract class template");
                Console.WriteLine("       --template-class        Sets the class template");
                Console.WriteLine("       --template-method       Sets the method template");
                Console.WriteLine("       --target-namespace      Sets the target namespace");
                Console.WriteLine("                               Default is \"TeleSharp.TL\"");
                Console.WriteLine("       --output-json           Only Parses TL and outputs schema as JSON");
                Console.WriteLine("  -h,  --help                  Displays this help message");
                Console.WriteLine();
                Console.WriteLine("For more information, please read the manual,");
                Console.WriteLine("or visit the GitHub page.");
                Console.WriteLine("Submit your bug reports to our GitHub repository.");
            }
            else
            {
                Console.WriteLine("Try `TeleSharp.Generator --help' for more options.");
            }
        }

        enum Format
        {
            TL,
            JSON
        }

        static void Main(string[] args)
        {
            string Json = "";
            string inputPath = "";
            string outputPath = "";
            Format format = Format.TL;
            bool forceFormat = false;
            bool outputJson = false;
            bool showHelp = false;


            string AbsStyle = File.ReadAllText("ConstructorAbs.tmp");
            string NormalStyle = File.ReadAllText("Constructor.tmp");
            string MethodStyle = File.ReadAllText("Method.tmp");
            string TargetNamespace = "TeleSharp.TL";

            bool invalidFormat = false;
            bool invalidTargetNamespace = false;

            OptionSet optionset = new OptionSet()
                .Add("h|help", h => showHelp = h != null)
                .Add("f|format=", f =>
                {
                    f = f ?? "";
                    if (f != null)
                        forceFormat = true;
                    switch (f.ToLower())
                    {
                        case "tl":
                            format = Format.TL;
                            break;
                        case "json":
                            format = Format.JSON;
                            break;
                        case "":
                            format = Format.TL;
                            break;
                        default:
                            invalidFormat = true;
                            break;
                    }
                })
                .Add("target-namespace=", a =>
                {
                    if (!string.IsNullOrEmpty(a))
                    {
                        Match m = Regex.Match(a, @"(@?[a-z_A-Z]\w+(?:\.@?[a-z_A-Z]\w+)*)");
                        if (m.Success)
                        {
                            TargetNamespace = m.Groups[0].Value;
                        }
                    }
                    else
                    {
                        invalidTargetNamespace = true;
                    }
                })
                .Add("output-json", a => outputJson = a != null)
                .Add("template-abstract=", a => AbsStyle = (a != null) ? File.ReadAllText(a) : AbsStyle)
                .Add("template-normal=", a => NormalStyle = (a != null) ? File.ReadAllText(a) : NormalStyle)
                .Add("template-method=", a => MethodStyle = (a != null) ? File.ReadAllText(a) : MethodStyle);
            List<string> extra;
            try
            {
                extra = optionset.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("Error: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `TeleSharp.Generator --help' for more information.");
                return;
            }
            if (showHelp)
            {
                DisplayHelp(true);
                return;
            }
            if (extra == null || extra.Count == 0)
            {
                DisplayHelp(false);
                return;
            }
            if (invalidFormat)
            {
                Console.WriteLine("Error: Invalid input format.");
                Console.WriteLine("Try `TeleSharp.Generator --help' for more information.");
                return;
            }
            if (invalidTargetNamespace)
            {
                Console.WriteLine("Error: Invalid target namespace.");
                Console.WriteLine("Try `TeleSharp.Generator --help' for more information.");
                return;
            }
            
            inputPath = extra[0];
            if (!forceFormat)
            {
                string ext = Path.GetExtension(extra[0]);
                if (ext == "json")
                {
                    format = Format.JSON;
                }
            }
            if (extra.Count > 1)
            {
                outputPath = extra[1];
            }
            else
            { // no output path provided
                if (!outputJson)
                    outputPath = Path.GetDirectoryName(Path.GetFullPath(extra[0]));
                else
                    outputPath = Path.ChangeExtension(inputPath, ".json");
            }

            Console.WriteLine("TLSharp TL Parser v1.0, a TL schema to C# transcompiler");


            Json = File.ReadAllText(inputPath);

            if (format == Format.TL)
            { // if input is tl, convert to json
                Json = TL2JSON.ParseToJson(Json);
                Console.WriteLine("Converting TL to JSON...");
            }

            if (outputJson)
            {
                File.WriteAllText(outputPath, Json);
                return;
            }
            #region Translate to C#

            Schema schema = JsonConvert.DeserializeObject<Schema>(Json);
            foreach (var c in schema.Constructors)
            {
                interfacesList.Add(c.BaseType);
                classesList.Add(c.ConstructorName);
            }
            Console.WriteLine("Implementing abstract classes...");
            var abstractParams = new Dictionary<string, List<Param>>();
            #region Abstract classes
            foreach (var c in schema.Constructors)
            {
                var list = schema.Constructors.Where(x => x.BaseType == c.BaseType); // check if there is a dependence on base type of this class (base type is an abstract class)
                if (list.Count() > 1)
                {
                    string path = Path.Combine(outputPath, GetNameSpace(c.BaseType, TargetNamespace).Replace(TargetNamespace, @"TL\").Replace(".", ""), GetNameofClass(c.BaseType, true) + ".cs").Replace(@"\\", @"\");
                    FileStream classFile = MakeFile(path);
                    using (StreamWriter writer = new StreamWriter(classFile))
                    {
                        string temp = AbsStyle.Replace("/* NAMESPACE */", GetNameSpace(c.BaseType, TargetNamespace).TrimEnd('.'));
                        temp = temp.Replace("/* NAME */", GetNameofClass(c.BaseType, true));
                        writer.Write(temp);
                        writer.Close();
                        classFile.Close();
                    }
                }
                else
                {
                    interfacesList.Remove(list.First().BaseType);
                    list.First().BaseType = "himself";
                }
            }
            #endregion

            Console.WriteLine("Implementing types...");
            #region Constructors
            foreach (var c in schema.Constructors)
            {
                string path = Path.Combine(outputPath, GetNameSpace(c.ConstructorName, TargetNamespace).Replace(TargetNamespace, @"TL\").Replace(".", ""), GetNameofClass(c.ConstructorName, false) + ".cs").Replace(@"\\", @"\");
                FileStream classFile = MakeFile(path);
                using (StreamWriter writer = new StreamWriter(classFile))
                {
                    #region About Class
                    string temp = NormalStyle.Replace("/* NAMESPACE */", GetNameSpace(c.ConstructorName, TargetNamespace).TrimEnd('.'));
                    temp = (c.BaseType == "himself") ? 
                        temp.Replace("/* PARENT */", "TLObject") : 
                        temp.Replace("/* PARENT */", GetNameofClass(c.BaseType, true));
                    temp = temp.Replace("/*Constructor*/", c.Id.ToString());
                    temp = temp.Replace("/* NAME */", GetNameofClass(c.ConstructorName, false));
                    #endregion
                    #region Fields
                    /*
                     Note: Fields were mostly moved to abstract classes to provide maximum polymorphism usability.
                     */
                    //string fields = "";
                    string parent_name = GetNameofClass(c.BaseType, true);
                    
                    if (c.BaseType != "himself")
                    {
                        foreach (var tmp in c.Params)
                        {
                            Param field = new Param {Name = tmp.Name, Type = tmp.Type};
                            if (abstractParams.All(item => item.Key != c.BaseType))
                                abstractParams.Add(c.BaseType, new List<Param>());
                            else if (!abstractParams[c.BaseType].Any(f => f.Name == field.Name && f.Type == field.Type))
                                abstractParams[c.BaseType].Add(field);
                        }
                    }
                    else
                    {
                        string fields = "";
                        foreach (var param in c.Params)
                        {
                            fields += $"        public {CheckForFlagBase(param.Type, GetTypeName(param.Type))} {CheckForKeyword(param.Name)} " + "{ get; set; }" + Environment.NewLine;
                        }
                        temp = temp.Replace("/* PARAMS */", fields);
                    }
                    #endregion
                    #region ComputeFlagFunc
                    if (c.Params.All(x => x.Name != "flags")) temp = temp.Replace("/* COMPUTE */", "");
                    else
                    {
                        var compute = "flags = 0;" + Environment.NewLine;
                        foreach (var param in c.Params.Where(x => IsFlagBase(x.Type)))
                        {
                            if (IsTrueFlag(param.Type))
                            {
                                compute += $"flags = {CheckForKeyword(param.Name)} ? (flags | {GetBitMask(param.Type)}) : (flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                            else
                            {
                                compute += $"flags = {CheckForKeyword(param.Name)} != null ? (flags | {GetBitMask(param.Type)}) : (flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                        }
                        temp = temp.Replace("/* COMPUTE */", compute);
                    }
                    #endregion
                    #region SerializeFunc
                    var serialize = "";

                    if (c.Params.Any(x => x.Name == "flags")) 
                        serialize += "ComputeFlags();" + Environment.NewLine + "bw.Write(flags);" + Environment.NewLine;
                    foreach (var p in c.Params.Where(x => x.Name != "flags"))
                    {
                        serialize += WriteWriteCode(p) + Environment.NewLine;
                    }
                    temp = temp.Replace("/* SERIALIZE */", serialize);
                    #endregion
                    #region DeSerializeFunc
                    var deserialize = "";

                    foreach (var p in c.Params)
                    {
                        deserialize += WriteReadCode(p) + Environment.NewLine;
                    }
                    temp = temp.Replace("/* DESERIALIZE */", deserialize);
                    #endregion
                    writer.Write(temp);
                    writer.Close();
                    classFile.Close();
                }
            }
            #endregion

            Console.WriteLine("Implementing methods...");
            #region Methods
            foreach (var c in schema.Methods)
            {
                string path = Path.Combine(outputPath, GetNameSpace(c.MethodName, TargetNamespace).Replace(TargetNamespace, @"TL\").Replace(".", ""), GetNameofClass(c.MethodName, false, true) + ".cs").Replace(@"\\", @"\");
                FileStream classFile = MakeFile(path);
                using (StreamWriter writer = new StreamWriter(classFile))
                {
                    #region About Class
                    string temp = MethodStyle.Replace("/* NAMESPACE */", GetNameSpace(c.MethodName, TargetNamespace).TrimEnd('.'));
                    temp = temp.Replace("/* PARENT */", "TLMethod");
                    temp = temp.Replace("/*Constructor*/", c.Id.ToString());
                    temp = temp.Replace("/* NAME */", GetNameofClass(c.MethodName, false, true));
                    #endregion
                    #region Fields
                    string fields = "";
                    foreach (var tmp in c.Params)
                    {
                        fields += $"        public {CheckForFlagBase(tmp.Type, GetTypeName(tmp.Type))} {CheckForKeyword(tmp.Name)} " + "{ get; set; }" + Environment.NewLine;
                    }
                    fields += $"        public {CheckForFlagBase(c.ResponseType, GetTypeName(c.ResponseType))} Response" + "{ get; set; }" + Environment.NewLine;
                    temp = temp.Replace("/* PARAMS */", fields);
                    #endregion
                    #region ComputeFlagFunc
                    if (!c.Params.Any(x => x.Name == "flags")) temp = temp.Replace("/* COMPUTE */", "");
                    else
                    {
                        var compute = "flags = 0;" + Environment.NewLine;
                        foreach (var param in c.Params.Where(x => IsFlagBase(x.Type)))
                        {
                            if (IsTrueFlag(param.Type))
                            {
                                compute += $"flags = {CheckForKeyword(param.Name)} ? (flags | {GetBitMask(param.Type)}) : (flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                            else
                            {
                                compute += $"flags = {CheckForKeyword(param.Name)} != null ? (flags | {GetBitMask(param.Type)}) : (flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                        }
                        temp = temp.Replace("/* COMPUTE */", compute);
                    }
                    #endregion
                    #region SerializeFunc
                    var serialize = "";

                    if (c.Params.Any(x => x.Name == "flags")) serialize += "ComputeFlags();" + Environment.NewLine + "bw.Write(flags);" + Environment.NewLine;
                    foreach (var p in c.Params.Where(x => x.Name != "flags"))
                    {
                        serialize += WriteWriteCode(p) + Environment.NewLine;
                    }
                    temp = temp.Replace("/* SERIALIZE */", serialize);
                    #endregion
                    #region DeSerializeFunc
                    var deserialize = "";

                    foreach (var p in c.Params)
                    {
                        deserialize += WriteReadCode(p) + Environment.NewLine;
                    }
                    temp = temp.Replace("/* DESERIALIZE */", deserialize);
                    #endregion
                    #region DeSerializeRespFunc
                    var deserializeResp = "";
                    Param p2 = new Param() { Name = "Response", Type = c.ResponseType };
                    deserializeResp += WriteReadCode(p2) + Environment.NewLine;
                    temp = temp.Replace("/* DESERIALIZEResp */", deserializeResp);
                    #endregion
                    writer.Write(temp);
                    writer.Close();
                    classFile.Close();
                }
            }
            #endregion

            Console.WriteLine("Adding fields to abstract classes...");
            // add fields to abstract classes
            foreach (KeyValuePair<string, List<Param>> absClass in abstractParams)
            {
                string path = Path.Combine(outputPath, GetNameSpace(absClass.Key, TargetNamespace).Replace(TargetNamespace, @"TL\").Replace(".", ""), GetNameofClass(absClass.Key, true) + ".cs").Replace(@"\\", @"\");
                string tmp = File.ReadAllText(path);
                tmp = tmp.Replace("/* PARAMS */", ConvertPropertyList(absClass.Value));
                File.WriteAllText(path, tmp);
            }
            #endregion

            Console.WriteLine("Done.");
        }

        public static string ConvertPropertyList(List<Param> list)
        {
            string output = "";
            foreach (var param in list)
            {
                output += $"        public {CheckForFlagBase(param.Type, GetTypeName(param.Type))} {CheckForKeyword(param.Name)} " + "{ get; set; }" + Environment.NewLine;
            }
            return output;
        }

        public static string FormatName(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH! Class Name was empty.");
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
        public static string CheckForKeyword(string name)
        {
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
        public static string GetNameSpace(string type, string targetns)
        {
            if (type.IndexOf('.') != -1)
                return targetns + "." + FormatName(type.Split('.')[0]);
            else
                return targetns;
        }
        public static string CheckForFlagBase(string type, string result)
        {
            if (type.IndexOf('?') == -1)
                return result;

            string innerType = type.Split('?')[1];

            if (innerType == "true")
                return result;

            if ((new string[] { "bool", "int", "uint", "long", "BigMath.Int128", "BigMath.Int256", "double" }).Contains(result))
                return result + "?";

            return result;
        }
        public static string GetTypeName(string type)
        {
            switch (type.ToLower())
            {
                case "#":
                case "int":
                    return "int";
                case "int128":
                    return "BigMath.Int128";
                case "int256":
                    return "BigMath.Int256";
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
                case "vector t":
                    return "List<T>";
            }

            if (type.StartsWith("Vector<"))
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
                    return flag ? $"bw.Write({CheckForKeyword(p.Name)}.Value);" : $"bw.Write({CheckForKeyword(p.Name)});";
                case "long":
                    return flag ? $"bw.Write({CheckForKeyword(p.Name)}.Value);" : $"bw.Write({CheckForKeyword(p.Name)});";
                case "string":
                    return $"StringUtil.Serialize({CheckForKeyword(p.Name)},bw);";
                case "bool":
                    return flag ? $"BoolUtil.Serialize({CheckForKeyword(p.Name)}.Value,bw);" : $"BoolUtil.Serialize({CheckForKeyword(p.Name)},bw);";
                case "true":
                    return $"BoolUtil.Serialize({CheckForKeyword(p.Name)},bw);";
                case "bytes":
                    return $"BytesUtil.Serialize({CheckForKeyword(p.Name)},bw);";
                case "double":
                    return flag ? $"bw.Write({CheckForKeyword(p.Name)}.Value);" : $"bw.Write({CheckForKeyword(p.Name)});";
                default:
                    if (!IsFlagBase(p.Type))
                        return $"ObjectUtils.SerializeObject({CheckForKeyword(p.Name)},bw);";
                    else
                    {
                        if (IsTrueFlag(p.Type))
                            return $"";
                        else
                        {
                            Param p2 = new Param() { Name = p.Name, Type = p.Type.Split('?')[1] };
                            return $"if ((flags & {GetBitMask(p.Type).ToString()}) != 0)" + Environment.NewLine +
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
                    return $"{CheckForKeyword(p.Name)} = br.ReadInt32();";
                case "long":
                    return $"{CheckForKeyword(p.Name)} = br.ReadInt64();";
                case "string":
                    return $"{CheckForKeyword(p.Name)} = StringUtil.Deserialize(br);";
                case "bool":
                case "true":
                    return $"{CheckForKeyword(p.Name)} = BoolUtil.Deserialize(br);";
                case "bytes":
                    return $"{CheckForKeyword(p.Name)} = BytesUtil.Deserialize(br);";
                case "double":
                    return $"{CheckForKeyword(p.Name)} = br.ReadDouble();";
                default:
                    if (!IsFlagBase(p.Type))
                    {
                        if (p.Type.ToLower().Contains("vector"))
                        {
                            return $"{CheckForKeyword(p.Name)} = ({GetTypeName(p.Type)})ObjectUtils.DeserializeVector<{GetTypeName(p.Type).Replace("TLVector<", "").Replace(">", "")}>(br);";
                        }
                        else return $"{CheckForKeyword(p.Name)} = ({GetTypeName(p.Type)})ObjectUtils.DeserializeObject(br);";
                    }
                    else
                    {
                        if (IsTrueFlag(p.Type))
                            return $"{CheckForKeyword(p.Name)} = (flags & {GetBitMask(p.Type).ToString()}) != 0;";
                        else
                        {
                            Param p2 = new Param() { Name = p.Name, Type = p.Type.Split('?')[1] };
                            return $"if ((flags & {GetBitMask(p.Type).ToString()}) != 0)" + Environment.NewLine +
                                WriteReadCode(p2) + Environment.NewLine +
                            "else" + Environment.NewLine +
                                $"{CheckForKeyword(p.Name)} = null;" + Environment.NewLine;
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

    struct Property
    {
        public Property(string type, string name){
            Type = type;
            Name = name;
        }
        public string Type;
        public string Name;
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof (Property))
            {
                return ((Property) obj).Type == Type && ((Property)obj).Name == Name;
            }
            return false;
        }
    }

}
