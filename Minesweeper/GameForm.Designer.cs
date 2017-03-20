// GameForm.Designer.cs

namespace Minesweeper
{
    public partial class GameForm
    {

        #region Data Members

        // Const Members

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblMines;

        // Event Members

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnHighScores;

        #endregion

        #region Ctor Dtor

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Components

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblMines = new System.Windows.Forms.Label();
            this.btnHighScores = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Location = new System.Drawing.Point(16, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 224);
            this.panel1.TabIndex = 0;
            // 
            // lblTimer
            // 
            this.lblTimer.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTimer.ForeColor = System.Drawing.Color.Red;
            this.lblTimer.Location = new System.Drawing.Point(211, 8);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(56, 23);
            this.lblTimer.TabIndex = 1;
            this.lblTimer.Text = "0";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(84, 8);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblMines
            // 
            this.lblMines.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblMines.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblMines.ForeColor = System.Drawing.Color.Red;
            this.lblMines.Location = new System.Drawing.Point(16, 8);
            this.lblMines.Name = "lblMines";
            this.lblMines.Size = new System.Drawing.Size(56, 23);
            this.lblMines.TabIndex = 3;
            this.lblMines.Text = "0";
            this.lblMines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnHighScores
            // 
            this.btnHighScores.Location = new System.Drawing.Point(105, 276);
            this.btnHighScores.Name = "btnHighScores";
            this.btnHighScores.Size = new System.Drawing.Size(75, 23);
            this.btnHighScores.TabIndex = 4;
            this.btnHighScores.Text = "High Scores";
            this.btnHighScores.UseVisualStyleBackColor = true;
            this.btnHighScores.Click += new System.EventHandler(this.btnHighScores_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(292, 311);
            this.Controls.Add(this.btnHighScores);
            this.Controls.Add(this.lblMines);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MineSweeper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #endregion

    } // Class
} // Namespace

