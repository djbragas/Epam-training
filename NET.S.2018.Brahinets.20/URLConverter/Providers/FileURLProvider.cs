﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLParser
{
    public class FileURLProvider : IURLAddresesProvider
    {
        private readonly string path;
        private readonly IURLParser parser;
        private readonly ILogger logger;
        private readonly IURLValidator urlValidator; 

        public FileURLProvider(string path, IURLParser parser, ILogger logger, IURLValidator urlValidator)
        {
            this.path = path ?? throw new ArgumentNullException($"{nameof(path)} is null");
            this.parser = parser ?? throw new ArgumentNullException($"{nameof(parser)} is null");
            this.logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} is null");
            this.urlValidator = urlValidator ?? throw new ArgumentNullException($"{nameof(urlValidator)} is null");
            CheckExistance();
        }

        public IEnumerable<URL> GetURLs()
        {
            CheckExistance();

            IEnumerable<string> content = File.ReadLines(path);
            var parsedUrls = new List<URL>();

            foreach(var tryUrl in content)
            {
                if (urlValidator.IsValid(tryUrl))
                {
                    URL parsedUrl = parser.Parse(tryUrl);
                    parsedUrls.Add(parsedUrl);
                }
                else
                {
                    logger.Log($"can't parse { tryUrl }"); 
                }
            }

            return parsedUrls;
        }

        private void CheckExistance()
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"a file by {path} can't be found");
            }
        }
    }
}
