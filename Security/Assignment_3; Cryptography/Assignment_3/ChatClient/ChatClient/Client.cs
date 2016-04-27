/*****************************************************************************
 * Project:		    ChatClient
 * 
 * Student Names:	Jordan Thompson and Kyle Thomson
 * 
 * File Name:		Client.cs
 *
 * Date:			March 19, 2016
 *
 * Description:	    The purpose of the file is to model a server in a chat program 
 *                  that recieves messages from all clients and sends all messages
 *                  recieved to each client.
 *****************************************************************************/

using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Client : Form
    {
        #region Variables
        public Socket clientSocket;
        private byte[] byteData = new byte[1024];
        #endregion

        public Client()
        {
            InitializeComponent();
        }

        /*****************************************************************************
         *  Function:       btnConnect_Click
         *  
         *  Description:    Connects the client to the server with a socket connection
         *                  and starts listening for messages.
        *****************************************************************************/
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try {
                //Enable the Send Button
                btnSendMessage.Enabled = true;

                //Disable the Connect Button
                btnConnect.Enabled = false;

                //Create the Client Socket
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //Get the IP Address entered by the User and create an EndPoint
                IPAddress ipAddress = IPAddress.Parse(txtIPAddress.Text);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8888);

                //Connect to the server
                clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(ConnectCallback), null);

                //Start listening
                byteData = new byte[1024];
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(ReceieveCallback), null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                btnConnect.Enabled = true;
                btnSendMessage.Enabled = false;
            }
        }

        /*****************************************************************************
         *  Function:       ConnectCallback
         *  
         *  Description:    Completes the connection of the client to the server.
         *  
         *  Parameter:      IAsyncResult ar:    Represents the status of the asychronous
         *                                      client socket.
         *****************************************************************************/
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);
            }
            catch (Exception)
            {
                Console.WriteLine("Exception with ConnectCallback");
            }
        }

        /*****************************************************************************
         *  Function:       btnSendMessage_Click
         *  
         *  Description:    Sends the data from the client to the server. The data will
         *                  be encrypted if the option is selected in the client.
         *****************************************************************************/
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBuilder msgSend = new MessageBuilder();
                msgSend.clientName = txtUserName.Text;

                //Get the message being sent                

                if (cBEncrypt.Checked)
                {
                    //Encrypt if needed
                    BlowFish b = new BlowFish(txtEncryptKey.Text);
                    string cipherText = b.Encrypt_CBC(txtMessage.Text);

                    //Set Send Message
                    msgSend.clientMessage = cipherText;
                }
                else
                {
                    //Set Send Message
                    msgSend.clientMessage = txtMessage.Text;
                }

                byte[] byteData = msgSend.ConvertToByte();

                //Send message to the server
                clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);

                txtMessage.Clear();

                if (msgSend.clientMessage == "!Disconnect")
                {
                    clientSocket.Close();
                    Application.Exit();
                }               
            }
            catch (Exception)
            {
                Console.WriteLine("Exception when attempting to send a message to the server");
            }
        }

        /*****************************************************************************
         *  Function:       SendCallback
         *  
         *  Description:    Completes the sending of the data to the server.
         *  
         *  Parameter:      IAsyncResult ar:    Represents the status of the asychronous
         *                                      client socket.
         *****************************************************************************/
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (Exception)
            {
                Console.WriteLine("Exception with SendCallback");
            }
        }

        /*****************************************************************************
         *  Function:       ReceieveCallback
         *  
         *  Description:    Completes the receiving of the data from the server.
         *                  Decrypts the data if necessary and updates the chat box.
         *  
         *  Parameter:      IAsyncResult ar:    Represents the status of the asychronous
         *                                      client socket.
         *****************************************************************************/
        private void ReceieveCallback(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);
                MessageBuilder msgReceive = new MessageBuilder(byteData);

                if (msgReceive.clientMessage != null)
                {
                    try
                    {
                        if (cBEncrypt.Checked)
                        {
                            BlowFish b = new BlowFish(txtEncryptKey.Text);

                            //Separating recieved message and decrypting only the message and not the client name
                            Invoke((MethodInvoker)delegate()
                            {
                                txtChat.AppendText(msgReceive.clientMessage + Environment.NewLine);
                            });
                            
                            string[] splitMessage = msgReceive.clientMessage.Split(':');
                            string plain = b.Decrypt_CBC(splitMessage[1].Trim());
                            
                            Invoke((MethodInvoker)delegate()
                            {
                                txtChat.AppendText(splitMessage[0] + ": " + plain + Environment.NewLine);
                            });                            
                        }
                        else
                        {
                            Invoke((MethodInvoker)delegate()
                            {
                                txtChat.AppendText(msgReceive.clientMessage + Environment.NewLine);
                            });
                        }
                    }
                    catch (Exception)
                    {
                        Invoke((MethodInvoker)delegate()
                        {
                            txtChat.AppendText("Could not decrypt message." + Environment.NewLine);
                        });
                    }
                }

                byteData = new byte[1024];
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(ReceieveCallback), null);
            }
            catch (Exception)
            {
                Console.WriteLine("Exception with ReceieveCallback");
            }
        }

        /*****************************************************************************
         *  Function:       Client_FormClosing
         *  
         *  Description:    Attempts to close the client socket.
         *****************************************************************************/
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MessageBuilder msgSend = new MessageBuilder();
                msgSend.clientName = txtUserName.Text;
                msgSend.clientMessage = "!Disconnect";
                byte[] byteData = msgSend.ConvertToByte();

                //Send message to the server
                clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);

                clientSocket.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Exception when attempting to close the socket");
            }
        }

        /*****************************************************************************
         *  Function:       txtMessage_KeyDown
         *  
         *  Description:    Calls the btnSendMessage_Click method when the User presses
         *                  the Enter key.
         *****************************************************************************/
        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSendMessage_Click(sender, null);
            }
        }
    }
}
