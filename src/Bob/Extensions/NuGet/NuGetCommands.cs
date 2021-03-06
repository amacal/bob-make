﻿namespace Bob.Extensions.NuGet
{
    public static class NuGetCommands
    {
        public static NuGetConfigureCommand Configure()
        {
            return new NuGetConfigureCommand();
        }

        public static NuGetInstallCommand Install()
        {
            return new NuGetInstallCommand();
        }

        public static NuGetPackCommand Pack()
        {
            return new NuGetPackCommand();
        }

        public static NuGetRestoreCommand Restore()
        {
            return new NuGetRestoreCommand();
        }
    }
}