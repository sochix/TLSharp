using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TeleSharp.Generator
{
    static class TL2JSON
    {

        public static string RemoveComments(string input)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";
            return Regex.Replace(input,
                blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
                me =>
                {
                    if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                        return me.Value.StartsWith("//") ? Environment.NewLine : "";
                    // Keep the literal strings
                    return me.Value;
                },
                RegexOptions.Singleline);
        }
        public static string RemoveEmptyLines(string input) => 
            Regex.Replace(input, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

        public static string ParseTypeLine(string line)
        {
            List<string> convertedParamsList = new List<string>();
            var regex = @"([a-zA-Z0-9.]+)#([0-9a-fA-F]+)([a-zA-Z0-9_:<>. !]+)= ([a-zA-Z0-9 .!]+);"; // 0 = type name, 1 = constructor code, 2 = params, 3 = base type name
            var match = Regex.Match(line, regex);
            if (!match.Success)
                throw new FormatException($"Cannot parse line: \"{line}\"");
            // now parse the params to json

            string[] paramslist = Regex.Replace(match.Groups[3].Value, "[ ]{2,}", " ", RegexOptions.None).Split(' ');

            foreach (var param in paramslist)
            {
                string[] param_split = param.Split(':'); // 0=name,1=type
                if(param_split.Length == 2)
                    convertedParamsList.Add($"{{\"name\": \"{param_split[0]}\", \"type\": \"{param_split[1]}\"}}");
            }
            string convertedParams = $"[{string.Join(",", convertedParamsList)}]"; // [ {"name":"NAME","type":"TYPE"}, {"name":"NAME","type":"TYPE"} ]

            // now, make the final object
            return $"{{" +
                     $"\"id\": \"{Convert.ToInt32("0x"+match.Groups[2].Value, 16)}\"" + "," +
                     $"\"predicate\": \"{match.Groups[1].Value}\"" + "," +
                     $"\"params\": {convertedParams}" + "," +
                     $"\"type\": \"{match.Groups[4].Value}\"" +
                     $"}}";
            
        }

        public static string ParseMethodLine(string line)
        {
            List<string> convertedParamsList = new List<string>();
            var regex = @"([a-zA-Z0-9.]+)#([0-9a-fA-F]+)([a-zA-Z0-9_:<>. !]+)= ([a-zA-Z0-9 .!]+);"; // 0 = method name, 1 = method code, 2 = params, 3 = base type name
            var match = Regex.Match(line, regex);
            if (!match.Success)
                throw new FormatException($"Cannot parse line: \"{line}\"");
            // now parse the params to json

            string[] paramslist = Regex.Replace(match.Groups[3].Value, "[ ]{2,}", " ", RegexOptions.None).Split(' ');

            foreach (var param in paramslist)
            {
                string[] param_split = param.Split(':'); // 0=name,1=type
                if (param_split.Length == 2)
                    convertedParamsList.Add($"{{\"name\": \"{param_split[0]}\", \"type\": \"{param_split[1]}\"}}");
            }
            string convertedParams = $"[{string.Join(",", convertedParamsList)}]"; // [ {"name":"NAME","type":"TYPE"}, {"name":"NAME","type":"TYPE"} ]

            // now, make the final object
            return $"{{" +
                     $"\"id\": \"{Convert.ToInt32("0x" + match.Groups[2].Value, 16)}\"" + "," +
                     $"\"method\": \"{match.Groups[1].Value}\"" + "," +
                     $"\"params\": {convertedParams}" + "," +
                     $"\"type\": \"{match.Groups[4].Value}\"" +
                     $"}}";
        }

        public static string ParseToJson(string input)
        {
            List<string> convertedTypesList = new List<string>();
            List<string> convertedMethodsList = new List<string>();
            string[] lines = RemoveEmptyLines(RemoveComments(input)).Replace("\r\n","\n").Split('\n');
            
            int functions_splitter = Array.IndexOf(lines, "---functions---");
            string[] typeLines = lines.Take(functions_splitter - 1).ToArray();
            string[] methodLines = lines.Skip(functions_splitter + 1).ToArray();
            foreach (var line in typeLines)
            {
                convertedTypesList.Add(ParseTypeLine(line));
            }
            foreach (var line in typeLines)
            {
                convertedMethodsList.Add(ParseMethodLine(line));
            }

            return $"{{\"constructors\":[{string.Join(",", convertedTypesList)}], \"methods\": [{string.Join(",", convertedMethodsList)}]}}"; // { "constructors": [ OBJECT, OBJECT ], "methods": [ OBJECT, OBJECT ] }
        }
    }
}
