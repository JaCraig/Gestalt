﻿using BenchmarkDotNet.Running;

namespace Gestalt.SpeedTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new BenchmarkSwitcher(typeof(Program).Assembly).Run(args);
        }
    }
}