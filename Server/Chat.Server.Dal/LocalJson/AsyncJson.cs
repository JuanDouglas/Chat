using Chat.Server.Dal.LocalJson.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Server.Dal.LocalJson
{
    public class AsyncJson<TModel> : IAsyncJsonFile<TModel>
    {
        public AsyncJson(string filePath, Encoding encoding)
        {
            Path = filePath;
            _encoding = encoding;
            if (!File.Exists(filePath))
            {
                InitializeFile(filePath);
            }

            _fileStream = new(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            UpdateBuffer();
        }

        FileStream _fileStream;
        FileStream IAsyncJsonFile<TModel>.FileStream
        {
            get => _fileStream;
            set => _fileStream = value;
        }

        Encoding _encoding;
        Encoding IAsyncJsonFile<TModel>.Encoding
        {
            get => _encoding;
            set => _encoding = value;
        }
        public string Path { get; set; }

        bool _modified;
        bool IAsyncJsonFile<TModel>.Modified
        {
            get => _modified; set
            {
                if (value)
                {
                    Thread th = new(() =>
                    {
                        UpdateBuffer();
                    });
                    th.Start();
                    return;
                }

                _modified = value;
            }
        }
        public int FileLength => (int)new FileInfo(Path).Length;
        IEnumerable<TModel> _buffer;
        private bool disposedValue;
        IEnumerable<TModel> IAsyncJsonFile<TModel>.Buffer { get => _buffer; set => _buffer = value; }
        string IAsyncJsonFile<TModel>.Path { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        ValueTask IAsyncDisposable.DisposeAsync()
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerator<TModel> IAsyncEnumerable<TModel>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<TModel> GetOneAsync(Func<TModel, bool> predicate)
        {
            byte[] buffer = new byte[FileLength];
            await _fileStream.ReadAsync(buffer.AsMemory());
            string text = _encoding.GetString(buffer);

            if (_buffer != null)
            {
                return _buffer.FirstOrDefault(predicate);
            }

            TModel[] objs = await Task.Run(() =>
            {
                return (TModel[])JsonConvert.DeserializeObject(text);
            });

            return await Task.Run(() =>
            {
                return objs.FirstOrDefault(predicate);
            });
        }
        public void InitializeFile(string path)
        {

            if (!Directory.Exists(path))
            {
                string[] splited = path.Split('\\');
                string text = splited[^splited.Length];

                Directory.CreateDirectory(path.Remove(path.Length - text.Length));
            }


            byte[] encBytes = _encoding.GetBytes("[]");

            _fileStream = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            _fileStream.Write(encBytes, 0, encBytes.Length);
            _fileStream.Close();
        }

        bool UpdateBuffer()
        {
            if (_modified)
            {
                return false;
            }

            _buffer ??= Array.Empty<TModel>();

            byte[] buffer = new byte[IAsyncJsonFile<TModel>.DefaultBufferSize];
            string text = string.Empty;

            _fileStream.Flush();


            _fileStream.Seek(0, SeekOrigin.Begin);
            _fileStream.Read(buffer, 0, buffer.Length);
            text = _encoding.GetString(buffer);

            _buffer = JsonConvert.DeserializeObject<IEnumerable<TModel>>(text);
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~AsyncJson()
        // {
        //     // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
