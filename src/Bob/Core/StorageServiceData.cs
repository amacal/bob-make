﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bob.Core
{
    public class StorageServiceData : IStorageData
    {
        private readonly string path;

        public StorageServiceData(string directory)
        {
            this.path = directory.Backslash().TrimEnd('\\');
        }

        public string Path
        {
            get { return this.path; }
        }

        public IEnumerable<string> Files(Glob glob)
        {
            return Directory.EnumerateFiles(this.path, "*", SearchOption.AllDirectories).Select(this.Split).Where(x => glob.IsMatch(x.Item1)).Select(this.Strip);
        }

        private Tuple<string, string> Split(string name)
        {
            return Tuple.Create(name.Substring(this.path.Length + 1), name);
        }

        private string Strip(Tuple<string, string> tuple)
        {
            return tuple.Item2;
        }
    }
}