using System;
using System.Collections.Generic;
using System.Text;

namespace TLSharp.Compiler
{
    class CSharpCompiler : TCompiler
    {
        public string Namespace { get; set; }

        class IndentStringBuilder
        {
            const int spacesPerIndentation = 4;

            StringBuilder sb = new StringBuilder();
            bool needsIndent = true;

            public int Indentation { get; set; }
            public int Length { get { return sb.Length; } set { sb.Length = value; } }

            #region Append

            public void Append(char c, bool ignoreIndent = false)
            {
                checkIndent(ignoreIndent);
                sb.Append(c);
            }
            public void Append(string str, bool ignoreIndent = false)
            {
                checkIndent(ignoreIndent);
                sb.Append(str);
            }
            public void Append(int i, bool ignoreIndent = false)
            {
                checkIndent(ignoreIndent);
                sb.Append(i);
            }

            #endregion

            #region Append line

            public void AppendLine()
            {
                sb.AppendLine();
                needsIndent = true;
            }

            public void AppendLine(string str, bool ignoreIndent = false)
            {
                checkIndent(ignoreIndent);
                sb.AppendLine(str);
                needsIndent = true;
            }

            #endregion

            #region Braces

            // open a brace and increment indentation
            public void OpenBrace()
            {
                AppendLine("{");
                ++Indentation;
            }

            // decrement indentation and close a brace
            public void CloseBrace()
            {
                --Indentation;
                AppendLine("}");
            }

            #endregion

            void checkIndent(bool ignoreIndent)
            {
                if (needsIndent && !ignoreIndent)
                {
                    sb.Append(new string(' ', spacesPerIndentation * Indentation));
                    needsIndent = false;
                }
            }

            public override string ToString()
            {
                return sb.ToString();
            }
        }

