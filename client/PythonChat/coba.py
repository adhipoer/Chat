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
    
