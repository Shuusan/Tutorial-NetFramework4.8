using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace windows_form_app_tutorial.Class
{
    public class ProgressStreamContent : StreamContent
    {
        private readonly Stream _content;
        private readonly int _bufferSize;
        private readonly Action<long, long> _progress;

        public ProgressStreamContent(Stream content, Action<long, long> progress, int bufferSize = 8192) : base(content)
        {
            _content = content;
            _bufferSize = bufferSize;
            _progress = progress;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            var buffer = new byte[_bufferSize];
            long totalRead = 0;
            long totalLength = _content.Length;

            using (_content)
            {
                int bytesRead;
                while ((bytesRead = await _content.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await stream.WriteAsync(buffer, 0, bytesRead);
                    totalRead += bytesRead;
                    _progress?.Invoke(totalRead, totalLength);
                }
            }
        }
    }
}
