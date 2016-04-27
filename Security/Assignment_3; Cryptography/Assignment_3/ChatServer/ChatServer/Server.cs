/*****************************************************************************
 * Project:		    ChatServer
 * 
 * Student Names:	Jordan Thompson and Kyle Thomson
 * 
 * File Name:		Server.cs
 *
 * Date:			March 17, 2016
 *
 * Description:	    The purpose of the file is to model a server in a chat program 
 *                  that recieves messages from all clients and broadcasts all
 *                  messages recieved to each client.
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Server : Form
    {
        #region Variables
        //List of Clients for Broadcasting
        List<Client> clientList = new List<Client>();

        //Server Socket
        Socket serverSocket;

        //Message Data
        byte[] byteData = new byte[1024];
        #endregion

        #region Client Struct
        //Client Struct to keep track
        //of each client's socket and name
        struct Client
        {
            public Socket socket;
            public string name;
        }
        #endregion

        public Server()
        {
            InitializeComponent();
        }

        /*****************************************************************************
         *  Function:       Server_Load
         *  
         *  Description:    When the form loads this creates the server socket,
         *                  binds the socket to the local Endpoint and begins listening
         *                  for clients.
         *****************************************************************************/
        private void Server_Load(object sender, EventArgs e)
        {
            try
            {
                //Create Server socket
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 8888);

                //Bind Socket to local EndPoint
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(4);

                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception)
            {
                Console.WriteLine("Exeception when attempting to create and bind server socket");
            }
        }

        /*****************************************************************************
         *  Function:       AcceptCallback
         *  
         *  Description:    Sets up a client socket for the client, listens for more
         *                  clients, and listens for any messages sent from the client
         *                  socket.
         *  
         *  Parameter:      IAsyncResult ar:    Represents the status of the asychronous
         *                                      server socket.
         *****************************************************************************/
        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                //Sets up the client socket
                Socket clientSocket = serverSocket.EndAccept(ar);

                //Listen for more clients
                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

                //Listen for client messages
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
            }
            catch (Exception)
            {
                Console.WriteLine("Exception when setting up clientSocket and listening for more");
            }
        }

        /*****************************************************************************
         *  Function:       ReceiveCallback
         *  
         *  Description:    Receives a client message, adds that client to the clientList,
         *                  if they are not already on the list, and then constructs and
         *                  broadcasts the message to all the clients on the list.
         *  
         *  Parameter:      IAsyncResult ar:    Represents the status of the asychronous
         *                                      server socket.
         *****************************************************************************/
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                //Gets the client socket from the receive
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);

                //Constructs the message received and message that will be sent
                MessageBuilder msgReceive = new MessageBuilder(byteData);
                MessageBuilder msgSend = new MessageBuilder();

                //Checks if the current client is on the client list
                //If not, then it adds the client with it's name and socket
                int index = clientList.FindIndex(item => item.socket == clientSocket);
                if (!(index >= 0))
                {
                    Client client = new Client();
                    client.socket = clientSocket;
                    client.name = msgReceive.clientName;
                    clientList.Add(client);
                }

                if (msgReceive.clientMessage == "!Disconnect")
                {
                    clientSocket.Close();
                    clientList.Remove(clientList[index]);
                }
                else
                {
                    //Construct message that will be broadcasted to all clients
                    msgSend.clientName = msgReceive.clientName;
                    msgSend.clientMessage = msgReceive.clientName + ": " + msgReceive.clientMessage;

                    //Converts the message to a byte array
                    byte[] byteMessage = msgSend.ConvertToByte();

                    //Broadcasts the message to each client in the client list
                    foreach (Client clientInList in clientList)
                    {
                        clientInList.socket.BeginSend(byteMessage, 0, byteMessage.Length, SocketFlags.None, new AsyncCallback(SendCallback), clientInList.socket);
                    }

                    //Updates the Server chat log
                    Invoke((MethodInvoker)delegate()
                    {
                        txtServerChat.AppendText(msgSend.clientMessage + Environment.NewLine);
                    });

                    //Waits for more messages
                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
                }                                
            }
            catch (Exception)
            {
                Console.WriteLine("Exception when receiveing and / or broadcasting messages from clients");
            }
        }

        /*****************************************************************************
         *  Function:       SendCallback
         *  
         *  Description:    Completes the sending of the data to the client(s).
         *  
         *  Parameter:      IAsyncResult ar:    Represents the status of the asychronous
         *                                      client socket.
         *****************************************************************************/
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndSend(ar);
            }
            catch (Exception)
            {
                Console.WriteLine("Exception when sending message to the client(s)");
            }
        }
    }
}
