using System.Collections.Generic;

namespace TLSharp.Compiler
{
    class Tokenizer
    {
        // sections
        const string sFunctions = "functions";
        const string sTypes = "types";

        // types
        const string tFlags = "flags";

        enum Status
        {
            SeekingName,
            SeekingConstructor,

            SeekingArgName,
            SeekingArgType,

            SeekingGenericName,
            SeekingGenericType,

            SeekingFlagIndex,

            SeekingResultName,

            SeekingChange,
            SeekingComment,
            SeekingLineEnd,
        }

        // current...
        string cName, cConstructor,
               cArgName, cArgType,
               cGenericName, cGenericType,
               cResultName, cChange;

        bool cArgGeneric;
        int cFlagIndex;
        
        List<Argument> cArgs;
        List<Argument> cGenerics;

        AutoclearStringBuilder asb;
        Status stat;

        void ResetCurrent()
        {
            cName        = null;
            cConstructor = null;
            cArgName     = null;
            cArgType     = null;
            cGenericName = null;
            cGenericType = null;
            cResultName  = null;

            cArgGeneric = false;

            cFlagIndex = -1;

            stat = Status.SeekingName;

            asb = new AutoclearStringBuilder();
            
            cArgs = new List<Argument>();
            cGenerics = new List<Argument>();
        }

        static string scanLine(ref string source, int cindex)
        {
            if (source[cindex] == '\n') ++cindex;
            if (cindex == source.Length) --cindex;

            int lstart = -1;
            for (int i = cindex; i >= 0; i--)
            {
                if (source[i] == '\n')
                {
                    lstart = i + 1;
                    break;
                }
            }
            if (lstart < 0) return null;

            int lend = -1;
            for (int i = cindex; i < source.Length; i++)
            {
                if (source[i] == '\n')
                {
                    lend = i - (source[i - 1] == '\r' ? 1 : 0);
                    break;
                }
            }

            if (lend < 0) return null;
            if (lend <= lstart) return null;

            return source.Substring(lstart, lend - lstart);
        }
        
        public List<TLObject> Tokenize(string source)
        {
            var cTLObjects = new List<TLObject>();

            ResetCurrent();
            cChange = sFunctions;
            
            foreach (var c in source)
            {
                if (c == '/' && stat != Status.SeekingLineEnd)
                {
                    if (stat == Status.SeekingComment)
                    {
                        stat = Status.SeekingLineEnd;
                    }
                    else
                    {
                        stat = Status.SeekingComment;
                    }
                }

                switch (stat)
                {
                    case Status.SeekingName:

                        if (c == '#')
                        {
                            cName = asb;
                            stat = Status.SeekingConstructor;
                        }
                        else if (c == '-')
                        {
                            stat = Status.SeekingChange;
                        }
                        else if (!char.IsWhiteSpace(c))
                        {
                            asb.Append(c);
                        }

                        break;


                    case Status.SeekingConstructor:

                        if (char.IsWhiteSpace(c) && asb.NotEmpty)
                        {
                            cConstructor = asb;
                            stat = Status.SeekingArgName;
                        }
                        else
                        {
                            asb.Append(c);
                        }

                        break;

                    case Status.SeekingArgName:

                        if (c == '{')
                        {
                            stat = Status.SeekingGenericName;
                        }
                        else if (c == ':')
                        {
                            cArgName = asb;
                            stat = Status.SeekingArgType;
                        }
                        else if (c == '=')
                        {
                            stat = Status.SeekingResultName;
                        }
                        else if (!char.IsWhiteSpace(c))
                        {
                            asb.Append(c);
                        }

                        break;

                    case Status.SeekingArgType:


                        if (c == '!')
                        {
                            cArgGeneric = true;
                        }
                        else if (c == '#') // flags argument
                        {
                            stat = Status.SeekingArgName;
                        }

                        else if (c == '.' && asb.Peek() == tFlags)
                        {
                            asb.Clear();
                            stat = Status.SeekingFlagIndex;
                        }

                        else if (char.IsWhiteSpace(c) && asb.NotEmpty)
                        {
                            cArgType = asb;

                            cArgs.Add(new Argument(cArgName, cArgType, cArgGeneric, cFlagIndex));

                            cArgGeneric = false;
                            cFlagIndex = -1;
                            stat = Status.SeekingArgName;
                        }
                        else
                        {
                            asb.Append(c);
                        }

                        break;

                    case Status.SeekingGenericName:

                        if (c == ':')
                        {
                            cGenericName = asb;
                            stat = Status.SeekingGenericType;
                        }
                        else if (!char.IsWhiteSpace(c))
                        {
                            asb.Append(c);
                        }

                        break;

                    case Status.SeekingGenericType:

                        if (c == '}')
                        {
                            cGenericType = asb;
                            cGenerics.Add(new Argument(cGenericName, cGenericType));
                            stat = Status.SeekingArgName;
                        }
                        else if (!char.IsWhiteSpace(c) && asb.NotEmpty)
                        {
                            asb.Append(c);
                        }

                        break;

                    case Status.SeekingFlagIndex:

                        if (c == '?')
                        {
                            cFlagIndex = int.Parse(asb);
                            stat = Status.SeekingArgType;
                        }
                        else if (c >= '0' && c <= '9')
                        {
                            asb.Append(c);
                        }

                        break;

                    case Status.SeekingResultName:

                        if (c == ';' || c == '\n')
                        {
                            cResultName = asb;
                            cTLObjects.Add(new TLObject(cName, cConstructor, cArgs, cResultName, cChange == sFunctions));
                            ResetCurrent();
                        }
                        else if (!char.IsWhiteSpace(c))
                        {
                            asb.Append(c);
                        }

                        break;

                    case Status.SeekingChange:
                        
                        if (!char.IsWhiteSpace(c))
                        {
                            if (c != '-')
                            {
                                asb.Append(c);
                            }
                            else if (asb.NotEmpty)
                            {
                                cChange = asb;
                                stat = Status.SeekingLineEnd;
                            }
                        }
                        break;

                    case Status.SeekingComment: break;
                    case Status.SeekingLineEnd:

                        if (c == '\n')
                        {
                            ResetCurrent();
                        }

                        break;
                }
            }

            return cTLObjects;
        }
    }
}
