using System;
using System.Diagnostics;
using NetMQ;
using NetMQ.Sockets;

namespace PythonRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            using var server = new ResponseSocket();
            server.Bind("tcp://*:5555");
            var processConfiguration = new ProcessStartInfo
            {
                FileName = "python3",
                Arguments = "Python/HelloZMQ.py",
                UseShellExecute = true
            };
            using var process = Process.Start(processConfiguration);
            var message = server.ReceiveFrameString();
            Console.WriteLine($"Message from python: {message}");
        }
    }
}