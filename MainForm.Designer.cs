using System.Windows.Forms;

namespace MultiFaceRec
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelTrain = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.panelResults = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            this.panelMatches = new System.Windows.Forms.Panel();
            this.matchesGrid = new System.Windows.Forms.FlowLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.toggleTheme = new System.Windows.Forms.Button();
            this.headerLabel = new System.Windows.Forms.Label();

            this.panelTrain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.panelResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).BeginInit();
            this.panelMatches.SuspendLayout();
            this.matchesGrid.SuspendLayout();
            this.SuspendLayout();

            // Form Styling
            this.BackColor = System.Drawing.Color.FromArgb(34, 39, 46);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ClientSize = new System.Drawing.Size(1000, 770);
            this.Text = "FaceRec Pro";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Header
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(255, 107, 107);
            this.headerLabel.Location = new System.Drawing.Point(20, 10);
            this.headerLabel.Text = "Face Recognition Pro";

            // Theme Toggle
            this.toggleTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toggleTheme.BackColor = System.Drawing.Color.FromArgb(66, 73, 83);
            this.toggleTheme.ForeColor = System.Drawing.Color.White;
            this.toggleTheme.Location = new System.Drawing.Point(900, 10);
            this.toggleTheme.Size = new System.Drawing.Size(80, 30);
            this.toggleTheme.Text = "Light";
            this.toggleTheme.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toggleTheme.Click += new System.EventHandler(this.ToggleTheme_Click);
            this.toggleTheme.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 80, 30, 10, 10));

            // button2 (Add Face)
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.BackColor = System.Drawing.Color.FromArgb(88, 165, 255);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(20, 240);
            this.button2.Size = new System.Drawing.Size(220, 45);
            this.button2.Text = "Add Face";
            this.button2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 220, 45, 15, 15));

            // textBox1 (Name Input)
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(45, 51, 59);
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(20, 200);
            this.textBox1.Size = new System.Drawing.Size(220, 30);

            // panelTrain (Training Section)
            this.panelTrain.BackColor = System.Drawing.Color.FromArgb(45, 51, 59);
            this.panelTrain.Controls.Add(this.label1);
            this.panelTrain.Controls.Add(this.textBox1);
            this.panelTrain.Controls.Add(this.imageBox1);
            this.panelTrain.Controls.Add(this.button2);
            this.panelTrain.Location = new System.Drawing.Point(20, 50);
            this.panelTrain.Size = new System.Drawing.Size(260, 300);

            // label1 (Name Label)
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.label1.Location = new System.Drawing.Point(20, 180);
            this.label1.Text = "Name";

            // imageBox1 (Training Preview)
            this.imageBox1.BackColor = System.Drawing.Color.FromArgb(34, 39, 46);
            this.imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox1.Location = new System.Drawing.Point(20, 20);
            this.imageBox1.Size = new System.Drawing.Size(220, 150);

            // panelResults (Detection Section)
            this.panelResults.BackColor = System.Drawing.Color.FromArgb(45, 51, 59);
            this.panelResults.Controls.Add(this.label5);
            this.panelResults.Controls.Add(this.label4);
            this.panelResults.Controls.Add(this.label3);
            this.panelResults.Controls.Add(this.label2);
            this.panelResults.Controls.Add(this.button1);
            this.panelResults.Controls.Add(this.button3);
            this.panelResults.Location = new System.Drawing.Point(720, 50);
            this.panelResults.Size = new System.Drawing.Size(260, 300);

            // label5 (Persons Present Label)
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.label5.Location = new System.Drawing.Point(20, 20);
            this.label5.Text = "Recognized";

            // label4 (Names Display)
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(88, 165, 255);
            this.label4.Location = new System.Drawing.Point(20, 40);
            this.label4.Text = "None";

            // label3 (Face Count)
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(255, 107, 107);
            this.label3.Location = new System.Drawing.Point(200, 70);
            this.label3.Text = "0";

            // label2 (Face Count Label)
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.label2.Location = new System.Drawing.Point(20, 70);
            this.label2.Text = "Faces Detected";

            // button1 (Detect & Recognize)
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.BackColor = System.Drawing.Color.FromArgb(88, 165, 255);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(20, 140);
            this.button1.Size = new System.Drawing.Size(220, 45);
            this.button1.Text = "Detect & Recognize";
            this.button1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 220, 45, 15, 15));

            // button3 (Search Matches)
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.BackColor = System.Drawing.Color.FromArgb(88, 255, 130);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(20, 200);
            this.button3.Size = new System.Drawing.Size(220, 45);
            this.button3.Text = "Search Matches";
            this.button3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 220, 45, 15, 15));

            // imageBoxFrameGrabber (Camera Feed)
            this.imageBoxFrameGrabber.BackColor = System.Drawing.Color.FromArgb(34, 39, 46);
            this.imageBoxFrameGrabber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxFrameGrabber.Location = new System.Drawing.Point(300, 50);
            this.imageBoxFrameGrabber.Size = new System.Drawing.Size(400, 300);

            // panelMatches (Matching Images)
            this.panelMatches.BackColor = System.Drawing.Color.FromArgb(45, 51, 59);
            this.panelMatches.Controls.Add(this.matchesGrid);
            this.panelMatches.Location = new System.Drawing.Point(20, 360);
            this.panelMatches.Size = new System.Drawing.Size(960, 400);

            // matchesGrid (Grid for Search Results)
            this.matchesGrid.AutoScroll = true;
            this.matchesGrid.BackColor = System.Drawing.Color.FromArgb(45, 51, 59);
            this.matchesGrid.Location = new System.Drawing.Point(20, 20);
            this.matchesGrid.Size = new System.Drawing.Size(920, 360);
            this.matchesGrid.FlowDirection = FlowDirection.LeftToRight;
            this.matchesGrid.WrapContents = true;

            // Form Layout
            this.Controls.Add(this.headerLabel);
            this.Controls.Add(this.toggleTheme);
            this.Controls.Add(this.panelMatches);
            this.Controls.Add(this.panelResults);
            this.Controls.Add(this.panelTrain);
            this.Controls.Add(this.imageBoxFrameGrabber);

            this.panelTrain.ResumeLayout(false);
            this.panelTrain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.panelResults.ResumeLayout(false);
            this.panelResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).EndInit();
            this.panelMatches.ResumeLayout(false);
            this.matchesGrid.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panelTrain;
        private System.Windows.Forms.Label label1;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Panel panelResults;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber;
        private System.Windows.Forms.Panel panelMatches;
        private System.Windows.Forms.FlowLayoutPanel matchesGrid;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button toggleTheme;
        private System.Windows.Forms.Label headerLabel;
    }
}