from socket import *

server_addr = ("127.0.0.1", 9000)

client = socket(AF_INET, SOCK_DGRAM)

message = "Hello from Python Client!"

client.sendto(message.encode(), server_addr)
print("sent message: {} to server at {}".format(message, server_addr))
