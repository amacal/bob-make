﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Bob.Core;

namespace Bob.Tests.Integration.Stubs
{
    public class ShellStub : IShell
    {
        private readonly List<ProcessStartInfo> executed;

        public ShellStub()
        {
            this.executed = new List<ProcessStartInfo>();
        }

        public int Start(ProcessStartInfo info)
        {
            this.executed.Add(info);
            return 0;
        }

        public bool Any(Func<ProcessStartInfo, bool> predicate)
        {
            return this.executed.Any(predicate);
        }
    }
}