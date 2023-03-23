using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_form_app_tutorial.Class
{
    public class HttpProgress : IProgress<long>
    {
        private readonly BackgroundWorker _backgroundWorker;
        private readonly long _totalBytes;

        public HttpProgress(BackgroundWorker backgroundWorker, long totalBytes)
        {
            _backgroundWorker = backgroundWorker;
            _totalBytes = totalBytes;
        }

        public void Report(long bytesRead)
        {
            int progressPercentage = (int)((bytesRead * 100L) / _totalBytes);
            _backgroundWorker.ReportProgress(progressPercentage);
        }
    }
}
