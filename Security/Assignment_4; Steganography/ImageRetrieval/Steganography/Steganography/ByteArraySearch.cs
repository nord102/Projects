using System;
using System.Collections.Generic;

namespace Steganography
{
    /*
    Class:      ByteArraySearch
    Purpose:    Scans an array of bytes for a pattern of bytes and saves areas between a starting and ending
                sequence to another array.
    Notes:      The original code base was taken from http://stackoverflow.com/questions/283456/byte-array-pattern-search
                developed by user: Jb Evain
                It has since been modifed from its original for the purposes of this assignment. 
    */
    public static class ByteArraySearch
    {
        // build a list of byte[] by scanning the source byte[] for header and footer patterns
        // and returning the list of possible images for further processing.
        public static List<byte[]> Locate(byte[] self)
        {
            // do not scan for the first FF because a delted file replaces the FF byte with something else.
            byte[] _header = new byte[3] { 216, 255, 225 };     // (FF) D8 FF E1  -  (255) 216 255 225
            byte[] _footer = new byte[2] { 255, 217 };          // FF D9
            byte[] _possibleImage;
            List<byte[]> list = new List<byte[]>();
            int headerIndex = 0, footerIndex = 0;            
            bool FindHeader = true;
            bool FoundEnd = false;

            // test if the original byte[] is empty or invalid.
            if (IsEmptyLocate(self))
                return null;

            // begin scanning the array.
            for (int i = 0; i < self.Length; i++)
            {
                // Look for a header pattern first
                if (FindHeader)
                {
                    if (IsMatch(self, i, _header))
                    {
                        headerIndex = i;
                        FindHeader = false;
                    }                    
                }
                // once a header is found, search for the next header
                // once found, scan the source backwards until you find a footer
                // save the byte[] from the header to the footer index to a new array 
                // and store in the list of possible images. 
                else
                {
                    // find the next header
                    if (IsMatch(self, i, _header))
                    {
                        // count backwards to the footer
                        do
                        {
                            i--;
                        } while (!IsMatch(self, i, _footer));

                        footerIndex = i;

                        FindHeader = true;
                        FoundEnd = true;
                    }
                    // if at the end of the file...
                    else if (i == self.Length - 1)
                    {
                        // count backwards to a footer
                        do
                        {
                            i--;
                        } while (!IsMatch(self, i, _footer));

                        footerIndex = i;

                        FindHeader = true;
                        FoundEnd = true;
                    }
                }

                // if an ender is found, dump the bytes into a separate byte[]
                if (FoundEnd)
                {                          
                    _possibleImage = GetArray(self, headerIndex, footerIndex);
                    if (_possibleImage != null && _possibleImage.Length > 0)
                    {
                        list.Add(_possibleImage);
                    }
                    FoundEnd = false;
                }
            } // end for

            return list;
        }

        // attempt to find a match in the source array to the candidate array
        public static bool IsMatch(byte[] array, int position, byte[] candidate)
        {
            if (candidate.Length > (array.Length - position))
                return false;

            for (int i = 0; i < candidate.Length; i++)
                if (array[position + i] != candidate[i])
                    return false;

            return true;
        }

        // determine if the source array is empty or null
        public static bool IsEmptyLocate(byte[] array)
        {
            return array == null || array.Length == 0;
        }

        // dump the array between the header and footer indices into a separate byte[]
        // and return as a possble image.
        public static byte[] GetArray(byte[] source, int start, int end)
        {
            if (start > end) // if the header index is located after the end, return null.
                return null;

            byte[] _image = new byte[end - start + 2];
            int j = 1;

            _image[0] = 255;
            for (int i = start; i <= end; i++)
            {
                _image[j] = source[i];
                j++;
            }

            return _image;
        }
    }
}

