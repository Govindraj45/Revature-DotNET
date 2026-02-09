using System;
using System.IO;

namespace GarbageCollectionDemo
{
    public class FileManager : IDisposable
    {
        private FileStream? _fileStream;
        private bool _disposed = false;

        public void OpenFile(string path)
        {
            // Open file and keep it in memory.
            _fileStream = new FileStream(path, FileMode.Open);
        }

        public void Dispose()
        {
            // We clean things ourselves instead of waiting for GC.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Close the file safely.
                _fileStream?.Dispose();
            }

            _disposed = true;
        }
    }
}
