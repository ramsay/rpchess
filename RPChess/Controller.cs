//-----------------------------------------------------------------------
// <copyright file="Controller.cs" company="BENTwerx">
//     GPLv3 Copyright 2008 Robert Ramsay
// </copyright>
// <author>Robert Ramsay</author>
//-----------------------------------------------------------------------

namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Interface to keep all future controllers interchangeable.
    /// </summary>
    interface Controller
    {
    }

    /// <summary>
    /// Class for handling input from in a text interface.
    /// </summary>
    class TextController : Controller
    {
        private uint _count;
        public int Count
        {
            get
            {
                return (int)_count;
            }
        }
        public TextController()
        {
        }
        public TextController(bool init)
        {
            if (init)
            {
                Initialize();
            }
        }
        public void Initialize()
        {
            _count = 0;
        }
    }
}
