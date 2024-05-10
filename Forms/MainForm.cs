using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;
using AForge.Vision.Motion;
using System.Diagnostics;
using System.Threading;

namespace Forms
{
    public partial class MainForm : Form
    {
        object _lock = new object();
        BackgroundWorker _backgroundWorker = new BackgroundWorker();
        VideoCaptureDevice _videoSource = null;
        MotionDetector _motionDetector = null;
        readonly System.Windows.Forms.Timer _resetTimer = new System.Windows.Forms.Timer();
        readonly System.Windows.Forms.Timer _autoCloseTimer = new System.Windows.Forms.Timer();
        readonly Stopwatch _stopwatch = new Stopwatch();
        readonly Stopwatch _openStopwatch = new Stopwatch();
        readonly Stopwatch _outputStopwatch = new Stopwatch();
        readonly Color _startingColor = Color.FromKnownColor(KnownColor.Control);
        bool _shutDown = false;
        int _motionCount = 0;

        public MainForm()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Application.Exit();
            };

            HideMainPanel();

            Size = new Size(350, 555);

            _messages.Text = "";

            _startingColor = _controlPanel.BackColor;
            StartCamera();

            _stopwatch.Start();
            _openStopwatch.Start();
            
            _resetTimer.Interval = 3000; // Set the timer interval to 3 seconds
            _resetTimer.Tick += ResetTimer_Tick;

            FormClosing += MainForm_FormClosing;

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += (sender, e) =>
            {
                _timeOpen.Text = _openStopwatch.Elapsed.ToString(@"hh\:mm\:ss");
            };
            timer.Start();
            _showParent.Focus();

