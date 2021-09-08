namespace Chat.Protocol.Base
{
    public class MessageAttribute
    {
        public string Key { get; set; }
        public string Content { get; set; }

        public MessageAttribute(string data)
        {
            string[] vs = data.Split(":");
            if (vs.Length > 2)
            {
                Key = ParseContent(vs[0]);
                Content = ParseContent(vs[1]);
            }
        }

        public MessageAttribute(string key, string content)
        {
            Key = key;
            Content = content;
        }

        public override string ToString() => $"{ParseQuery(Key)}: {ParseQuery(Content)}";

        public static string ParseQuery(string content) => content.Replace(":", ";").Replace("\n", "/n");
        public static string ParseContent(string content) => content.Replace("/n","\n");
    }
}