        public override string GetTLObjectsCode(List<TLObject> objs, int initialIndentation)
        {
            var isb = new IndentStringBuilder { Indentation = initialIndentation };

            var resultNames = new HashSet<string>();

            foreach (var obj in objs)
                if (!obj.IsFunction)
                    resultNames.Add(getPrettyName(getSuitableType(obj.ResultName)));

            isb.AppendLine("using System;");
            isb.AppendLine("using System.Collections.Generic;");
            isb.AppendLine("using System.IO;");
            isb.AppendLine("using System.Text;");
            isb.AppendLine();

            #region Namespace

            if (!string.IsNullOrEmpty(Namespace))
            {
                isb.Append("namespace ");
                isb.AppendLine(Namespace);
                isb.OpenBrace();
            }

            #endregion

            #region Abstract types

            isb.AppendLine("#region Abstract types");
            isb.AppendLine();

            // TLObject
            isb.AppendLine("public abstract class TLObject");
            isb.OpenBrace();
            isb.AppendLine("public abstract uint ConstructorCode { get; }");
            isb.AppendLine("public abstract void Write(TBinaryWriter writer);");
            isb.AppendLine("public abstract void Read(TBinaryReader reader);");
            isb.CloseBrace();
            isb.AppendLine();

            // MTProtoRequest
            isb.AppendLine("public abstract class MTProtoRequest");
            isb.OpenBrace();
            isb.AppendLine("public long MessageId { get; set; }");
            isb.AppendLine("public int Sequence { get; set; }");
            isb.AppendLine("public abstract uint ConstructorCode { get; }");
            isb.AppendLine();
            isb.AppendLine("public bool Dirty { get; set; }");
            isb.AppendLine();
            isb.AppendLine("public bool Sent { get; private set; }");
            isb.AppendLine("public DateTime SendTime { get; private set; }");
            isb.AppendLine("public bool ConfirmReceived { get; set; }");
            isb.AppendLine("public abstract void OnSend(TBinaryWriter writer);");
            isb.AppendLine("public abstract void OnResponse(TBinaryReader reader);");
            isb.AppendLine("public abstract void OnException(Exception exception);");
            isb.AppendLine("public abstract bool Confirmed { get; }");
            isb.AppendLine("public abstract bool Responded { get; }");
            isb.AppendLine();
            isb.AppendLine("public virtual void OnSendSuccess()");
            isb.OpenBrace();
            isb.AppendLine("SendTime = DateTime.Now;");
            isb.AppendLine("Sent = true;");
            isb.CloseBrace();
            isb.AppendLine();
            isb.AppendLine("public virtual void OnConfirm()");
            isb.OpenBrace();
            isb.AppendLine("ConfirmReceived = true;");
            isb.CloseBrace();
            isb.AppendLine();
            isb.AppendLine("public bool NeedResend => Dirty || (Confirmed && !ConfirmReceived && DateTime.Now - SendTime > TimeSpan.FromSeconds(3));");
            isb.CloseBrace();
            isb.AppendLine();

            // Abstract classes : TLObject
            foreach (var resultName in resultNames)
            {
                isb.Append("public abstract class ");
                isb.Append(resultName);
                isb.AppendLine(" : TLObject { }");
            }
            isb.AppendLine();
            isb.AppendLine("#endregion");
            isb.AppendLine();

            #endregion

            #region TBinaryReader and TBinaryWriter

            isb.AppendLine("#region TBinaryReader and TBinaryWriter");
            isb.AppendLine();

            // TBinaryReader
            isb.AppendLine("public class TBinaryReader : BinaryReader");
            isb.OpenBrace();

            // TBinaryReader::Constructors
            isb.AppendLine("public TBinaryReader(Stream stream) : base(stream) { }");
            isb.AppendLine("public TBinaryReader(Stream stream, Encoding encoding) : base(stream, encoding) { }");
            isb.AppendLine("public TBinaryReader(Stream stream, Encoding encoding, bool leaveOpen) : base(stream, encoding, leaveOpen) { }");
            isb.AppendLine();

            // TBinaryReader::ReadBytes()
            isb.AppendLine("public byte[] ReadBytes()");
            isb.OpenBrace();
            isb.AppendLine("byte firstByte = ReadByte();");
            isb.AppendLine("int len, padding;");
            isb.AppendLine("if (firstByte == 254)");
            isb.OpenBrace();
            isb.AppendLine("len = ReadByte() | (ReadByte() << 8) | (ReadByte() << 16);");
            isb.AppendLine("padding = len % 4;");
            isb.CloseBrace();
            isb.AppendLine("else");
            isb.OpenBrace();
            isb.AppendLine("len = firstByte;");
            isb.AppendLine("padding = (len + 1) % 4;");
            isb.CloseBrace();
            isb.AppendLine();
            isb.AppendLine("byte[] data = ReadBytes(len);");
            isb.AppendLine("if (padding > 0)");
            isb.OpenBrace();
            isb.AppendLine("padding = 4 - padding;");
            isb.AppendLine("ReadBytes(padding);");
            isb.CloseBrace();
            isb.AppendLine();
            isb.AppendLine("return data;");
            isb.CloseBrace();
            isb.AppendLine();

            // TBinaryReader::ReadString()
            isb.AppendLine("public override string ReadString()");
            isb.OpenBrace();
            isb.AppendLine("byte[] data = ReadBytes();");
            isb.AppendLine("return Encoding.UTF8.GetString(data, 0, data.Length);");
            isb.CloseBrace();
            isb.AppendLine();

            // TBinaryReader::ReadBoolean()
            isb.AppendLine("public override bool ReadBoolean()");
            isb.OpenBrace();
            isb.AppendLine("return ReadUInt32() == 0x997275b5; // uint == true code ? true : false");
            isb.CloseBrace();
            isb.AppendLine();

            // TBinaryReader::ReadTrue()
            isb.AppendLine("public True ReadTrue()");
            isb.OpenBrace();
            isb.AppendLine("// true's don't require to be read, they're only used in flags");
            isb.AppendLine("return new TL.TrueType();");
            isb.CloseBrace();
            isb.AppendLine();

            // TBinaryReader::Read<T>()
            isb.AppendLine("public T Read<T>()");
            isb.OpenBrace();
            isb.AppendLine("return (T)(object)ReadTLObject();");
            isb.CloseBrace();
            isb.AppendLine();

            // TBinaryReader::ReadTLObject()
            isb.AppendLine("public TLObject ReadTLObject()");
            isb.OpenBrace();
            isb.AppendLine("var code = ReadUInt32();");
            isb.AppendLine("return ReadTLObject(code);");
            isb.CloseBrace();
            isb.AppendLine();

            // TBinaryReader::ReadTLObject(uint code)
            isb.AppendLine("public TLObject ReadTLObject(uint code)");
            isb.OpenBrace();
            isb.AppendLine("TLObject obj = (TLObject)Activator.CreateInstance(TL.Constructors[code]);");
            isb.AppendLine("obj.Read(this);");
            isb.AppendLine("return obj;");
            isb.CloseBrace();

            isb.CloseBrace();
            isb.AppendLine();

            // TBinaryWriter
            isb.AppendLine("public class TBinaryWriter : BinaryWriter");
            isb.OpenBrace();

            // TBinaryWriter::Constructors
            isb.AppendLine("public TBinaryWriter() { }");
            isb.AppendLine("public TBinaryWriter(Stream stream): base(stream) { }");
            isb.AppendLine("public TBinaryWriter(Stream stream, Encoding encoding): base(stream, encoding) { }");
            isb.AppendLine("public TBinaryWriter(Stream stream, Encoding encoding, bool leaveOpen): base(stream, encoding, leaveOpen) { }");
            isb.AppendLine();

            // TBinaryWriter::Write()
            isb.AppendLine("public override void Write(byte[] data)");
            isb.OpenBrace();
            isb.AppendLine("int padding;");
            isb.AppendLine("if (data.Length < 254)");
            isb.OpenBrace();
            isb.AppendLine("padding = (data.Length + 1) % 4;");
            isb.AppendLine("if (padding != 0)");
            isb.OpenBrace();
            isb.AppendLine("padding = 4 - padding;");
            isb.CloseBrace();
            isb.AppendLine();
            isb.AppendLine("base.Write((byte)data.Length);");
            isb.AppendLine("base.Write(data);");
            isb.CloseBrace();
            isb.AppendLine("else");
            isb.OpenBrace();
            isb.AppendLine("padding = (data.Length) % 4;");
            isb.AppendLine("if (padding != 0)");
            isb.OpenBrace();
            isb.AppendLine("padding = 4 - padding;");
            isb.CloseBrace();
            isb.AppendLine("base.Write((byte)254);");
            isb.AppendLine("base.Write((byte)(data.Length));");
            isb.AppendLine("base.Write((byte)(data.Length >> 8));");
            isb.AppendLine("base.Write((byte)(data.Length >> 16));");
            isb.AppendLine("base.Write(data);");
            isb.CloseBrace();
            isb.AppendLine();
            isb.AppendLine("for (int i = 0; i < padding; i++)");
            isb.OpenBrace();
            isb.AppendLine("base.Write((byte)0);");
            isb.CloseBrace();
            isb.CloseBrace();
            isb.AppendLine("public void WriteBase(byte[] data) => base.Write(data);");
            isb.AppendLine();

            // TBinaryReader::ReadString()
            isb.AppendLine("public override void Write(string value)");
            isb.OpenBrace();
            isb.AppendLine("Write(Encoding.UTF8.GetBytes(value));");
            isb.CloseBrace();
            isb.AppendLine();

            // TBinaryReader::ReadBoolean()
            isb.AppendLine("public override void Write(bool value)");
            isb.OpenBrace();
            isb.AppendLine("//            true         false");
            isb.AppendLine("Write(value ? 0x997275b5 : 0xbc799737);");
            isb.CloseBrace();
            isb.AppendLine();

            isb.CloseBrace();
            isb.AppendLine();
            isb.AppendLine("#endregion");
            isb.AppendLine();

            #endregion

            #region TL (requests and types)

            isb.AppendLine("public class TL");
            isb.OpenBrace();

            #region Constructors dictionary

            isb.AppendLine("#region Constructors dictionary");
            isb.AppendLine();
            isb.AppendLine("public static readonly Dictionary<uint, Type> Constructors = new Dictionary<uint, Type>()");
            isb.OpenBrace();

            foreach (var obj in objs)
                if (!obj.IsFunction)
                {
                    isb.Append("{ 0x");
                    isb.Append(obj.Constructor);
                    isb.Append(", typeof(");
                    isb.Append(getPrettyName(obj));
                    isb.AppendLine(") },");
                }
            isb.Length -= 1 + Environment.NewLine.Length; // ",".Length
            isb.AppendLine();
            --isb.Indentation;
            isb.AppendLine("};");

            isb.AppendLine();
            isb.AppendLine("#endregion");
            isb.AppendLine();

            #endregion

            #region Functions

            isb.AppendLine("#region Functions (requests)");
            isb.AppendLine();

            foreach (var obj in objs)
                if (obj.IsFunction)
                    isb.AppendLine(GetTLObjectCode(obj, isb.Indentation), true);
            isb.AppendLine("#endregion");
            isb.AppendLine();

            #endregion

            #region Types

            isb.AppendLine("#region Types");
            isb.AppendLine();

            foreach (var obj in objs)
                if (!obj.IsFunction)
                    isb.AppendLine(GetTLObjectCode(obj, isb.Indentation), true);
            isb.AppendLine("#endregion");

            #endregion

            #endregion

            isb.CloseBrace();

            #region Namespace

            if (!string.IsNullOrEmpty(Namespace))
            {
                isb.CloseBrace();
            }

            #endregion

            return isb.ToString();
        }
        
