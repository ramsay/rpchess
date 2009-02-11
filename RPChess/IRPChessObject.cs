//-----------------------------------------------------------------------
// <copyright file="IRPChessObject.cs" company="BENTwerx">
//     GPLv3 Copyright 2008 Robert Ramsay
// </copyright>
// <author>Robert Ramsay</author>
//-----------------------------------------------------------------------

namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// The root of all [evil!] RPChess IRPChessObjects all classes must implement
    /// this interface.  All other interfaces implement this interface.  
    /// Guarantees a uniform XML Serialization behavior.
    /// </summary>
    public interface IRPChessObject
    {
        /// <summary>
        /// Returns the IRPChessObject to the state it would be as if the game had not
        /// started.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Makes all IRPChessObjects comply to RPChess proprietary XML Serialization.
        /// </summary>
        /// <param name="xml">XmlDocument containing a Serialized IRPChessObject</param>
        /// <returns>The Constructed IRPChessObject Type-Casted</returns>
        IRPChessObject FromXmlDocument(XmlDocument xml);

        /// <summary>
        /// Makes all IRPChessObjects comply to RPChess proprietary XML Serialization.
        /// </summary>
        /// <returns>
        /// An xml snippet containing all the properties and fields 
        /// of the IRPChessObject
        /// </returns>
        XmlDocument ToXmlDocument();
    }
}
