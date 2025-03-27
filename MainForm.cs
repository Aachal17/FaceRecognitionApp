using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Drawing.Drawing2D;

namespace MultiFaceRec
{
    public partial class FrmPrincipal : Form
    {
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;
        string databasePath = Path.Combine(Application.StartupPath, "FaceDatabase");
        string photoFolderPath = @"C:\Users\Aachal\Source\Repos\Face-Detection-Clone\FaceRecProOV\EventImages";
        Image<Gray, byte> lastDetectedFace = null;
        private bool isDarkTheme = true;

        public FrmPrincipal()
        {
            InitializeComponent();
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt");
                string[] Labels = Labelsinfo.Split('%');
                NumLabels = Convert.ToInt16(Labels[0]);
                ContTrain = NumLabels;
                for (int tf = 1; tf < NumLabels + 1; tf++)
                {
                    string LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces/" + LoadFaces));
                    labels.Add(Labels[tf]);
                }
            }
            catch
            {
                MessageBox.Show("No trained faces in database. Add a face to proceed.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (!Directory.Exists(databasePath)) Directory.CreateDirectory(databasePath);
            if (!Directory.Exists(photoFolderPath)) Directory.CreateDirectory(photoFolderPath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            grabber = new Capture();
            grabber.QueryFrame();
            Application.Idle += new EventHandler(FrameGrabber);
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Enter a name to add face.");
                return;
            }

            ContTrain++;
            gray = grabber.QueryGrayFrame().Resize(320, 240, INTER.CV_INTER_CUBIC);
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2, 10, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

            if (facesDetected[0].Length > 0)
            {
                TrainedFace = currentFrame.Copy(facesDetected[0][0].rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labels.Add(textBox1.Text);
                imageBox1.Image = TrainedFace;

                string fileName = Path.Combine(databasePath, $"{textBox1.Text}_{DateTime.Now.Ticks}.bmp");
                TrainedFace.Save(fileName);

                File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.Count + "%");
                for (int i = 1; i <= trainingImages.Count; i++)
                {
                    trainingImages[i - 1].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
                    File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", labels[i - 1] + "%");
                }

                MessageBox.Show(textBox1.Text + "'s face added.", "Training OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No face detected. Try again.", "Training Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void FrameGrabber(object sender, EventArgs e)
        {
            label3.Text = "0";
            NamePersons.Clear();
            currentFrame = grabber.QueryFrame().Resize(320, 240, INTER.CV_INTER_CUBIC);
            gray = currentFrame.Convert<Gray, Byte>();
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2, 10, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

            foreach (MCvAvgComp f in facesDetected[0])
            {
                t++;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                currentFrame.Draw(f.rect, new Bgr(Color.Green), 2);

                if (trainingImages.Count > 0)
                {
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 5000, ref termCrit);
                    name = recognizer.Recognize(result);
                    currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.Red));
                    lastDetectedFace = result.Clone();
                }

                NamePersons.Add(name);
            }

            label3.Text = facesDetected[0].Length.ToString();
            names = string.Join(", ", NamePersons);
            imageBoxFrameGrabber.Image = currentFrame;
            label4.Text = names;
            names = "";
            NamePersons.Clear();
            t = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lastDetectedFace == null)
            {
                MessageBox.Show("No face detected. Scan a face first.", "Search Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (trainingImages.Count == 0)
            {
                MessageBox.Show("No trained faces available.", "Search Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
            EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 5000, ref termCrit);
            string recognizedName = recognizer.Recognize(lastDetectedFace);

            if (!string.IsNullOrEmpty(recognizedName))
            {
                SearchAndDisplayMatches(recognizedName);
            }
            else
            {
                matchesGrid.Controls.Clear();
                MessageBox.Show("No match found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SearchAndDisplayMatches(string name)
        {
            string[] photoFiles = Directory.GetFiles(photoFolderPath, "*.bmp");
            List<Image<Bgr, byte>> matchingImages = new List<Image<Bgr, byte>>();

            foreach (string file in photoFiles)
            {
                try
                {
                    using (Image<Bgr, byte> img = new Image<Bgr, byte>(file))
                    {
                        Image<Gray, byte> grayImg = img.Convert<Gray, byte>();
                        MCvAvgComp[][] facesDetected = grayImg.DetectHaarCascade(face, 1.1, 5, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

                        foreach (MCvAvgComp f in facesDetected[0])
                        {
                            Image<Gray, byte> faceImg = grayImg.Copy(f.rect).Resize(100, 100, INTER.CV_INTER_CUBIC);
                            MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
                            EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 5000, ref termCrit);
                            if (recognizer.Recognize(faceImg) == name)
                            {
                                matchingImages.Add(img.Clone());
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing {Path.GetFileName(file)}: {ex.Message}", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            matchesGrid.Controls.Clear();

            if (matchingImages.Count > 0)
            {
                try
                {
                    int imageSize = 150;
                    foreach (var img in matchingImages)
                    {
                        Panel imagePanel = new Panel
                        {
                            Size = new Size(imageSize + 10, imageSize + 50),
                            BackColor = Color.Transparent
                        };

                        Bitmap originalBitmap = img.ToBitmap(); // Convert to Bitmap once
                        Bitmap roundedBitmap = CreateRoundedImage(originalBitmap, imageSize, imageSize, 20);

                        PictureBox pb = new PictureBox
                        {
                            Size = new Size(imageSize, imageSize),
                            Image = roundedBitmap,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Location = new Point(5, 5),
                            Tag = originalBitmap // Store original Bitmap for download
                        };

                        Button downloadBtn = new Button
                        {
                            Text = "Download",
                            FlatStyle = FlatStyle.Flat,
                            BackColor = System.Drawing.Color.FromArgb(88, 165, 255),
                            ForeColor = Color.White,
                            Size = new Size(80, 30),
                            Location = new Point((imageSize - 80) / 2, imageSize + 10),
                            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold)
                        };
                        downloadBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 80, 30, 10, 10));
                        downloadBtn.Click += (s, ev) => DownloadImage((Bitmap)pb.Tag, name); // Use Tag for original Bitmap

                        imagePanel.Controls.Add(pb);
                        imagePanel.Controls.Add(downloadBtn);
                        matchesGrid.Controls.Add(imagePanel);

                        img.Dispose(); // Dispose Emgu.CV image
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error displaying images: {ex.Message}", "Display Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No matching images found in EventImages.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Bitmap CreateRoundedImage(Bitmap original, int width, int height, int cornerRadius)
        {
            Bitmap roundedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(roundedImage))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(width - cornerRadius, height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                path.AddArc(0, height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                path.CloseFigure();

                g.SetClip(path);
                g.DrawImage(original, 0, 0, width, height);
            }
            return roundedImage;
        }

        private void DownloadImage(Bitmap image, string name)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "BMP Files (*.bmp)|*.bmp";
                sfd.FileName = $"{name}_{DateTime.Now.Ticks}.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    image.Save(sfd.FileName); // Save the Bitmap directly
                    MessageBox.Show("Image downloaded successfully!", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ToggleTheme_Click(object sender, EventArgs e)
        {
            if (isDarkTheme)
            {
                this.BackColor = System.Drawing.Color.FromArgb(240, 240, 245);
                panelTrain.BackColor = System.Drawing.Color.White;
                panelResults.BackColor = System.Drawing.Color.White;
                panelMatches.BackColor = System.Drawing.Color.White;
                matchesGrid.BackColor = System.Drawing.Color.White;
                imageBox1.BackColor = System.Drawing.Color.FromArgb(240, 240, 245);
                imageBoxFrameGrabber.BackColor = System.Drawing.Color.FromArgb(240, 240, 245);
                textBox1.BackColor = System.Drawing.Color.White;
                textBox1.ForeColor = System.Drawing.Color.Black;
                label1.ForeColor = System.Drawing.Color.Black;
                label2.ForeColor = System.Drawing.Color.Black;
                label5.ForeColor = System.Drawing.Color.Black;
                toggleTheme.Text = "Dark";
            }
            else
            {
                this.BackColor = System.Drawing.Color.FromArgb(34, 39, 46);
                panelTrain.BackColor = System.Drawing.Color.FromArgb(45, 51, 59);
                panelResults.BackColor = System.Drawing.Color.FromArgb(45, 51, 59);
                panelMatches.BackColor = System.Drawing.Color.FromArgb(45, 51, 59);
                matchesGrid.BackColor = System.Drawing.Color.FromArgb(45, 51, 59);
                imageBox1.BackColor = System.Drawing.Color.FromArgb(34, 39, 46);
                imageBoxFrameGrabber.BackColor = System.Drawing.Color.FromArgb(34, 39, 46);
                textBox1.BackColor = System.Drawing.Color.FromArgb(45, 51, 59);
                textBox1.ForeColor = System.Drawing.Color.White;
                label1.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
                label2.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
                label5.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
                toggleTheme.Text = "Light";
            }
            isDarkTheme = !isDarkTheme;
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
    }
}