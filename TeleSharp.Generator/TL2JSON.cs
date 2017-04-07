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
            input.Replace("\r\n\r\n", "\r\n").Replace("\n\n","\n");

        public static string ParseLine(string line, BlockType blockType)
        {
            List<string> convertedParamsList = new List<string>();
            var regex = @"([a-zA-Z0-9._]+)#([0-9a-fA-F]+) ((([{])([A-z0-9_.:\(\)]+)([}]))?)([a-zA-Z0-9_:<>.#? !\(\)]+)?(([ #\[]+)([A-z0-9._ ]+)([\]]))?([ ]?)=([ ]?)([a-zA-Z0-9 .!_<>\(\)]+)([ ]?);";
            // 1 = type name, 2 = constructor code, 3 = template parameters,  8 = params, 11 = Don't know what it is (seen in vector definition , 15 = base type
            // template parameters are ignored in json schema

            var match = Regex.Match(line, regex);
            if (!match.Success)
                throw new FormatException($"Cannot parse line: \"{line}\"");
            // now parse the params to json
            
            string paramsline = Regex.Replace(match.Groups[8].Value, "[ ]{2,}", " ", RegexOptions.None);
            MatchCollection paramslist =
                Regex.Matches(
                    paramsline,
                    @"([A-z0-9._]+):(([A-z0-9._?<>]+)|(\([\(A-z0-9._<> \)]+\)))"
                );
            foreach (var param in paramslist.Cast<Match>())
            {
                convertedParamsList.Add($"{{\"name\":\"{param.Groups[1].Value.Trim()}\", \"type\":\"{param.Groups[2].Value.TrimStart('(').TrimEnd(')').Trim(' ')}\"}}");
            }
            string convertedParams = $"[{string.Join(",", convertedParamsList)}]"; // [ {"name":"NAME","type":"TYPE"}, {"name":"NAME","type":"TYPE"} ]

            // now, make the final object
            return $"{{" +
                     $"\"id\": \"{Convert.ToInt32("0x"+match.Groups[2].Value, 16)}\"" + "," +
                     $"\"{(blockType == BlockType.Class ? "predicate" : "method")}\": \"{match.Groups[1].Value}\"" + "," +
                     $"\"params\": {convertedParams}" + "," +
                     $"\"type\": \"{match.Groups[15].Value}\"" +
                     $"}}";
            
        }
        
        public static string ParseToJson(string input)
        {
            List<string> convertedTypesList = new List<string>();
            List<string> convertedMethodsList = new List<string>();
            string[] lines = RemoveEmptyLines(RemoveComments(input)).Replace("\r\n","\n").Trim('\n').Split('\n');

            //int functions_splitter = Array.IndexOf(lines, "---functions---");
            //string[] typeLines = lines.Take(functions_splitter - 1).ToArray();
            //string[] methodLines = lines.Skip(functions_splitter + 1).ToArray();

            BlockType blockType = BlockType.Class;
            foreach (var line in lines)
            {
                if (line == "")
                    continue;

                if (line == "---functions---")
                {
                    blockType = BlockType.Method;
                    continue;
                }
                if (line == "---types---")
                {
                    blockType = BlockType.Class;
                    continue;
                }

                if (blockType == BlockType.Class)
                    convertedTypesList.Add(ParseLine(line, blockType));
                else
                    convertedMethodsList.Add(ParseLine(line, blockType));
            }

            return $"{{\"constructors\":[{string.Join(",", convertedTypesList)}], \"methods\": [{string.Join(",", convertedMethodsList)}]}}"; // { "constructors": [ OBJECT, OBJECT ], "methods": [ OBJECT, OBJECT ] }
        }

        public enum BlockType
        {
            Class,
            Method
        }
    }
}
