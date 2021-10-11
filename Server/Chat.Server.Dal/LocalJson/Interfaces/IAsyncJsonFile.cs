using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Server.Dal.LocalJson.Interfaces
{
    public interface IAsyncJsonFile<TModel> : IAsyncEnumerable<TModel>, IAsyncDisposable, IDisposable
    {
        const int DefaultBufferSize = 2024;
        /// <summary>
        /// Json File Path
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Get File Stream for Json File
        /// </summary>
        protected internal FileStream FileStream { get; set; }
        /// <summary>
        /// Get Encoding for 
        /// </summary>
        protected internal Encoding Encoding { get; set; }
        /// <summary>
        /// Get File bytes length
        /// </summary>
        public int FileLength { get; }
        public bool Modified { get; set; }
        public bool IsEmpty
        {
            get
            {
                return Buffer.Count() < 1;
            }
        }
        protected internal IEnumerable<TModel> Buffer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(TModel model)
        {
            string json = JsonConvert.SerializeObject(model);
            json += "]";

            if (!IsEmpty)
            {
                json = "," + json;
            }

            byte[] encBytes = Encoding.GetBytes(json);

            FileStream.Seek(FileLength - 1, SeekOrigin.Begin);
            await FileStream.WriteAsync(encBytes.AsMemory(0, encBytes.Length));

            await FileStream.FlushAsync();
            Modified = true;
        }

        /// <summary>
        /// Zeroes the bytes with the object data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(TModel model)
        {
            string json = JsonConvert.SerializeObject(model);
            byte[] encBytes = Encoding.GetBytes(json);

            for (int i = 1; i < FileLength; i++)
            {
                bool equal = true;
                byte[] buffer = new byte[DefaultBufferSize];
                await FileStream.ReadAsync(buffer.AsMemory());

                for (int j = 0; j < encBytes.Length; j++)
                {
                    if (buffer[j] != encBytes[j])
                    {
                        equal = false;
                        break;
                    }
                }

                if (equal)
                {
                    buffer = new byte[encBytes.Length];
                    FileStream.Seek(i, SeekOrigin.Current);
                    await FileStream.WriteAsync(buffer.AsMemory());
                    break;
                }
            }

            await FileStream.FlushAsync();
            Modified = true;
        }
        protected internal virtual bool UpdateBuffer()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TModel> GetOneAsync(Func<TModel, bool> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Creates a new empty file in the specified directory
        /// </summary>
        /// <param name="path">new file path</param>
        public void InitializeFile(string path);
    }
}
