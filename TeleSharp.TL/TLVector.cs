using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleSharp.TL
{
    public class TLVector<T> : TLObject, IList<T>
    {
        [TLObject(481674261)]
        private List<T> lists;

        public TLVector(IEnumerable<T> collection)
        {
            lists = new List<T>(collection);
        }

        public TLVector()
        {
            lists = new List<T>();
        }

        public T this[int index]
        {
            get { return lists[index]; }
            set { lists[index] = value; }
        }

        public override int Constructor
        {
            get
            {
                return 481674261;
            }
        }

        public int Count => lists.Count;

        public bool IsReadOnly => ((IList<T>)lists).IsReadOnly;

        public void Add(T item)
        {
            lists.Add(item);
        }

        public void Clear()
        {
            lists.Clear();
        }

        public bool Contains(T item)
        {
            return lists.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lists.CopyTo(array, arrayIndex);
        }

        public override void DeserializeBody(BinaryReader br)
        {
            int count = br.ReadInt32();
            for (var i = 0; i < count; i++)
            {
                if (typeof(T) == typeof(int))
                {
                    lists.Add((T)Convert.ChangeType(br.ReadInt32(), typeof(T)));
                }
                else if (typeof(T) == typeof(long))
                {
                    lists.Add((T)Convert.ChangeType(br.ReadInt64(), typeof(T)));
                }
                else if (typeof(T) == typeof(string))
                {
                    lists.Add((T)Convert.ChangeType(StringUtil.Deserialize(br), typeof(T)));
                }
                else if (typeof(T) == typeof(double))
                {
                    lists.Add((T)Convert.ChangeType(br.ReadDouble(), typeof(T)));
                }
                else if (typeof(T).BaseType == typeof(TLObject))
                {
                    int constructor = br.ReadInt32();
                    Type type = TLContext.getType(constructor);
                    object obj = Activator.CreateInstance(type);
                    type.GetMethod("DeserializeBody").Invoke(obj, new object[] { br });
                    lists.Add((T)Convert.ChangeType(obj, type));
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return lists.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return lists.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            lists.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return lists.Remove(item);
        }

        public void RemoveAt(int index)
        {
            lists.RemoveAt(index);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(lists.Count());

            foreach (var item in lists)
            {
                if (typeof(T) == typeof(int))
                {
                    var res = (int)Convert.ChangeType(item, typeof(int));

                    bw.Write(res);
                }
                else if (typeof(T) == typeof(long))
                {
                    var res = (long)Convert.ChangeType(item, typeof(long));
                    bw.Write(res);
                }
                else if (typeof(T) == typeof(string))
                {
                    var res = (string)(Convert.ChangeType(item, typeof(string)));
                    StringUtil.Serialize(res, bw);
                }
                else if (typeof(T) == typeof(double))
                {
                    var res = (double)Convert.ChangeType(item, typeof(double));
                    bw.Write(res);
                }
                else if (typeof(T).BaseType == typeof(TLObject))
                {
                    var res = (TLObject)(object)item;
                    res.SerializeBody(bw);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return lists.GetEnumerator();
        }
    }
}
