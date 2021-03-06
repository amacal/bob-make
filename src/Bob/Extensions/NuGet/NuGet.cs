﻿using Bob.Extensions.NuGet;
using System;

namespace Bob
{
    public static class NuGet
    {
        public static NuGetCacheFactory Cache
        {
            get { return new NuGetCacheFactory(); }
        }

        public static NuGetPathRepository Path
        {
            get { return new NuGetPathRepository(); }
        }

        public static NuGetPackageRepository Repository
        {
            get { return new NuGetPackageRepository(); }
        }

        public static NuGetSpecificationRepository Specification
        {
            get { return new NuGetSpecificationRepository(); }
        }

        public static ITask Configure(Action<NuGetConfigureParameters> parameters)
        {
            return NuGetCommands.Configure().Execute(parameters);
        }

        public static ITask Install(Action<NuGetInstallParameters> parameters)
        {
            return NuGetCommands.Install().Execute(parameters);
        }

        public static ITask Pack(Action<NuGetPackParameters> parameters)
        {
            return NuGetCommands.Pack().Execute(parameters);
        }

        public static ITask Restore()
        {
            return NuGetCommands.Restore().Execute();
        }

        public static ITask Restore(Action<NuGetRestoreParameters> parameters)
        {
            return NuGetCommands.Restore().Execute(parameters);
        }
    }
}