            _autoCloseTimer.Interval = 1000 * 60 * 15;
            _autoCloseTimer.Tick += (sender, e) =>
            {
                HideForm();
                AppendMessage($"Auto close event detected. Taking measures.");
            };
            Activated += (sender, e) =>
            {
                _autoCloseTimer.Stop();
                _autoCloseTimer.Start();
                _showParent.Focus();
            };

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.DoWork += (sender, e) =>
            {
                while(true)
                {
                    _backgroundWorker.ReportProgress(0);
                    Thread.Sleep(5000);
                }
            };
            _backgroundWorker.ProgressChanged += (sender, e) =>
            {
                if (_outputStopwatch.IsRunning)
                {
                    if (_outputStopwatch.Elapsed == TimeSpan.FromMinutes(30)) 
                    {
                        _output.Text = "";
                    }
                    Random random = new Random();
                    int randomNumber = random.Next();
                    if (randomNumber % 2 == 0)
                    {
                        if (_output.Text == "" || _output.Text == _output.Name)
                        {
                            _output.Text = $"[{DateTime.Now}]: Starting...{Environment.NewLine}";
                        }
                        else
                        {
                            _output.Text += $"[{DateTime.Now}]: {randomNumber}{Environment.NewLine}";
                        }
                        _placeholder.VerticalScroll.Value = _placeholder.VerticalScroll.Maximum;
                    }
                }
            };
            _backgroundWorker.RunWorkerAsync();
        }

        bool TryAquireLock(int maxAttempts)
        {
            bool lockAcquired = false;
            for (int i = 0; i < maxAttempts; i++)
            {
                Monitor.TryEnter(_lock, ref lockAcquired);
                if (lockAcquired)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            return lockAcquired;
        }

        void ReleaseLock(bool lockAquired)
        {
            if (lockAquired)
            {
                Monitor.Exit(_lock);
            }
        }

        void StartCamera()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0) AppendMessage("No devices found.");
            try
            {
                _videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                _videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);
                _videoSource.Start();
                _motionDetector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionBorderHighlighting());
                AppendMessage("Initialized device.");
            }
            catch (Exception ex)
            {
                AppendMessage("Error initializing device: " + ex.Message);
            }
        }

        void StopCamera()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                Task.Run(() =>
                {
                    if (_videoSource != null && _videoSource.IsRunning)
                    {
                        _videoSource.WaitForStop();
                    }
                });
                _videoSource = null;
                _motionDetector = null;
            }
        }

        void AppendMessage(string message)
        {
            if (_shutDown) return;
            var currentText = _messages.Text;
            _messages.Text = $"[{DateTime.Now}]: {message}{Environment.NewLine}{currentText}";
        }

        void HideMainPanel()
        {
            _mainPanel.Visible = false;
            _placeholder.Dock = DockStyle.Fill;
            _placeholder.Visible = true;
            _placeholder.BringToFront();
            _outputStopwatch.Start();
            _showParent.Focus();
        }

        void HideForm()
        {
            HideMainPanel();
        }

        void PreClose()
        {
            _shutDown = true;
            _resetTimer.Stop();
            _stopwatch.Stop();
            StopCamera();
        }

        void ResetTimer_Tick(object sender, EventArgs e)
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(_lock, ref lockTaken);
                if (lockTaken)
                {
                    _resetTimer.Stop();
                    _controlPanel.BackColor = _startingColor;
                    _leftBorder.BackColor = Color.Transparent;
                    _rightBorder.BackColor = Color.Transparent;
                    _bottomBorder.BackColor = Color.Transparent;
                    _motionCount = 0;
                }
                else
                {
                    AppendMessage("Thread 1 lock was not taken, which means the object is currently locked by another thread.");
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_lock);
                }
            }
        }

        void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    bool lockAquired = TryAquireLock(5);
                    try
                    {
                        if (lockAquired)
                        {
                            if (_shutDown || _stopwatch.ElapsedMilliseconds < 500) return;
                            _stopwatch.Restart();
                            if (_videoSource != null)
                            {
                                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                                {
                                    _live.Image = (Bitmap)bitmap.Clone();
                                    float motionLevel = _motionDetector.ProcessFrame((Bitmap)bitmap.Clone());
                                    if (motionLevel > 0.0001)
                                    {
                                        var warningColor = Color.Yellow;
                                        _motionCount++;
                                        AppendMessage($"Sequenced event {_motionCount} detected.");
                                        if (_motionCount > 4)
                                        {
                                            warningColor = Color.Red;
                                        }
                                        if (_motionCount > 8)
                                        {
                                            HideForm();
                                            AppendMessage($"Sequenced event {_motionCount} detected. Taking measures.");
                                            return;
                                        }
                                        _controlPanel.BackColor = warningColor;
                                        _leftBorder.BackColor = warningColor;
                                        _rightBorder.BackColor = warningColor;
                                        _bottomBorder.BackColor = warningColor;
                                        _resetTimer.Stop();
                                        _resetTimer.Start();
                                        if (!_keep.Checked && ActiveForm != this)
                                        {
                                            HideMainPanel();
                                        }
                                        BringToFront();
                                        TopMost = true;
                                        if (!_top.Checked)
                                        {
                                            TopMost = false;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Trace.TraceError("Thread 2 lock was not taken, which means the object is currently locked by another thread.");
                        }
                    }
                    finally
                    {
                        ReleaseLock(lockAquired);
                    }
                });
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error processing frame: " + ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PreClose();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            HideMainPanel();
        }

        private void ShowParent_TextChanged(object sender, EventArgs e)
        {
            var lockAquired = TryAquireLock(5);
            if (lockAquired)
            {
                if (_showParent.Text == "qrz")
                {
                    _placeholder.Visible = false;
                    _outputStopwatch.Stop();
                    _mainPanel.Visible = true;
                    _showParent.Text = "";
                }
            }
        }

        private void Top_CheckedChanged(object sender, EventArgs e)
        {
            if (_top.Checked)
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
        }

        private void Messages_DoubleClick(object sender, EventArgs e)
        {
            _messages.Text = "";
        }
    }
}
