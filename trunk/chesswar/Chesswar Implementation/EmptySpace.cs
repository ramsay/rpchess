using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace chesswar
{
    public sealed class EmptySpace : IBoardSpace
    {
        static readonly EmptySpace instance = new EmptySpace();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static EmptySpace()
        {
        }

        EmptySpace()
        {
        }

        public static EmptySpace Instance
        {
            get
            {
                return instance;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Initializes all of the Piece memembers
        /// according to data placed in a well formed
        /// Xml element.
        /// </summary>
        /// <param name="xml">An xml node containing all the member data.</param>
        /// <returns>A Piece type-casted as an IRPChessObject.</returns>
        public void ReadXml(XmlReader xr)
        {
            //TODO
        }

        /// <summary>
        /// Write all the piece data to a well formatted 
        /// XML document for human readable storage.
        /// </summary>
        /// <returns>An XmlDocument</returns>
        public void WriteXml(XmlWriter xw)
        {
            xw.WriteElementString("EmptySpace", "");
            xw.WriteEndElement();
        }

        public XmlSchema GetSchema()
        {
            return (null);
        }
    }
}
