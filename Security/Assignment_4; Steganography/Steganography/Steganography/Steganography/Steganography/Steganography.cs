/*****************************************************************************
 * Project:		    Steganography
 * 
 * Student Names:	Jordan Thompson and Kyle Thomson
 * 
 * File Name:		Steganography.cs
 *
 * Date:			March 31, 2016
 *
 * Description:	    The purpose of the file is to model a steganography application
 *                  that hides text in images and extracts text from images.
 *****************************************************************************/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Steganography
{
    public partial class Steganography : Form
    {
        private Bitmap bitmap = null;

        public Steganography()
        {
            InitializeComponent();
        }

        /*****************************************************************************
         *  Function:       btnOpenImage_Click
         *  
         *  Description:    Opens up a OpenFileDialog for the User to select an Image
         *                  file and displays it in the pictureBox.
         *****************************************************************************/
        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            //Create openDialog and specify it's filter (for images only)
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Images Files (*.jpg; *.png; *.bmp) | *.jpg; *.png; *.bmp";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                //Sets the image to the pictureBox
                pBImage.Image = Image.FromFile(openDialog.FileName);

                //Enables other form buttons
                btnExtractText.Enabled = true;
                btnHideText.Enabled = true;

                //Clears both text boxes
                txtHiddenText.Clear();
                txtExtractedText.Clear();
            }
        }

        /*****************************************************************************
         *  Function:       btnHideText_Click
         *  
         *  Description:    Grabs the currently loaded image and text, and if there is
         *                  text, then it embeds the text in the image and saves that
         *                  image.
         *****************************************************************************/
        private void btnHideText_Click(object sender, EventArgs e)
        {
            //Get Bitmap image and text
            bitmap = (Bitmap)pBImage.Image;
            string hideText = txtHiddenText.Text;

            //Ensure text is not blank
            if (hideText == "")
            {
                MessageBox.Show("You need to enter some text. You can't hide nothing.");
            }
            else
            {
                HideText(hideText);
                SaveImage();
            }
        }
        
        /*****************************************************************************
         *  Function:       HideText
         *  
         *  Description:    This function embeds text into a bitmap image. It stores
         *                  each character of the text in 8 bits which are spread across
         *                  the LSB of the pixel RGB elements.
         *****************************************************************************/
        public void HideText(string text)
        {
            //Tracking states
            bool hideMode = true;   //Hiding Text State
            bool breakOut = false;  //Break out of Loops State

            //Character Index and Value
            int cIndex = 0;
            int cValue = 0;

            //Pixel Element Index and Elements
            long elementIndex = 0;
            int elementR = 0;
            int elementG = 0;
            int elementB = 0;
                        
            //Loop through each pixel
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    //Gets the current pixel
                    Color pixel = bitmap.GetPixel(j, i);

                    //Clears the LSB from each pixel element
                    elementR = pixel.R - pixel.R % 2;
                    elementG = pixel.G - pixel.G % 2;
                    elementB = pixel.B - pixel.B % 2;

                    //Loop through each element in the pixel
                    for (int k = 0; k < 3; k++)
                    {                   
                        //Checks if 8 bits has been processed
                        if (elementIndex % 8 == 0)
                        {
                            if (!hideMode)
                            {
                                //Sets the last pixel on the image
                                if ((elementIndex - 1) % 3 < 2)
                                {
                                    bitmap.SetPixel(j, i, Color.FromArgb(elementR, elementG, elementB));
                                }

                                breakOut = true;
                                break;
                            }

                            //Check if the text has all been hidden
                            if (cIndex >= text.Length)
                            {
                                hideMode = false;
                            }
                            else //Move on to next character 
                            {
                                
                                cValue = text[cIndex++];
                            }
                        }

                        //Checking which LSB to change in a specific element
                        if (elementIndex % 3 == 0) //R
                        {
                            if (hideMode)
                            {
                                //Add the rightmost bit to the LSB 
                                elementR += cValue % 2;

                                //Removes the rightmost bit since it was just added
                                cValue /= 2;
                            }
                        }
                        else if (elementIndex % 3 == 1) //G
                        {
                            if (hideMode)
                            {
                                //Add the rightmost bit to the LSB 
                                elementG += cValue % 2;

                                //Removes the rightmost bit since it was just added
                                cValue /= 2;
                            }
                        }
                        else if (elementIndex % 3 == 2) //B
                        {
                            if (hideMode)
                            {
                                //Add the rightmost bit to the LSB 
                                elementB += cValue % 2;

                                //Removes the rightmost bit since it was just added
                                cValue /= 2;
                            }

                            //Sets the pixel since all element LSBs have been assigned
                            bitmap.SetPixel(j, i, Color.FromArgb(elementR, elementG, elementB));
                        }
                        
                        elementIndex++;
                    }
                    //Break out of the J / Width Loop
                    if (breakOut)
                    {
                        break;
                    }
                }
                //Break out of the I / Height Loop
                if (breakOut)
                {
                    break;
                }
            }
        }

        /*****************************************************************************
         *  Function:       SaveImage
         *  
         *  Description:    Opens up a SaveFileDialog for the User to select an image
         *                  type to save their embedded image.
         *****************************************************************************/
        private void SaveImage()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Png|*.png|Bitmap|*.bmp";
            ImageFormat imgFormat = ImageFormat.Png;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string imgExtension = Path.GetExtension(saveDialog.FileName);

                if (imgExtension == ".png")
                {
                    imgFormat = ImageFormat.Png;
                }
                else if (imgExtension == ".bmp")
                {
                    imgFormat = ImageFormat.Bmp;
                }

                bitmap.Save(saveDialog.FileName, imgFormat);
            }
        }

        /*****************************************************************************
        *  Function:       btnExtractText_Click
        *  
        *  Description:    Grabs the currently loaded image and extracts the text from
        *                  that image.
        *****************************************************************************/
        private void btnExtractText_Click(object sender, EventArgs e)
        {
            bitmap = (Bitmap)pBImage.Image;

            txtExtractedText.Text = ExtractText();
        }

        /*****************************************************************************
        *  Function:        ExtractText
        *  
        *  Description:     This function extracts text from a bitmap image. It constructs
        *                   characters from each pixel element's LSB. Each of these
        *                   character then constructs a string.
        *****************************************************************************/
        public string ExtractText()
        {
            string extractedText = "";
            bool breakOut = false;

            int index = 0;
            int cValue = 0;

            //Loop through each pixel
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color pixel = bitmap.GetPixel(j, i);

                    //Loop through each element in the pixel
                    for (int k = 0; k < 3; k++)
                    {
                        //Adds the LSB of the pixel element to the cValue right bit
                        if (index % 3 == 0) //R
                        {
                            //Adding a bit to the right
                            cValue = cValue * 2;
                            
                            //Replacing that bit with the element's LSB
                            cValue += pixel.R % 2;
                        }
                        else if (index % 3 == 1) //G
                        {
                            //Adding a bit to the right
                            cValue = cValue * 2;

                            //Replacing that bit with the element's LSB
                            cValue += pixel.G % 2;
                        }
                        else if (index % 3 == 2) //B
                        {
                            //Adding a bit to the right
                            cValue = cValue * 2;

                            //Replacing that bit with the element's LSB
                            cValue += pixel.B % 2;
                        }

                        index++;

                        //If 8 bits have been processed, then modify the result text
                        if (index % 8 == 0)
                        {
                            int cFinal = 0;

                            for (int m = 0; m < 8; m++)
                            {
                                //Adding a bit to the right
                                cFinal = cFinal * 2;

                                //Replacing that bit with the element's LSB
                                cFinal += cValue % 2;

                                //Removes the rightmost bit since it was just added
                                cValue /= 2;
                            }

                            cValue = cFinal;

                            //If all of the characters have been added to the extractedText
                            //then display the text and break out
                            if (cValue == 0)
                            {
                                txtExtractedText.Text = extractedText;
                                breakOut = true;
                                break;
                            }
                            else
                            {
                                //Append character to extractedText
                                extractedText += ((char)cValue).ToString();                                                              
                            }
                        }

                    }
                    if (breakOut)
                    {
                        break;
                    }
                }
                if (breakOut)
                {
                    break;
                }
            }
            return extractedText;
        }      
    }
}
