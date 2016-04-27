using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Steganography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // sert up the file stream and the openfiledialog
            Stream myStream = null;
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            // if a file is selected from the fileDialog
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<byte[]> _possibleImages = null;

                    if ((myStream = ofd.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // send the file stream to the ByteArraySearch 
                            byte[] buffer = new byte[Convert.ToInt32(myStream.Length)];
                            myStream.Read(buffer, 0, Convert.ToInt32(myStream.Length) - 1);
                            _possibleImages = ByteArraySearch.Locate(buffer);
                        }
                    }

                    if (_possibleImages != null)
                    {
                        int _fileName = 11111110; // starting file name

                        foreach (byte[] _pImg in _possibleImages)
                        {
                            // write all the bytes of each possible image to an image file
                            File.WriteAllBytes(String.Format("{0}.jpeg", _fileName++), _pImg);
                            textBox1.AppendText(string.Format("File created: <<{0}.jpeg>> {1} bytes{2}", _fileName, _pImg.Length, Environment.NewLine));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        } // end button click
        
    }
}
