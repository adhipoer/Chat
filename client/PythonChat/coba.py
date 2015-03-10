import socket, sys, select

def duringyou():
    sys.stdout.write('you :')
def client():
    if(len(sys.argv)<3):
        print 'Usage: chat hostname port'
        sys.exit()
    host = sys.argv[1]
    port = int(sys.argv[2])

    s=socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.settimeout(2)

    try:
        s.connect((host,port)
    execpt:
        print'sorry, unable to connect'
        sys.exit()
    print 'you can start sending message'
        duringyou()

    while 1:
        socket_list = [sys.stdin, s]
        read,write,in_error = select.select(socket_list , [], [])
        for sock in read:
            if sock == s:
                data = sock.recv(4096)
                    if not data :
                        print '\nDisconnected'
                        sys.exit()
                    else :
                        sys.stdout.write(data)
                        duringyou()
            else:
                message = sys.stdin.readline()
                s.send(message)
                duringyou()
                  
if __name__ == "__main__":
    sys.exit(client())
