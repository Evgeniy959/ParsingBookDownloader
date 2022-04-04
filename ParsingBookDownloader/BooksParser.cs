﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingBookDownloader
{
    class BooksParser
    {
        List<Book> books;
        string baseUrl = "https://tululu.org/txt.php?id=";

        public List<Book> GetBooks(string site)
        {
            books = new List<Book>();
            string name = "";
            string url = "";
            int index = site.IndexOf("href=\"/b");
            while (index >= 0)
            {
                //site = site.Remove(0, index + 6);
                site = site.Remove(0, index + 8);
                //index = site.IndexOf("\">");
                index = site.IndexOf("/");
                if (index >= 0)
                {
                    url = baseUrl + site.Remove(index);
                    //url = baseUrl + "/txt.php?id" + "=" + site.Remove(index);
                    //url = null;
                    //site = site.Remove(0, index + 2);
                    site = site.Remove(0, index + 3);
                    name = site.Remove(site.IndexOf("</a>"));
                    books.Add(new Book() { Name = name, Url = url });
                    index = site.IndexOf("href=\"/b");
                }
            }
            return books;
        }
    }
}