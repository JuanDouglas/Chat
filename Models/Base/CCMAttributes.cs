using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Protocol.Base
{
    public class CCMAttributes : IList<MessageAttribute>
    {
        public CCMAttributes(string data)
        {
            string[] attributes = data.Split("\n");
            foreach (string stringAttribute in attributes)
            {
                Add(new(stringAttribute));
            }
        }
        public CCMAttributes()
        {
            attributes = new();
            Add(new("SendDate", DateTime.Now.ToString()));
            Add(new(CCMContent.EncodingAttributeKey, Encoding.Default.HeaderName));
        }

        private List<MessageAttribute> attributes;

        public MessageAttribute this[int index] { get => attributes[index]; set => attributes[index] = value; }

        public int Count => attributes.Count;

        public bool IsReadOnly => false;

        public void Add(MessageAttribute item)
        {
            if (attributes.FirstOrDefault(fs => fs.Key == item.Key) == null)
            {
                attributes.Add(item);
            }
        }

        public void Clear() =>
            attributes.Clear();
        public bool Contains(MessageAttribute item) =>
             attributes.Contains(item);
        public void CopyTo(MessageAttribute[] array, int arrayIndex) =>
            attributes.CopyTo(array, arrayIndex);
        public IEnumerator<MessageAttribute> GetEnumerator() =>
            attributes.GetEnumerator();
        public int IndexOf(MessageAttribute item) =>
            attributes.IndexOf(item);
        public void Insert(int index, MessageAttribute item) =>
            attributes.Insert(index, item);
        public bool Remove(MessageAttribute item) =>
            attributes.Remove(item);

        public int IndexOf(string key)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                if (key == attributes[i].Key)
                    return i;
            }

            return 0;
        }
        public void RemoveAt(int index) =>
            attributes.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            string result = string.Empty;

            foreach (MessageAttribute item in attributes)
            {
                result += $"\n{item}";
            }

            return result;
        }
    }
}