        public override string GetTLObjectCode(TLObject obj, int initialIndentation)
        {
            var isb = new IndentStringBuilder { Indentation = initialIndentation };

            // class declaration: «public class <name> : <inherit>»
            isb.Append("public class ");
            isb.Append(getPrettyName(obj));
            isb.Append(" : ");
            // inherit from request or base type whether it's a function or not
            if (obj.IsFunction)
                isb.AppendLine("MTProtoRequest");
            else
                isb.AppendLine(getSuitableType(getPrettyName(obj.ResultName)));
            isb.OpenBrace();

            // set constructor getter: «public uint ConstructorCode => 0x<constructor>;»
            isb.Append("public override uint ConstructorCode => 0x");
            isb.Append(obj.Constructor);
            isb.AppendLine(";");
            isb.AppendLine();

            // fields: «public <type> <name>;»
            if (obj.Args.Count > 0)
            {
                foreach (var arg in obj.Args)
                {
                    isb.Append("public ");
                    isb.Append(getArgumentCode(arg));
                    isb.AppendLine(";");
                }
                isb.AppendLine();
            }

            // if it's a function, add the result field: «public <type> Result;»
            if (obj.IsFunction)
            {
                isb.Append("public ");
                isb.Append(obj.HasGeneric && obj.GenericResult ? "TLObject" : getSuitableType(getPrettyName(obj.ResultName)));
                isb.AppendLine(" Result;");
                isb.AppendLine();
            }

            // empty constructor: «public <Name>() { }»
            isb.Append("public ");
            isb.Append(getPrettyName(obj));
            isb.AppendLine("() { }");
            isb.AppendLine();

            // constructor with paramethers: «public <Name>(<args type> <args name>)»
            if (obj.Args.Count > 0)
            {
                // add summary to show which arguments can be null
                if (obj.HasFlags)
                {
                    isb.AppendLine("/// <summary>");
                    isb.Append("/// The following arguments can be null: ");
                    foreach (var arg in obj.GetFlags())
                    {
                        isb.Append(getPrettyName(arg.Name));
                        isb.Append(", ");
                    }
                    isb.Length -= 2; // ", ".Length
                    isb.AppendLine();
                    isb.AppendLine("/// </summary>");
                    foreach (var arg in obj.Args)
                    {
                        isb.Append("/// <param name=\"");
                        isb.Append(getPrettyName(arg.Name));
                        isb.Append("\">");
                        isb.Append("Can ");
                        if (!arg.IsFlag)
                            isb.Append("NOT ");
                        isb.AppendLine("be null</param>");
                    }
                }
                isb.Append("public ");
                isb.Append(getPrettyName(obj));
                isb.Append('(');
                foreach (var arg in obj.Args)
                {
                    isb.Append(getArgumentCode(arg));
                    isb.Append(", ");
                }
                isb.Length -= 2; // ", ".Length
                isb.AppendLine(")");

                // set the args values to their fields: «this.<arg name> = <arg name>;»
                isb.OpenBrace();
                foreach (var arg in obj.Args)
                {
                    isb.Append("this.");
                    isb.Append(getPrettyName(arg.Name));
                    isb.Append(" = ");
                    isb.Append(getPrettyName(arg.Name));
                    isb.AppendLine(";");
                }
                isb.CloseBrace();
                isb.AppendLine();
            }

            // write: «public override void Write(TBinaryWriter writer)»
            if (obj.IsFunction)
                isb.AppendLine("public override void OnSend(TBinaryWriter writer)");
            else
                isb.AppendLine("public override void Write(TBinaryWriter writer)");
            isb.OpenBrace();

            // if object has flags, calculate the values: «var flags = (<args name> != null ? (1 << n) : 0)»
            if (obj.HasFlags)
            {
                isb.Append("int flags =");
                ++isb.Indentation;
                foreach (var flag in obj.GetFlags())
                {
                    isb.AppendLine();
                    isb.Append('(');
                    isb.Append(getPrettyName(flag.Name));
                    isb.Append(" != null ? 1 << ");
                    isb.Append(flag.FlagIndex);
                    isb.Append(" : 0) |");
                }
                isb.Length -= 2; // " |".Length
                isb.AppendLine(";");
                isb.AppendLine();
                --isb.Indentation;
            }

            // write constructor code: «writer.Write(ConstructorCode);»
            isb.AppendLine("writer.Write(ConstructorCode);");
            if (obj.HasFlags)
            {
                isb.AppendLine("writer.Write(flags);");
                isb.AppendLine();
            }
            // write every field: «writer.Write(<arg name>);»
            foreach (var arg in obj.Args)
                isb.AppendLine(GetArgumentWrite(arg, isb.Indentation), true);
            isb.CloseBrace();
            isb.AppendLine();

            // read: «public override void Read(TBinaryReader reader)»
            if (obj.IsFunction)
                isb.AppendLine("public override void OnResponse(TBinaryReader reader)");
            else
                isb.AppendLine("public override void Read(TBinaryReader reader)");

            // if it's a function, parse result only: «Result = reader.Read<type>();»
            isb.OpenBrace();
            if (obj.IsFunction)
            {
                if (obj.HasGeneric && obj.GenericResult)
                    isb.AppendLine("Result = reader.Read<TLObject>();");
                else
                {
                    isb.Append("Result = ");
                    isb.Append(getOnlyRead(getSuitableType(getPrettyName(obj.ResultName))));
                    isb.AppendLine(";");
                }
            }
            else // else read and set all the fields manually: «arg = reader.Read<type>();»
            {
                if (obj.HasFlags)
                {
                    isb.AppendLine("int flags = reader.ReadInt32();");
                }

                foreach (var arg in obj.Args)
                    isb.AppendLine(GetArgumentRead(arg, isb.Indentation), true);
            }
            isb.CloseBrace();
            isb.AppendLine();

            // override OnException, Confirmed and Responded
            if (obj.IsFunction)
            {
                isb.AppendLine("public override void OnException(Exception exception)");
                isb.OpenBrace();
                isb.AppendLine("throw exception;");
                isb.CloseBrace();
                isb.AppendLine();
                isb.AppendLine("public override bool Confirmed => true;");
                isb.AppendLine("public override bool Responded { get; }");
                isb.AppendLine();
            }
            // to string: «public override string ToString()»
            isb.AppendLine("public override string ToString()");
            isb.OpenBrace();

            isb.Append("return ");
            if (obj.Args.Count == 0) // no arguments: «return "(<name>)";»
            {
                isb.Append("\"(");
                isb.Append(getPrettyName(obj));
                isb.AppendLine(")\";");
            }
            else // arguments: «return string.Format("(name <args name>:{n})", <args name>);»
            {
                isb.Append("string.Format(\"(");
                isb.Append(getPrettyName(obj));
                for (int i = 0; i < obj.Args.Count; i++)
                {
                    isb.Append(' ');
                    isb.Append(getPrettyName(obj.Args[i].Name));
                    isb.Append(":{");
                    isb.Append(i);
                    isb.Append("}");
                }
                isb.Append(")\"");
                foreach (var arg in obj.Args)
                {
                    isb.Append(", ");
                    isb.Append(getPrettyName(arg.Name));
                }
                isb.AppendLine(");");
            }
            isb.CloseBrace();

            isb.CloseBrace();
            return isb.ToString();
        }

