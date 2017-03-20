// Cell.cs

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Minesweeper
{

    /// -----------------------------------------------------------------------
    ///						Cell
    ///						----
    ///	
    /// This class extends Button and represents the cell of the game board that includes 
    /// Location of cell: x,y coordinates, number of mines around this cell, cell empty or have mine,
    /// cell is opened by the player already or doesn't, cell is flagged by the player already or doesn't.
    ///	
    /// -----------------------------------------------------------------------
    /// Programmer  : Dima Kolyas
    /// Student No  : 307504233
    /// Date	    : 20/03/2010
    /// -----------------------------------------------------------------------
    /// <summary>
    /// Represents the cell of the game board.
    /// </summary>

    public class Cell : Button
    {

        #region Data Members

        // Data Members

        private int m_nCoordinateX;             // Cell X Coordinate
        private int m_nCoordinateY;             // Cell Y Coordinate
        private bool m_bIsEmpty;                // Cell is empty
        private int m_nBombArround;             // Number of bombs arround the cell
        private bool m_bIsOpen;                 // Cell is open
        private bool m_bIsFlagged;              // Cell with flag

        #endregion

        #region Properties

        /// <summary>
        /// X coordinate get\set
        /// </summary>
        public int X
        {
            get
            {
                return (this.m_nCoordinateX);
            }
            set
            {
                this.m_nCoordinateX = value;
            }
        }

        /// <summary>
        /// Y coordinate get\set
        /// </summary>
        public int Y
        {
            get
            {
                return (this.m_nCoordinateY);
            }
            set
            {
                this.m_nCoordinateY = value;
            }
        }

        /// <summary>
        /// Cell is empty get\set
        /// </summary>
        public bool EMPTY
        {
            get
            {
                return (this.m_bIsEmpty);
            }
            set
            {
                this.m_bIsEmpty = value;
            }
        }

        /// <summary>
        /// Number of bombs arround get\set
        /// </summary>
        public int BOMBARROUND
        {
            get
            {
                return (this.m_nBombArround);
            }
            set
            {
                this.m_nBombArround = value;
            }
        }

        /// <summary>
        /// Cell is open get\set 
        /// </summary>
        public bool OPEN
        {
            get
            {
                return (this.m_bIsOpen);
            }
            set
            {
                this.m_bIsOpen = value;
            }
        }

        /// <summary>
        /// Cell is flagged get\set
        /// </summary>
        public bool FLAG
        {
            get
            {
                return (this.m_bIsFlagged);
            }
            set
            {
                this.m_bIsFlagged = value;
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Default c'tor
        /// </summary>
        public Cell()
        {
            this.m_nCoordinateX = 0;
            this.m_nCoordinateY = 0;
            this.m_bIsEmpty = true;
            this.m_nBombArround = 0;
            this.m_bIsOpen = false;
            this.m_bIsFlagged = false;
        }

        /// <summary>
        /// Explicit c'tor
        /// </summary>
        /// <param name="nCrdX">X coordinate of cell</param>
        /// <param name="nCrdY">Y coordinate of cell</param>
        /// <param name="bIsEmpty">Is cell empty</param>
        /// <param name="nBombArround">Number of bombs arround the cell</param>
        /// <param name="bIsOpen">Is cell open</param>
        /// <param name="bIsFlagged">Is cell flagged</param>
        public Cell(int nCrdX,
                    int nCrdY,
                    bool bIsEmpty,
                    int nBombArround,
                    bool bIsOpen,
                    bool bIsFlagged)
        {
            this.m_nCoordinateX = nCrdX;
            this.m_nCoordinateY = nCrdY;
            this.m_bIsEmpty = bIsEmpty;
            this.m_nBombArround = nBombArround;
            this.m_bIsOpen = bIsOpen;
            this.m_bIsFlagged = bIsFlagged;
        }

        #endregion

    } // class
} // namespace