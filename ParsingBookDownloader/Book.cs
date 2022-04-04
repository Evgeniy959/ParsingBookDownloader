﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingBookDownloader
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string BaseUrl { get; set; }
        public string Txt { get; set; }
        public string Zip { get; set; }
        public string Jar { get; set; }
    }
}
