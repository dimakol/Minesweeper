// MyFile.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Minesweeper
{

    /// -----------------------------------------------------------------------
    ///						MyFile
    ///						------
    ///	
    /// This class represents work with text file that includes 
    /// Saving into the file, Loading from the file.
    ///	
    /// -----------------------------------------------------------------------
    /// Programmer  : Dima Kolyas
    /// Student No  : 307504233
    /// Date	    : 20/03/2010
    /// -----------------------------------------------------------------------
    /// <summary>
    /// Represents work with text file.
    /// </summary>

    public class MyFile
    {

        #region Data Members

        // Data Members

        private string m_strFileName;           // File Name
        private string m_strBuffer;             // Buffer that saved to file or loaded from file

        #endregion

        #region Properties

        /// <summary>
        /// File Name get\set
        /// </summary>
        public string FILENAME
        {
            get
            {
                return (this.m_strFileName);
            }
            set
            {
                this.m_strFileName = value;
            }
        }

        /// <summary>
        /// File Buffer get\set
        /// </summary>
        public string BUFFER
        {
            get
            {
                return (this.m_strBuffer);
            }
            set
            {
                this.m_strBuffer = value;
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="strFileName">This will be the name of the file</param>
        public MyFile(string strFileName)
        {
            this.m_strFileName = strFileName;
            // Creating new txt file if he doesn't exist yet
            // Or open already exist txt file
            StreamWriter swCreateOrOpen = File.AppendText(this.m_strFileName);
            // Close the file
            swCreateOrOpen.Close();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Read from txt file
        /// </summary>
        /// <param name="lstLoad">List that will contain the readed contents of the file</param>
        public void Load(List<string> lstLoad)
        {
            // Open text file
            StreamReader srMyFile = File.OpenText(FILENAME);
            // Read line from text file
            BUFFER = srMyFile.ReadLine();
            // Not end of the file
            while (BUFFER != null)
            // Processing the info that was readed from the file
            // and read next line
            {
                // Add to list the info that was readed
                lstLoad.Add(BUFFER);

                // Read line from text file
                BUFFER = srMyFile.ReadLine();
            }
            // Close file
            srMyFile.Close();
        }

        /// <summary>
        /// Write to txt file
        /// </summary>
        /// <param name="strEntry">The info line to write</param>
        public void Save(string strEntry)
        {
            // Append text to the end of the file
            StreamWriter swMyFile = File.AppendText(FILENAME);
            // Put the info line to property
            BUFFER = strEntry;
            // Add new entry to the file
            swMyFile.WriteLine(BUFFER);
            // Close file
            swMyFile.Close();
        }

        #endregion

    } // class
} // namespace