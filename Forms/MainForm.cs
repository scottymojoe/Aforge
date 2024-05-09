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
        VideoCaptureDevice _videoSource = null;
        MotionDetector _motionDetector = null;
        readonly System.Windows.Forms.Timer _resetTimer = new System.Windows.Forms.Timer();
        readonly Stopwatch _stopwatch = new Stopwatch();
        readonly Stopwatch _openStopwatch = new Stopwatch();
        readonly Color _startingColor = Color.FromKnownColor(KnownColor.Control);
        bool _shutDown = false;
        int _motionCount = 0;

        public MainForm()
        {
            InitializeComponent();
            _startingColor = _controlPanel.BackColor;
            StartCamera();

            _openStopwatch.Start();

            _stopwatch.Start();
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
        }

        private void ResetTimer_Tick(object sender, EventArgs e)
        {
            _resetTimer.Stop();
            BeginInvoke((MethodInvoker)delegate {
                bool lockTaken = false;
                try
                {
                    Monitor.TryEnter(_lock, ref lockTaken);
                    if (lockTaken)
                    {
                        _controlPanel.BackColor = _startingColor;
                        _leftBorder.BackColor = Color.Transparent;
                        _rightBorder.BackColor = Color.Transparent;
                        _bottomBorder.BackColor = Color.Transparent;
                        _motionCount = 0;
                    }
                    else
                    {
                        _thread1.Text = "Thread 1 lock was not taken, which means the object is currently locked by another thread.";
                    }
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(_lock);
                    }
                }
            });
        }

        void StartCamera()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0) throw new ApplicationException("No video devices found");
            try
            {
                _videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                _videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);
                _videoSource.Start();
                _motionDetector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionBorderHighlighting());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing camera: " + ex.Message);
            }
        }

        void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                if (_shutDown || _stopwatch.ElapsedMilliseconds < 500) return;
                _stopwatch.Restart();
                var bitmap = (Bitmap)eventArgs.Frame.Clone();
                Invoke((MethodInvoker)delegate
                {
                    bool lockTaken = false;
                    try
                    {
                        Monitor.TryEnter(_lock, ref lockTaken);
                        if (lockTaken)
                        {
                            _live.Image = bitmap;
                        }
                        else
                        {
                            _thread2.Text = "Thread 2 lock was not taken, which means the object is currently locked by another thread.";
                        }
                    }
                    finally
                    {
                        if (lockTaken)
                        {
                            Monitor.Exit(_lock);
                        }
                    }
                });
                float motionLevel = _motionDetector.ProcessFrame((Bitmap)eventArgs.Frame.Clone());
                if (motionLevel > 0.0001)
                {
                    var warningColor = Color.Yellow;
                    _motionCount++;
                    if (_motionCount > 4)
                    {
                        warningColor = Color.Red;
                    }
                    Invoke((MethodInvoker)delegate
                    {
                        bool lockTaken = false;
                        try
                        {
                            Monitor.TryEnter(_lock, ref lockTaken);
                            if (lockTaken)
                            {
                                if (_motionCount > 8)
                                {
                                    HideMainPanel();
                                }
                                _controlPanel.BackColor = warningColor;
                                _leftBorder.BackColor = warningColor;
                                _rightBorder.BackColor = warningColor;
                                _bottomBorder.BackColor = warningColor;
                                _resetTimer.Stop(); 
                                _resetTimer.Start();
                            }
                            else
                            {
                                _thread3.Text = "Thread 3 lock was not taken, which means the object is currently locked by another thread.";
                            }
                        }
                        finally
                        {
                            if (lockTaken)
                            {
                                Monitor.Exit(_lock);
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing frame: " + ex.Message);
            }
        }

        void HideMainPanel()
        {
            _mainPanel.Visible = false;
            _showParent.Focus();
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

        void PreClose()
        {
            _shutDown = true;
            _resetTimer.Stop();
            _stopwatch.Stop();
            StopCamera();
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
                _mainPanel.Visible = true;
                _showParent.Text = "";
            }
        }
    }
}