        #region Arguments

        // join "Type Name"
        static string getArgumentCode(Argument arg)
            => getSuitableType(arg.Type) + (arg.IsFlag && typeNeedsNullable(arg.Type) ? "? " : " ") + getPrettyName(arg.Name);

        static bool typeNeedsNullable(string type)
        {
            switch (type)
            {
                case "int":
                case "long":
                case "double":
                case "bool":
                    return true;

                default:
                    return false;
            }
        }

        #region Write argument

        // get the argument read part
        public override string GetArgumentWrite(Argument arg, int initialIndentation)
        {
            var type = getSuitableType(arg.Type);
            var name = getPrettyName(arg.Name);

            var isb = new IndentStringBuilder { Indentation = initialIndentation };
            if (arg.IsFlag)
            {
                isb.Append("if (");
                isb.Append(name);
                isb.AppendLine(" != null) {");
                ++isb.Indentation;
            }
            if (arg.IsVector)
            {
                isb.Append("writer.Write(");
                isb.Append(vectorConstructor);
                isb.AppendLine("); // vector code");
                isb.Append("writer.Write(");
                isb.Append(name);
                isb.AppendLine(".Count);");
                isb.Append("foreach (");
                isb.Append(getSuitableType(arg.InnerVectorType));
                isb.Append(' ');
                isb.Append(name);
                isb.Append("Element in ");
                isb.Append(name);
                isb.AppendLine(")");
                isb.Append(GetArgumentWrite(
                    new Argument(name + "Element", arg.InnerVectorType), isb.Indentation + 1), true);
            }
            else
            {
                var write = getOnlyWrite(type, arg.IsFlag && typeNeedsNullable(type) ? name + ".Value" : name);
                if (!string.IsNullOrEmpty(write))
                {
                    isb.Append(write);
                    isb.Append(';');
                }
            }
            if (arg.IsFlag)
            {
                isb.AppendLine();
                isb.CloseBrace();
            }

            return isb.ToString();
        }

