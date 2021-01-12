using System.Diagnostics;
using NetMQ;
using NetMQ.Sockets;
using Xunit;

namespace pythonnet
{
    public class ExecutingPython
    {
        [Fact]
        public void Execute_a_python_script_and_receive_a_message()
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
            Assert.True(message.Contains("Hello"));
        }
    }
}
