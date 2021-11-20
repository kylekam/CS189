from socket import *

IP = "127.0.0.1"
PORT = 10000

server = socket(AF_INET, SOCK_DGRAM)
server.bind((IP, PORT))
print("Listening on {} port {}".format(IP, PORT))

while True:
    data, addr = server.recvfrom(1024)

    message = data.decode()

    print(message)