        // get only the write part, without indentation, equals, etc
        // returns null if it shouldn't be written
        static string getOnlyWrite(string type, string name)
        {
            switch (type)
            {
                case "int":
                case "long":
                case "Int128":
                case "Int256":
                case "double":
                case "string":
                case "byte[]":
                case "bool":
                    return "writer.Write(" + name + ")";
                case "True":
                    return null;
                default:
                    return name + (type == "MTProtoRequest" ? ".OnSend(writer)" : ".Write(writer)");
            }
        }

        #endregion

        #region Read argument

        // get the argument read part
        public override string GetArgumentRead(Argument arg, int initialIndentation)
        {
            var type = getSuitableType(arg.Type);
            var name = getPrettyName(arg.Name);

            var isb = new IndentStringBuilder { Indentation = initialIndentation };

            if (arg.IsFlag)
            {
                isb.Append("if ((flags & (1 << ");
                isb.Append(arg.FlagIndex);
                isb.AppendLine(")) != 0) {");
                ++isb.Indentation;
            }
            if (arg.IsVector)
            {
                isb.AppendLine("reader.ReadInt32(); // vector code");
                isb.Append("int ");
                isb.Append(name);
                isb.AppendLine("Length = reader.ReadInt32();");
                isb.Append(name);
                isb.Append(" = new ");
                isb.Append(type);
                isb.Append("(");
                isb.Append(name);
                isb.Append("Length");
                isb.AppendLine(");");
                isb.Append("for (int ");
                isb.Append(name);
                isb.Append("Index = 0; ");
                isb.Append(name);
                isb.Append("Index < ");
                isb.Append(name);
                isb.Append("Length; ");
                isb.Append(name);
                isb.AppendLine("Index++)");
                ++isb.Indentation;
                isb.Append(name);
                isb.Append(".Add(");
                isb.Append(getOnlyRead(getSuitableType(arg.InnerVectorType)));
                isb.Append(");");
            }
            else
            {
                isb.Append(name);
                isb.Append(" = ");
                isb.Append(getOnlyRead(type));
                isb.Append(';');
            }
            if (arg.IsFlag)
            {
                isb.AppendLine();
                isb.CloseBrace();
            }

            return isb.ToString();
        }

