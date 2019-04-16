using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace log4netParser
{
    public class LogDataSource
    {
        public static LogDataSource Instance { get; set; }

        public LogData LogData { get; }
        private BackgroundWorker _worker;
        private string _path;
        private long _position;
        private long _length;

        public LogDataSource(ISynchronizeInvoke invoke)
        {
            LogData = new LogData(invoke);
            Settings.Instance.LiveChanged += (sender, args) =>
            {
                if (!Settings.Instance.Live)
                {
                    StopMonitoringFile();
                }
                else
                {
                    if (_worker == null)
                    {
                        StartMonitoringFile();
                    }
                }
            };
        }
        public void SetData(string text)
        {
            StopMonitoringFile();
            LogData.Clear();
            var parser = new Parser(LogData);
            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.Write(text);
                writer.Flush();
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    LogData.SuspendUpdates();
                    while ((line = reader.ReadLine()) != null)
                    {
                        parser.ParseLine(line);
                    }
                    LogData.ResumeUpdates();
                }
            }
        }

        public void MonitorFile(string path)
        {
            StopMonitoringFile();

            Settings.Instance.LastLoadedFile = path;
            Settings.Instance.Save();
            LogData.Clear();
            _position = 0;
            _length = 0;
            _path = path;
            StartMonitoringFile();
        }

        private void StartMonitoringFile()
        {
            _worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerAsync();
        }

        private void StopMonitoringFile()
        {
            if (_worker != null)
            {
                _worker.CancelAsync();
                _worker.Dispose();
                _worker = null;
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {

            using (var file = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (e.Cancel) return;
                //if file size is less than last time, reload file from beginning
                if (file.Length < _length || _position > file.Length)
                {
                    _position = 0;
                    LogData.Clear();
                }

                _length = file.Length;
                file.Position = _position;
                var parser = new Parser(LogData);
                using (var reader = new StreamReader(file))
                {
                    for (;;)
                    {
                        if (e.Cancel) return;
                        string line;
                        var noOfLines = 0;
                        LogData.SuspendUpdates();
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (e.Cancel) return;
                            parser.ParseLine(line);
                            noOfLines++;
                        }
                        Debug.WriteLine($"Added {noOfLines} lines");
                        LogData.ResumeUpdates();
                        _length = file.Length;
                        _position = file.Position;
                        if (!Settings.Instance.Live) return;
                        Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }
                }
            }
        }

    }
}