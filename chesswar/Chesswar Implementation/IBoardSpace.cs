//-----------------------------------------------------------------------
// <copyright file="Special.cs" company="BENTwerx">
//     GPLv3 Copyright 2008 Robert Ramsay
// </copyright>
// <author>Robert Ramsay</author>
//-----------------------------------------------------------------------

namespace chesswar
{
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// An interface to hold empty board spaces and pieces.
    /// </summary>
    public interface IBoardSpace : IXmlSerializable
    {
        /// <summary>
        /// Gets a value indicating whether this IBoardSpace is empty.
        /// </summary>
        bool IsEmpty
        {
            get;
        }
		
    }
}
