/*****************************************************************************
 * Project:		    ChatClient
 * 
 * Student Names:	Jordan Thompson and Kyle Thomson
 * 
 * File Name:		MessageBuilder.cs
 *
 * Date:			March 19, 2016
 *
 * Description:	    The purpose of this file is to model a message builder class
 *                  that contructs a message based on a string or byte array.
 *****************************************************************************
 * This code was influenced by the Data Class in
 * http://www.codeproject.com/Articles/16916/A-Chat-Application-Using-Asynchronous-TCP-Sockets
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer
{
    class MessageBuilder
    {
        #region Variables
        public string clientName { get; set; }
        public string clientMessage { get; set; }

        #endregion

        #region Constructors
        /*****************************************************************************
         *  Function:       MessageBuilder
         *  
         *  Description:    Default Contructor. Initializes the MessageBuilder
         *                  attributes to null.
        *****************************************************************************/
        public MessageBuilder()
        {
            clientName = null;
            clientMessage = null;
        }

        /*****************************************************************************
         *  Function:       MessageBuilder
         *  
         *  Description:    Initializes the MessageBuilder attributes by converting
         *                  the byte array into strings.
        *****************************************************************************/
        public MessageBuilder(byte[] data)
        {
            int clientNameLength = BitConverter.ToInt32(data, 0);
            int clientMessageLength = BitConverter.ToInt32(data, 4);

            if (clientNameLength > 0)
            {
                clientName = Encoding.UTF8.GetString(data, 8, clientNameLength);
            }

            if (clientMessageLength > 0)
            {
                clientMessage = Encoding.UTF8.GetString(data, 8 + clientNameLength, clientMessageLength);
            }
        }
        #endregion

        #region Functions
        /*****************************************************************************
         *  Function:       ConvertToByte
         *  
         *  Description:    Converts a string message to byte array.
        *****************************************************************************/
        public byte[] ConvertToByte()
        {
            List<byte> byteMessage = new List<byte>();

            byteMessage.AddRange(BitConverter.GetBytes(clientName.Length));
            byteMessage.AddRange(BitConverter.GetBytes(clientMessage.Length));

            byteMessage.AddRange(Encoding.UTF8.GetBytes(clientName));
            byteMessage.AddRange(Encoding.UTF8.GetBytes(clientMessage));

            return byteMessage.ToArray();
        }
        #endregion
    }
}