        // get only the read part, without indentation, equals, etc
        static string getOnlyRead(string type)
        {
            switch (type)
            {
                case "int": return "reader.ReadInt32()";
                case "long": return "reader.ReadInt64()";
                case "double": return "reader.ReadDouble()";
                case "string": return "reader.ReadString()";
                case "byte[]": return "reader.ReadBytes()";
                case "bool": return "reader.ReadBoolean()";
                case "True": return "reader.ReadTrue()";

                default: return "reader.Read<" + type + ">()";
            }
        }

        #endregion

        #endregion

        #region Beautify names and sanitize types

        #region Beautify name

        // get a pretty name for an object
        static string getPrettyName(TLObject obj)
            => getPrettyName(obj.Name) + (obj.IsFunction ? "Request" : "Type");

        // get a pretty name for any name
        static string getPrettyName(string uglyName)
        {
            var sb = new StringBuilder();

            bool nextUpper = true;
            foreach (var c in uglyName)
            {
                if (c == '_' || c == '.')
                {
                    nextUpper = true;
                    continue;
                }
                if (nextUpper)
                {
                    sb.Append(char.IsLower(c) ? char.ToUpper(c) : c);
                    nextUpper = false;
                    continue;
                }
                if (c == '<')
                {
                    nextUpper = true;
                }

                sb.Append(c);
            }

            return sb.ToString();
        }

