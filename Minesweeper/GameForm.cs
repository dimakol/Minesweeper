// GameForm.cs

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Minesweeper
{

    /// ----------------------------------------------------------------------
    ///					GameForm
	///					--------
	///							
	/// General : This module implements the game itself.
	///	
	/// Input   : Get mouse click on the cell board from the user.
    ///           Name of the user if he win the game.
	///	
	/// Process : Checking if the cell is empty or have mine.
	///	
	/// Output  : The Board game.
    ///           Open the cell and show to the user if cell is empty (how many mines around)
    ///           or if the cell have a mine.
    ///           High Scores.
	///	
	/// ----------------------------------------------------------------------
    /// Programmer  : Dima Kolyas
    /// Student No  : 307504233
    /// Date	    : 20/03/2010
	/// ----------------------------------------------------------------------
	/// <summary>
    /// Implements the game itself.
	/// </summary>

    public partial class GameForm : Form
    {

        #region Data Members

        // Data Members

        public Cell[,] arrclBoard;                              // Array of Cells (Buttons)
        public int[,] arrnDirections = new int[,] { { -1, -1 },
                                                    { 0, -1 },
                                                    { 1, -1 },
                                                    { -1, 0 },
                                                    { 1, 0 },
                                                    { -1, 1 },
                                                    { 0, 1 },
                                                    { 1, 1 } }; // Array of Directions (8 Directions)
        private int nHeight=10, nWidth=10;                      // Size of the Board
        private int nMines = 10;                                // Number of Mines
        private int nFlags = 10;                                // Number of Flags
        private Timer tmrTime;                                  // Timer of the game
        private int nTime = 0;                                  // Time on timer   
        private int nBtnClick = 0;                              // Number of buttons click on the Board  
        private Form frmName;                                   // Form with the winner name
        private TextBox txtName;                                // TextBox that have the name
        private Stream myStreamFlag, myStreamMine;              // Streams of the images as embedded resource 
        private Stream myStreamMineFocused;                     // Streams of the images as embedded resource

        #endregion

        #region Ctor

        /// <summary>
        /// Default c'tor
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
            // Put the board of the game on the panel
            InitializeBoard();
            // Set number of mines on the label
            InitializeMinesLbl(this.nMines);
            // Load image resources
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            myStreamFlag = myAssembly.GetManifestResourceStream("Minesweeper.flag.bmp");
            myStreamMine = myAssembly.GetManifestResourceStream("Minesweeper.mine.bmp");
            myStreamMineFocused = myAssembly.GetManifestResourceStream("Minesweeper.minefocused.bmp");
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// The event that will happen on every tick(second) of the timer
        /// </summary>
        /// <param name="sender">Timer</param>
        /// <param name="e">Tick</param>
        private void t_Tick(object sender, EventArgs e)
        {
            // Refresh the timer
            this.Refresh();
            // Add one second to the time
            this.nTime++;
            // Show the time on the time label
            lblTimer.Text = this.nTime.ToString();
        }

        /// <summary>
        /// The event that will happen on every left button click on the board
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Button Click</param>
        private void ButtonClick(object sender, EventArgs e)
        {
            // The cell that was clicked
            Cell clCurrent = ((Cell)sender);

            // Increase the number of buttons click
            this.nBtnClick++;
            // First Click
            if (this.nBtnClick == 1)
            {
                // Start the timer
                InitializeTimer();
                // Place Mines on the board
                int nCount = PlaceMines(clCurrent.X, clCurrent.Y);
                //ShowMines();  // Debug
            }

            // Only if cell not opened yet
            // and not flagged yet perform
            if ((!clCurrent.OPEN) &&
               (!clCurrent.FLAG))
            {
                // Mark cell as Open
                clCurrent.OPEN = true;
                // Empty
                if (clCurrent.EMPTY)
                {
                    CountAround(clCurrent.X, clCurrent.Y);
                    // Empty cells arround
                    if (clCurrent.BOMBARROUND == 0)
                    {
                        // Start the recursion
                        Open(clCurrent.X, clCurrent.Y);
                        // Check if win the game
                        Win();
                    }
                    // Bomb arround
                    else
                    {
                        // print number of bombs arround
                        arrclBoard[clCurrent.Y, clCurrent.X].Text = Convert.ToString(this.arrclBoard[clCurrent.Y, clCurrent.X].BOMBARROUND);
                        // print the number in color
                        ColorNumbers(clCurrent.X, clCurrent.Y);
                        // Check if win the game
                        Win();
                    }
                }
                // Bomb Here
                else
                {
                    // Stop the timer
                    this.tmrTime.Stop();
                    // Show all mines
                    ShowMines();
                    // Focused Mine
                    this.arrclBoard[clCurrent.Y, clCurrent.X].BackgroundImage = Image.FromStream(myStreamMineFocused);
                    // Print Game Over
                    MessageBox.Show("Game Over");
                }
            }
        }

        /// <summary>
        /// The event that will happen on every right button click on the board
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Mouse click</param>
        private void btnMouseClick(object sender, MouseEventArgs e)
        {
            // Right click
            if (e.Button == MouseButtons.Right)
            {
                // The cell that was clicked
                Cell clCurrent = ((Cell)sender);

                // Not First Click
                if (this.nBtnClick > 0)
                {
                    // Only if cell not opened yet perform
                    if (!clCurrent.OPEN)
                    {
                        // Only if cell not flagged yet perform 
                        if (!clCurrent.FLAG)
                        {
                            // Put flag
                            this.arrclBoard[clCurrent.Y, clCurrent.X].BackgroundImage = Image.FromStream(myStreamFlag);
                            // Mark as flagged
                            clCurrent.FLAG = true;
                            // Decrease flags
                            this.nFlags--;
                            // Show on label
                            InitializeMinesLbl(this.nFlags);
                            // Check if win the game
                            Win();
                        }
                        else
                        // Cell flagged
                        {
                            // Show empty cell
                            this.arrclBoard[clCurrent.Y, clCurrent.X].BackgroundImage = null;
                            // Mark as unflagged
                            clCurrent.FLAG = false;
                            // Increase flags
                            this.nFlags++;
                            // Show on label
                            InitializeMinesLbl(this.nFlags);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Restart the game
        /// </summary>
        /// <param name="sender">Button for restart</param>
        /// <param name="e">Button Click</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            // Restart the game
            Application.Restart();
        }

        /// <summary>
        /// The event that will happen on every click on the accept button
        /// Saving the name and the time into the file
        /// </summary>
        /// <param name="sender">Button that accepting the name</param>
        /// <param name="e">Button Click Event</param>
        private void AcceptName(object sender, EventArgs e)
        {
            // Form Name not visible
            this.frmName.Visible = false;

            // Chaining the name with the time
            string strSave = this.txtName.Text + ";" + this.nTime.ToString();

            // Open file
            MyFile mfFile = new MyFile("Minesweeper.Highscores.txt");

            // Save to file
            mfFile.Save(strSave);

            // Show high score form
            ShowHighScore();
        }

        /// <summary>
        /// The event that will happen on every click on the button show high score
        /// </summary>
        /// <param name="sender">Button that shows the high score</param>
        /// <param name="e">Button Click Event</param>
        private void btnHighScores_Click(object sender, EventArgs e)
        {
            ShowHighScore();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Put the Buttons on the panel
        /// </summary>
        public void InitializeBoard()
        {
            // Size of the matrix
            this.arrclBoard = new Cell[this.nHeight, this.nWidth];
            // Run on all the height of the matrix
            for (int nRowIndex = 0; nRowIndex < this.nHeight; nRowIndex++)
            {
                // Run on all the width of the matrix
                for (int nColIndex = 0; nColIndex < this.nWidth; nColIndex++)
                {
                    // Create new cell
                    this.arrclBoard[nColIndex, nRowIndex] = new Cell();
                    // Save x coordinate of the cell
                    this.arrclBoard[nColIndex, nRowIndex].X = nRowIndex;
                    // Save y coordinate of the cell
                    this.arrclBoard[nColIndex, nRowIndex].Y = nColIndex;
                    // Event of left button click
                    this.arrclBoard[nColIndex, nRowIndex].Click += new EventHandler(ButtonClick);
                    // Event of right button click
                    this.arrclBoard[nColIndex, nRowIndex].MouseUp += new MouseEventHandler(btnMouseClick);
                    // Where to place the button on the panel
                    this.arrclBoard[nColIndex, nRowIndex].SetBounds(20 + nRowIndex * 20, 20 + nColIndex * 20, 20, 20);
                    // Add button to the panel
                    this.panel1.Controls.Add(this.arrclBoard[nColIndex, nRowIndex]);
                }
            }
        }

        /// <summary>
        /// Init the timer of the game
        /// </summary>
        public void InitializeTimer()
        {
            this.tmrTime = new Timer();
            this.tmrTime.Interval = 1000;
            this.tmrTime.Enabled = true;
            this.tmrTime.Start();
            this.tmrTime.Tick += new EventHandler(t_Tick);
        }

        /// <summary>
        /// Set number of mines on the label
        /// </summary>
        /// <param name="nlblMines">number of mines</param>
        public void InitializeMinesLbl(int nlblMines)
        {
            this.lblMines.Text = nlblMines.ToString();
        }

        /// <summary>
        /// Place mines randomly on the board
        /// </summary>
        /// <param name="nCrdX">X coordinate of cell that first clicked</param>
        /// <param name="nCrdY">Y coordinate of cell that first clicked</param>
        /// <returns>int. Number of mines that was placed on the board</returns>
        public int PlaceMines(int nCrdX,int nCrdY)
        {
            // Number of Mines that will be placed
            int nCount = 0;
            // Place Mines
            Random r = new Random();
            // Can place another mine
            while (nCount < this.nMines)
            {
                int nRandomX = r.Next(this.nWidth);
                int nRandomY = r.Next(this.nHeight);

                // Empty cell
                // and not the cell that was clicked first
                if ((this.arrclBoard[nRandomY, nRandomX].EMPTY == true) &&
                    ((nRandomX != nCrdX) ||
                    (nRandomY != nCrdY)))
                {
                    // Place Mine
                    this.arrclBoard[nRandomY, nRandomX].EMPTY = false;
                    nCount++;
                }
            }
            // Number of mines that was placed on the board
            return (nCount);
        }

        /// <summary>
        /// Show all the mines on the board
        /// </summary>
        public void ShowMines()
        {
            // Run on all the height of the matrix
            for (int nRowIndex = 0; nRowIndex < this.nHeight; nRowIndex++)
            {
                // Run on all the width of the matrix
                for (int nColIndex = 0; nColIndex < this.nWidth; nColIndex++)
                {
                    // Mine Here
                    if (!this.arrclBoard[nColIndex, nRowIndex].EMPTY)
                    {
                        // Show Mine
                        this.arrclBoard[nColIndex, nRowIndex].BackgroundImage = Image.FromStream(myStreamMine);
                    }
                    // Mark cell as Open
                    this.arrclBoard[nColIndex, nRowIndex].OPEN = true;
                }
            }
        }

        /// <summary>
        /// Color the cell number that define number of mines around this cell
        /// </summary>
        /// <param name="nCrdX">X coordinate of cell</param>
        /// <param name="nCrdY">Y coordinate of cell</param>
        public void ColorNumbers(int nCrdX, int nCrdY)
        {
            if (this.arrclBoard[nCrdY, nCrdX].BOMBARROUND == 1)
                this.arrclBoard[nCrdY, nCrdX].ForeColor = Color.Blue;

            if (this.arrclBoard[nCrdY, nCrdX].BOMBARROUND == 2)
                this.arrclBoard[nCrdY, nCrdX].ForeColor = Color.Green;

            if (this.arrclBoard[nCrdY, nCrdX].BOMBARROUND == 3)
                this.arrclBoard[nCrdY, nCrdX].ForeColor = Color.Red;

            if (this.arrclBoard[nCrdY, nCrdX].BOMBARROUND == 4)
                this.arrclBoard[nCrdY, nCrdX].ForeColor = Color.DarkBlue;

            if (this.arrclBoard[nCrdY, nCrdX].BOMBARROUND == 5)
                this.arrclBoard[nCrdY, nCrdX].ForeColor = Color.DarkRed;

            if (this.arrclBoard[nCrdY, nCrdX].BOMBARROUND == 6)
                this.arrclBoard[nCrdY, nCrdX].ForeColor = Color.CadetBlue;

            if (this.arrclBoard[nCrdY, nCrdX].BOMBARROUND == 7)
                this.arrclBoard[nCrdY, nCrdX].ForeColor = Color.Black;

            if (this.arrclBoard[nCrdY, nCrdX].BOMBARROUND == 8)
                this.arrclBoard[nCrdY, nCrdX].ForeColor = Color.Gray;
        }

        /// <summary>
        /// Check if win the game
        /// </summary>
        public void Win()
        {
            // Count how many cells is opened
            int nCountCell = 0;
            // Run on all the height of the matrix
            for (int nRowIndex = 0; nRowIndex < this.nHeight; nRowIndex++)
            {
                // Run on all the width of the matrix
                for (int nColIndex = 0; nColIndex < this.nWidth; nColIndex++)
                {
                    // Cell is open
                    if (this.arrclBoard[nColIndex, nRowIndex].OPEN)
                        // Increase the count
                        nCountCell++;
                }
            }

            // All cells opened without the mines
            // and all flags placed on the board
            if ((nCountCell == (this.nHeight * this.nWidth) - this.nMines) &&
                (this.nFlags == 0))
            // Player win the game
            {
                // Stop the timer
                this.tmrTime.Stop();
                // Print Congratulations
                MessageBox.Show("Congratulations");

                // New Form
                // Print Form of name enter
                this.frmName = new Form();
                Label lblName = new Label();
                this.txtName = new TextBox();
                this.txtName.MaxLength = 15;
                Button btnAccept = new Button();

                this.frmName.Text = "High Score";
                this.frmName.Size = new System.Drawing.Size(150, 125);
                lblName.Height = 30;
                lblName.Width = 150;
                lblName.Text = "\n       Enter your name: ";
                this.txtName.SetBounds(25, 40, 85, 20);
                btnAccept.SetBounds(45, 70, 50, 25);
                btnAccept.Text = "Accept";
                this.frmName.Controls.Add(lblName);
                this.frmName.Controls.Add(txtName);
                this.frmName.Controls.Add(btnAccept);
                this.frmName.MaximumSize = new System.Drawing.Size(150, 125);
                // Button click event
                btnAccept.Click += new EventHandler(AcceptName);
                // Show Form
                this.frmName.ShowDialog();               
            }
        }

        /// <summary>
        /// Count how many mines arround the cell and show the number on the cell
        /// </summary>
        /// <param name="nCrdX">X coordinate of cell</param>
        /// <param name="nCrdY">Y coordinate of cell</param>
        /// <returns>int. Number of mines that arround this cell</returns>
        public int CountAround(int nCrdX, int nCrdY)
        {
            // Number of mines that arround the specified cell
            int nCount = 0;

            // Run on the Dirrections Array
            for (int nCurrIndex = 0; nCurrIndex < 8; nCurrIndex++)
            {
                try
                {
                    // Check if nearest cell have mine
                    if (!this.arrclBoard[nCrdY + this.arrnDirections[nCurrIndex, 0], nCrdX + this.arrnDirections[nCurrIndex, 1]].EMPTY)
                    {
                        // Increase count
                        this.arrclBoard[nCrdY, nCrdX].BOMBARROUND++;
                        nCount++;
                    }
                }
                catch (System.Exception ex)
                {
                }
            }
            
            // Print the numbers when end the recursion
            // Arround at least one mine
            // and the current cell hasn't flag
            if ((nCount > 0) &&
                (!this.arrclBoard[nCrdY, nCrdX].FLAG))
            {
                // Print how many mines arround
                this.arrclBoard[nCrdY, nCrdX].Text = Convert.ToString(arrclBoard[nCrdY, nCrdX].BOMBARROUND);
                ColorNumbers(nCrdX, nCrdY);
                // Mark cell as Open 
                this.arrclBoard[nCrdY, nCrdX].OPEN = true;
            }

            // Number of mines that arround the specified cell
            return (nCount);
        }

        /// <summary>
        /// Open the cells with recursion
        /// </summary>
        /// <param name="nCrdX">X coordinate of cell</param>
        /// <param name="nCrdY">Y coordinate of cell</param>
        public void Open(int nCrdX, int nCrdY)
        {
            // Run on the Dirrections Array
            for (int nCurrIndex = 0; nCurrIndex < 8; nCurrIndex++)
            {
                try
                {
                    // Cell is empty and without flag
                    // Cell near not opened yet
                    // All the cells arround is empty
                    if ((this.arrclBoard[nCrdY, nCrdX].EMPTY) &&
                        (!this.arrclBoard[nCrdY, nCrdX].FLAG) &&
                        (!this.arrclBoard[nCrdY + this.arrnDirections[nCurrIndex, 1], nCrdX + this.arrnDirections[nCurrIndex, 0]].OPEN) &&
                        (CountAround(nCrdX + this.arrnDirections[nCurrIndex, 0], nCrdY + this.arrnDirections[nCurrIndex, 1]) == 0))
                    {
                        // Paint the background of the cell in gray
                        this.arrclBoard[nCrdY, nCrdX].BackColor = Color.Gray;
                        // Mark cell as Open
                        this.arrclBoard[nCrdY, nCrdX].OPEN = true;
                        this.arrclBoard[nCrdY, nCrdX].Enabled = false;
                        // Call the recursion to cell that arround
                        Open(nCrdX + this.arrnDirections[nCurrIndex, 0], nCrdY + this.arrnDirections[nCurrIndex, 1]);
                    }
                }
                catch (System.Exception ex)
                {
                }

                // Mark the last
                // The cell is empty and without flag
                if ((this.arrclBoard[nCrdY, nCrdX].EMPTY) &&
                    (!this.arrclBoard[nCrdY, nCrdX].FLAG))
                {
                    // Paint the background of the cell in gray
                    this.arrclBoard[nCrdY, nCrdX].BackColor = Color.Gray;
                    // Mark cell as Open
                    this.arrclBoard[nCrdY, nCrdX].OPEN = true;
                    this.arrclBoard[nCrdY, nCrdX].Enabled = false;
                }
            }
        }

        /// <summary>
        /// Shows in new form the high scores
        /// </summary>
        public void ShowHighScore()
        {
            // High Scores Form
            Form frmHighScores = new Form();
            frmHighScores.Text = "High Scores";

            Label lblName = new Label();
            Label lblTime = new Label();
            lblName.Text = "Name";
            lblTime.Text = "Time";
            lblName.SetBounds(0, 0, 50, 20);
            lblTime.SetBounds(120, 0, 50, 20);
            frmHighScores.Controls.Add(lblName);
            frmHighScores.Controls.Add(lblTime);

            // Open file
            MyFile mfFile = new MyFile("Minesweeper.Highscores.txt");

            // List to save there the loaded info from text file
            List<string> lstHighScores = new List<string>();
            // Load from file to list
            mfFile.Load(lstHighScores);

            // Location to put the labels
            int nLocationX = 0;

            // List For Each
            lstHighScores.ForEach(delegate(string strEntry)
            {
                string strName;             // Name from list
                string strTime;             // Time from list
                int nIndexOfSeperator;      // Index of ; in string
                // Find the index of last ; in readed line
                nIndexOfSeperator = strEntry.LastIndexOf(';');
                // Time
                strTime = strEntry.Substring(nIndexOfSeperator + 1);
                // Name
                strName = strEntry.Insert(nIndexOfSeperator, "\0");

                // Put on High Scores Form
                Label lblNameRead = new Label();
                Label lblTimeRead = new Label();
                lblNameRead.Text = strName;
                lblTimeRead.Text = strTime;
                lblNameRead.SetBounds(0, nLocationX + 20, 100, 20);
                lblTimeRead.SetBounds(120, nLocationX + 20, 50, 20);
                nLocationX += 20;
                frmHighScores.Controls.Add(lblNameRead);
                frmHighScores.Controls.Add(lblTimeRead);
            });

            // Show the High Scores Form
            frmHighScores.ShowDialog();
        }

        #endregion

    } // class
} // namespace