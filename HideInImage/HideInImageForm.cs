using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HideInImage.Properties;

namespace HideInImage
{
    public partial class HideInImageForm : Form
    {
        enum OperationMode
        {
            Encoding,
            Decoding
        }

        readonly ProcessingForm _processingForm = new ProcessingForm();
        private Image _originalImage;
        private Image _encodedImage;
        private OperationMode prevMode;
        public HideInImageForm()
        {
            InitializeComponent();
            prevMode = OperationMode.Encoding;
            SetMode(prevMode);
            pictureBoxImage.Image = Resources.Cube;
        }

        #region From Events

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            string fileName = AskForFile();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }
            textBoxFileName.Text = fileName;

            if (UpdateImage(fileName) == false)
            {
                return;
            }

            buttonHide.Enabled = true;
            toolStripStatusLabel.Text = @"Image: " + Path.GetFileName(fileName);
            richTextBoxInputText.Text = string.IsNullOrEmpty(richTextBoxInputText.Text) ? @"Enter text to hide...הכנס טקסט להחבאה" : richTextBoxInputText.Text;
        }

        private bool UpdateImage(string fileName)
        {
            try
            {
                _originalImage = Image.FromFile(fileName);
                pictureBoxImage.Image =
                    Image.FromFile(fileName).Resize(new Size(pictureBoxImage.Width, pictureBoxImage.Height));
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    $@"Failed to load image from file:{Environment.NewLine}Exception: ""{exception.Message}"""
                    , @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private string AskForFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = @"Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            var result = openFileDialog.ShowDialog();
            return result == DialogResult.OK ? openFileDialog.FileName : null;
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBoxInputText.Text))
            {
                return;
            }
            LongProcessing(() =>
            {
                pictureBoxImage.BeginInvoke((Action) (() =>
                {
                    _encodedImage = Aux.EncodeTextToImage(richTextBoxInputText.Text,
                        (Bitmap) _originalImage.Clone());
                    pictureBoxImage.Image =
                        ((Image) _encodedImage.Clone()).Resize(new Size(pictureBoxImage.Width, pictureBoxImage.Height));
                    toolStripStatusLabel.Text = @"Displaying encoded version of " + Path.GetFileName(textBoxFileName.Text);

                }));
            });
            var saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = @"Image Files(*.BMP;)|*.BMP",
                FileName = Path.GetFileNameWithoutExtension(textBoxFileName.Text) + "_encoded.bmp"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _encodedImage.Save(saveFileDialog.FileName, ImageFormat.Bmp);
            }
        }

        private void buttonReveal_Click(object sender, EventArgs e)
        {
            string fileName = AskForFile();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            if (UpdateImage(fileName) == false)
            {
                return;
            }

            LongProcessing(() =>
            {
                richTextBoxInputText.BeginInvoke((Action) (() =>
                {
                    richTextBoxInputText.Text = Aux.DecodeTextFromImage((Bitmap) _originalImage.Clone());
                    //$@"Decoded Text{Environment.NewLine}{new string('=', charsInLine('=', richTextBoxInputText))}{Environment.NewLine}{}";
                    toolStripStatusLabel.Text = @"Displaying decoded image " + Path.GetFileName(fileName) + @", Hidden text displayed above image";
                }));
            });

        }

        private void richTextBoxInputText_TextChanged(object sender, EventArgs e)
        {
            var richTextBox = sender as RichTextBox;
            if (richTextBox != null)
            {
                buttonHide.Enabled = !string.IsNullOrEmpty(richTextBox.Text) && radioButtonEncoding.Checked && !string.IsNullOrWhiteSpace(textBoxFileName.Text);
            }
        }


        private void radioButtonEncoding_Click(object sender, EventArgs e)
        {
            SetMode(OperationMode.Encoding);
        }

        private void radioButtonDecoding_Click(object sender, EventArgs e)
        {
            SetMode(OperationMode.Decoding);
        }

        private void radioButtonEncoding_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonDecoding_CheckedChanged(object sender, EventArgs e)
        {
        }
        #endregion

        private void LongProcessing(Action action)
        {
            _processingForm.SetImage(Resources.Circle, new Size(pictureBoxImage.Width, pictureBoxImage.Height), pictureBoxImage.PointToScreen(new Point(0, 0)));

            Task.Factory.StartNew(() =>
            {
                var rnd = new Random(DateTime.Now.Millisecond);
                Thread.Sleep(rnd.Next(500, 3000));
                _processingForm.BeginInvoke((Action) (() =>
                {
                    _processingForm.Hide();
                }));
                action();
            });
            _processingForm.ShowDialog();
        }

        private void SetMode(OperationMode mode)
        {
            bool encoding = mode == OperationMode.Encoding;
            buttonHide.Enabled = encoding && !string.IsNullOrEmpty(textBoxFileName.Text);
            buttonReveal.Enabled = !encoding;
            textBoxFileName.Enabled = encoding;
            textBoxFileName.Text = encoding ? textBoxFileName.Text : string.Empty;
            richTextBoxInputText.ReadOnly = !encoding;
            buttonLoadImage.Enabled = encoding;
            radioButtonDecoding.Checked = !encoding;
            radioButtonEncoding.Checked = encoding;

            if (prevMode != mode)
            {
                if (_originalImage != null)
                {
                    pictureBoxImage.Image = Resources.Cube;
                }
                _originalImage = null;
                _encodedImage = null;
                richTextBoxInputText.Text = string.Empty;
                textBoxFileName.Text = string.Empty;
                toolStripStatusLabel.Text = @"Steganography - Hidden Text in Image (Ziv Shahaf © 2017)";
            }


            prevMode = mode;

        }
    }

    public static class Aux
    {
        /// <summary>
        /// Code taken from https://stackoverflow.com/a/1922086/2523211 
        /// </summary>
        public static Image ResizeImage(int newWidth, int newHeight, Image imgPhoto)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            //Consider vertical pics
            if (sourceWidth < sourceHeight)
            {
                int buff = newWidth;

                newWidth = newHeight;
                newHeight = buff;
            }

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent = 0, nPercentW = 0, nPercentH = 0;

            nPercentW = ((float) newWidth/(float) sourceWidth);
            nPercentH = ((float) newHeight/(float) sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth -
                                                (sourceWidth*nPercent))/2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight -
                                                (sourceHeight*nPercent))/2);
            }

            int destWidth = (int) (sourceWidth*nPercent);
            int destHeight = (int) (sourceHeight*nPercent);


            Bitmap bmPhoto = new Bitmap(newWidth, newHeight,
                PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            //imgPhoto.Dispose();
            return bmPhoto;
        }

        public static Color ColorFromByteArray(byte[] bytes)
        {
            return Color.FromArgb(bytes[0], bytes[1], bytes[2]);
        }

        private static byte[] ByteArrayFromColor(Color pixel)
        {
            return new[] { pixel.R, pixel.G, pixel.B };
        }

        private static byte[] PixelToMaskedByteArray(Color pixel)
        {
            return ByteArrayFromColor(pixel).Select(b => (byte) (b & 0xFE)).ToArray();
        }

        private static string CharToBinaryString(char c, int len)
        {
            var bitsString = Convert.ToString(c, 2); //Converting the char with base-2 to a binary string
            return bitsString.PadLeft(len).Replace(' ', '0'); //Padding with up to len '0' bits on the left (with len=8: '110101' --> '00110101')
        }

        /// <summary>
        /// Manipulate the image by changing each pixel's least significant bit (LSB) to hold a single bit from the given text string
        /// 
        /// Currently only works with ASCII charachters
        /// The algorithm overview:
        ///     Given an image and a text string (char array), we will go over the image and bit by bit 
        ///      we will copy bits from the text string and replace the LSB a each bytes of the pixel.
        ///      This means that for each pixel in the image we hide 3 bits from the input string (Assuming RGB format)
        /// The algorithm in practice:
        ///     Do:
        ///         Point to first bit of the string.
        ///         Go over each pixel of the image (Each pixel consists of 3 bytes: R,G,B)
        ///             If string pointer points to end of the string:
        ///                 Continue to next pixel
        ///             Else:
        ///                 Take the next 3 bits from the string (from where the pointer is pointing) and place them in the LSB of 
        ///                  the R, G and B bytes of the pixel (in that order)
        ///                 Advance the string pointer to next 3 bits
        ///     Until all text bits were placed in pixels and a trailing '\0' char was place in the trailing pixels or all pixels have be traversed
        /// </summary>
        /// <param name="text">Given text to hide in image</param>
        /// <param name="imageBitmap">The image in which text should be hidden</param>
        /// <returns>Image containing the hidden text</returns>
        public static Bitmap EncodeTextToImage(string text, Bitmap imageBitmap)
        {
            int numBitsPerChar = 8;
            numBitsPerChar = 16;
            
            int charIndexInText = 0; //A pointer to the character (from the text string) that we are currently hiding 
            string currentCharFromTextBinaryString = CharToBinaryString(text[0], numBitsPerChar); //a binary-string that represents the bits of the charachter we are currently hiding

            //Go over each pixel in the image
            for (int i = 0; i < imageBitmap.Height; i++)
            {
                for (int j = 0; j < imageBitmap.Width; j++)
                {
                    //Clear the LSB for all bytes of the pixel
                    byte[] maskedPixel = PixelToMaskedByteArray(imageBitmap.GetPixel(j, i));

                    //For each pixel write 3 bits from the text string and write to LSB of the pixel's bytes
                    for (int k = 0; k < maskedPixel.Length; k++)
                    {
                        //If we have read an entire character from the text (taken 8 bits, assuming ASCII), 
                        //then we can move to the next character
                        if (currentCharFromTextBinaryString == string.Empty)
                        {
                            //Since at this point we've read an entire character- if we have read all text 
                            // then we have also written a '\0' to the image (to indicate complete end of hidden text)
                            if (charIndexInText == text.Length)
                            {
                                if (k > 0)
                                {
                                    //In case we have changed only some of the bits while writing '\0', we need to make sure they will be written
                                    imageBitmap.SetPixel(j, i, ColorFromByteArray(maskedPixel));
                                }
                                return imageBitmap; //all text was wrriten with the addition of '\0'
                            }
                            //We've finished hiding a single char from the string, advance to next one
                            charIndexInText++;
                            //If the last chararcter was hidden, start writing zeros
                            currentCharFromTextBinaryString = charIndexInText < text.Length
                                ? CharToBinaryString(text[charIndexInText], numBitsPerChar) //Read next charachter
                                : CharToBinaryString('\0', numBitsPerChar);                 //Write null termination (0)
                        }

                        //Set LSB to hold bit number  (maskedPixel's LSB is zero so we simply add a bit to it (either 1 or 0)
                        maskedPixel[k] += Convert.ToByte(Convert.ToInt32(currentCharFromTextBinaryString[0].ToString(), 2));
                        //remove the first bit from the binary-string (We're actually writing the bits in reverse order)
                        currentCharFromTextBinaryString = currentCharFromTextBinaryString.Substring(1);
                    }
                    //Write the edited pixel back to the image
                    imageBitmap.SetPixel(j, i, ColorFromByteArray(maskedPixel));
                }
            }
            return imageBitmap;
        }

        /// <summary>
        /// Extracts hidden text from an image with encoded text in the pixels
        /// 
        /// The Algorithm for extracting the text is the reverse of the Encoding process.
        /// For each pixel in the image, go over each byte of the pixel, take the LSB from the pixel's bytes and concatante them while keeping order: R, G, B.
        /// Keep concatanating bits until the number of bits per text character has reached, and conver these N bits to a single char. Store this char in an array of chars (E.g string).
        /// </summary>
        /// <param name="imageBitmap">An image containing encoded text</param>
        /// <returns>The hidden text from the image</returns>
        public static string DecodeTextFromImage(Bitmap imageBitmap)
        {
            int numBitsPerChar = 8;
            numBitsPerChar = 16;

            string decodedText = string.Empty;
            string decodedBinaryString = string.Empty;
            
            //Go over each pixel in the image
            for (int i = 0; i < imageBitmap.Height; i++)
            {
                for (int j = 0; j < imageBitmap.Width; j++)
                {
                    byte[] pixel = ByteArrayFromColor(imageBitmap.GetPixel(j, i));

                    //For each pixel we need to read 3 bits from the text string and write to LSB of the pixels
                    foreach (byte b in pixel)
                    {
                        if (decodedBinaryString.Length == numBitsPerChar) //Found a complete char
                        {
                            //Convert the collected bits to a char
                            char decodedChar = Convert.ToChar(Convert.ToInt32(decodedBinaryString, 2));
                            if (decodedChar == '\0')
                            {
                                //Found the trailing zeros that indicate end of text
                                return decodedText; //no need to push this character (string does it itself)
                            }
                            //Append the newly found char to the resulting string
                            decodedText += decodedChar;
                            decodedBinaryString = string.Empty; //reset binary string
                        }
                        //Collect the LSB of the byte of the pixel
                        decodedBinaryString += b & 0x1;
                    }
                }
            }
            //We might have read all the image without finding a trailing null termination char, so return whatever we found
            return decodedText;
        }
    }

    public static class Extensions
    {
        public static Image Resize(this Image image, Size newSize)
        {
            return Aux.ResizeImage(newSize.Width, newSize.Height, image);
        }

        public static int AsBigEndianInt(this string binaryString) //e.g:binaryString = "00010111"
        {                                                           //                    ^      ^           
            char[] c = binaryString.ToCharArray();                  //                   LSB    MSB  ==> 0xE8 == 232                                   
            Array.Reverse(c);
            return Convert.ToInt32(Convert.ToInt32(new string(c), 2));
        }
    }
}
