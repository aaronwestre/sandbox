import zmq

def main():
    context = zmq.Context()
    socket = context.socket(zmq.REQ)
    socket.connect("tcp://localhost:5555")
    socket.send(b"Hello from python zmq!")

if __name__ == "__main__":
    main()
