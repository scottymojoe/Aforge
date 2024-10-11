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
using System.Media;

namespace Forms
{
    public partial class MainForm : Form
    {
        object _lock = new object();
        BackgroundWorker _backgroundWorker = new BackgroundWorker();
        VideoCaptureDevice _videoSource = null;
        MotionDetector _motionDetector = null;
        readonly System.Windows.Forms.Timer _noMotionTimer = new System.Windows.Forms.Timer();
        readonly System.Windows.Forms.Timer _autoCloseTimer = new System.Windows.Forms.Timer();
        readonly Stopwatch _motionStopwatch = new Stopwatch();
        readonly Stopwatch _runningStopwatch = new Stopwatch();
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

            _runningStopwatch.Start();

            _noMotionTimer.Interval = 3000; // Set the timer interval to 3 seconds
            _noMotionTimer.Tick += NoMotionTimer_Tick;

            FormClosing += MainForm_FormClosing;

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += (sender, e) =>
            {
                _timeOpen.Text = _runningStopwatch.Elapsed.ToString(@"hh\:mm\:ss");
            };
            timer.Start();
            _showParent.Focus();

            _autoCloseTimer.Interval = 1000 * 60 * 15;
            _autoCloseTimer.Tick += (sender, e) =>
            {
                _stayOpen.Checked = false;
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
                while (true)
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
            StartCamera();
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
            _messages.Items.Insert(0, $"[{DateTime.Now}]: {message}");
        }

        void HideMainPanel()
        {
            if (_stayOpen.Checked) return;
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
            _noMotionTimer.Stop();
            _motionStopwatch.Stop();
            StopCamera();
        }

        void PlaySound()
        {
            if (!_mainPanel.Visible) return;
            SoundPlayer player = new SoundPlayer
            {
                SoundLocation = "C:\\Windows\\Media\\ding.wav"
            };
            player.LoadCompleted += (sender, e) =>
            {
                //Task.Run(() =>
                //{
                //    player.PlaySync();
                //    player.PlaySync();
                //    player.Dispose();
                //});
            };
            player.LoadAsync();
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
                            try
                            {
                                if (_shutDown) return;
                                if (_videoSource != null)
                                {
                                    using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                                    {
                                        var currentImage = _live.Image;
                                        if (currentImage != null)
                                        {
                                            currentImage.Dispose();
                                        }
                                        _live.Image = (Bitmap)bitmap.Clone();
                                        double motionLevel = _motionDetector.ProcessFrame(bitmap);
                                        motionLevel = Math.Round(motionLevel, 8);
                                        decimal decimalMotionLevel = (decimal)motionLevel;
                                        if (motionLevel > 0.00001)
                                        {
                                            _noMotionTimer.Stop();
                                            var warningColor = Color.DarkGreen;
                                            if (motionLevel > 0.0001 || _motionStopwatch.IsRunning)
                                            {
                                                warningColor = Color.Yellow;
                                                if (!_motionStopwatch.IsRunning)
                                                {
                                                    _motionStopwatch.Start();
                                                    PlaySound();
                                                }
                                                _motionCount++;
                                            }
                                            AppendMessage($"Bitmap event {_motionCount} detected at level ({decimalMotionLevel}).");
                                            if (_motionStopwatch.Elapsed >= TimeSpan.FromSeconds(2))
                                            {
                                                warningColor = Color.Red;
                                            }
                                            if (_motionStopwatch.Elapsed >= TimeSpan.FromSeconds(4) || motionLevel > .001)
                                            {
                                                HideForm();
                                                if (_motionStopwatch.Elapsed >= TimeSpan.FromSeconds(4))
                                                {
                                                    AppendMessage($"Taking elapsed time related measures at second {_motionStopwatch.Elapsed.TotalSeconds} and level {decimalMotionLevel}.");
                                                }
                                                else if (motionLevel > .001)
                                                {
                                                    AppendMessage($"Taking level related measures at level {decimalMotionLevel} and second {_motionStopwatch.Elapsed.TotalSeconds}.");
                                                }
                                                _noMotionTimer.Start();
                                                return;
                                            }
                                            _controlPanel.BackColor = warningColor;
                                            _leftBorder.BackColor = warningColor;
                                            _rightBorder.BackColor = warningColor;
                                            _bottomBorder.BackColor = warningColor;
                                            if (!_keep.Checked && ActiveForm != this)
                                            {
                                                HideMainPanel();
                                            }
                                            _noMotionTimer.Start();
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                AppendMessage("Error processing frame: " + ex.Message);
                            }
                        }
                        else
                        {
                            Trace.TraceError("Thread 2 lock was not taken, which means the object is currently locked by another thread.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError("Error processing frame: " + ex.Message);
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
            if (_showParent.Text == "qrz")
            {
                ResetMotionTimer();
                _placeholder.Visible = false;
                _outputStopwatch.Stop();
                _mainPanel.Visible = true;
                _showParent.Text = "";
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

        void NoMotionTimer_Tick(object sender, EventArgs e)
        {
            _motionStopwatch.Reset();
            _noMotionTimer.Stop();
            _controlPanel.BackColor = _startingColor;
            _leftBorder.BackColor = Color.Transparent;
            _rightBorder.BackColor = Color.Transparent;
            _bottomBorder.BackColor = Color.Transparent;
            _motionCount = 0;
        }

        private void ClearMessages_Click(object sender, EventArgs e)
        {
            _messages.Items.Clear();
        }

        private void _resetMotionTimer_Click(object sender, EventArgs e)
        {
            ResetMotionTimer();
        }

        void ResetMotionTimer()
        {
            _autoCloseTimer.Stop();
            _autoCloseTimer.Start();
            _motionStopwatch.Reset();
            PlaySound();
        }
    }
}