        #endregion

        #region Sanitize type

        // some types are not valid in C#, convert them from .tl -> .cs
        static string getSuitableType(string nonSuitableType)
        {
            nonSuitableType = getPrettyName(nonSuitableType);

            // pretty name "breaks" these types :(
            if (nonSuitableType.Contains("Int")
                && !nonSuitableType.Contains("Int128") && !nonSuitableType.Contains("Int256"))
                nonSuitableType = nonSuitableType.Replace("Int", "int");

            if (nonSuitableType.Contains("Long"))
                nonSuitableType = nonSuitableType.Replace("Long", "long");

            if (nonSuitableType.Contains("Double"))
                nonSuitableType = nonSuitableType.Replace("Double", "double");

            if (nonSuitableType.Contains("String"))
                nonSuitableType = nonSuitableType.Replace("String", "string");

            if (nonSuitableType.Contains("Bool"))
                nonSuitableType = nonSuitableType.Replace("Bool", "bool");

            if (nonSuitableType.Contains("Bytes"))
                nonSuitableType = nonSuitableType.Replace("Bytes", "byte[]");

            if (nonSuitableType.Contains("Vector<"))
                nonSuitableType = nonSuitableType.Replace("Vector<", "List<");

            if (nonSuitableType.Contains("X"))
                nonSuitableType = nonSuitableType.Replace("X", "MTProtoRequest");

            return nonSuitableType;
        }

        #endregion

        #endregion
    }
}
