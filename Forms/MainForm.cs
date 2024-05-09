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
        readonly Color _startingColor = Color.FromKnownColor(KnownColor.Control);
        bool _shutDown = false;
        int _motionCount = 0;

        public MainForm()
        {
            InitializeComponent();
            _startingColor = _controlPanel.BackColor;
            StartCamera();

            _stopwatch.Start();
            _resetTimer.Interval = 3000; // Set the timer interval to 3 seconds
            _resetTimer.Tick += ResetTimer_Tick;
            FormClosing += MainForm_FormClosing;
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
                        _pictureBox.Visible = false;
                        _controlPanel.BackColor = _startingColor;
                        _motionCount = 0;
                        _keepOpen.Checked = false;
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
                        // Always release the lock if you were able to take it.
                        Monitor.Exit(_lock);
                    }
                }
            });
        }

        void StartCamera()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
                throw new ApplicationException("No video devices found");

            try
            {
                _videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                _videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);
                _videoSource.Start();

                _motionDetector = new MotionDetector(
                    new TwoFramesDifferenceDetector(),
                    new MotionBorderHighlighting());

                Console.WriteLine("Camera started successfully.");
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
                if (_shutDown || _stopwatch.ElapsedMilliseconds < 500)
                    return;
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
                            if (!_hideLive.Checked)
                            {
                                _live.Image = bitmap;
                                _live.Visible = true;
                            }
                            else
                            {
                                _live.Visible = false;
                            }
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
                            // Always release the lock if you were able to take it.
                            Monitor.Exit(_lock);
                        }
                    }
                });
                float motionLevel = _motionDetector.ProcessFrame((Bitmap)eventArgs.Frame.Clone());
                if (motionLevel > 0.0001) // Adjust sensitivity as needed
                {
                    var warningColor = Color.Yellow;
                    _motionCount++;
                    if (_motionCount > 4)
                    {
                        warningColor = Color.Red;
                    }
                    // Display the video in the PictureBox control
                    BeginInvoke((MethodInvoker)delegate
                    {
                        bool lockTaken = false;
                        try
                        {
                            Monitor.TryEnter(_lock, ref lockTaken);
                            if (lockTaken)
                            {
                                if (_motionCount > 8 && !_keepOpen.Checked)
                                {
                                    _mainPanel.Visible = false;
                                }
                                _controlPanel.BackColor = warningColor;
                                _pictureBox.Image = bitmap;
                                if (!_alwaysHide.Checked)
                                {
                                    _pictureBox.Visible = true;
                                }
                                _resetTimer.Stop(); // Stop the timer if it's already running
                                _resetTimer.Start(); // Start the timer
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
                                // Always release the lock if you were able to take it.
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

        void StopCamera()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();

                // Stop the video source in a separate task
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

        void CloseForm()
        {
            PreClose();
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PreClose();
        }

        private void Show_Click(object sender, EventArgs e)
        {
            _pictureBox.Visible = true;
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            _pictureBox.Visible = false;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            CloseForm();
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
