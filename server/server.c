
#include <sys/socket.h>
#include <arpa/inet.h>
#include <sys/types.h>
#include <unistd.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <pthread.h>

#include "usermanage.h"

#define FILENAME "userpass.txt"
#define MAXRECV 1000
#define MSG	1000
#define LINES	100
#define ALLUSER	7
#define LOGIN	5
#define REG	3
#define MESSAGE	7
#define LOGOUT	6

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
char usernames[LINES][MAXCHAR];
char passwords[LINES][MAXCHAR];

List *all;
List *threads;

int servFd;

void *threadsN(void *);
int checkUser(char *);
int checkPass(char *);
int reg(char *,char *);

int main(int argc, char **argv)
{
	if(argc!=2)
	{
		printf("port : %s", argv[0]);
		exit(1);
	}

	int cliFd;
	int portNum;
	int cliLen;

	struct sockaddr_in serv_addr;
	struct sockaddr_in cli_addr;
	pthread_t *thread;
	Request *req;
	all = (List *)malloc(sizeof(List));
	
	threads =(List *) malloc(sizeof(List ));
	
	all->head = NULL;
	threads->head = NULL;

	cliLen = sizeof(cli_addr);
	portNum = atoi(argv[1]);
	
	int i,j;
	for(i=0;i<LINES;i++)
	{
		for(j=0;j<MAXCHAR;j++)
		{
			usernames[i][j] = 0;
			passwords[i][j] = 0;		
		}
	}
	servFd = socket(AF_INET, SOCK_STREAM, 0);
	memset(&serv_addr, 0, sizeof(serv_addr));

	serv_addr.sin_family = AF_INET;
	serv_addr.sin_addr.s_addr= inet_addr("10.151.36.55");
	serv_addr.sin_port = htons(portNum);

	if(bind(servFd,(struct sockaddr *)&serv_addr, sizeof(serv_addr))<0)
		printf("failed\n");
	if(listen(servFd,MAXUSER)< 0)
		printf("failed\n");
	
	printf("server started\n");

	while(1)
	{
		if((cliFd = accept(servFd, (struct sockaddr *)&cli_addr, (socklen_t *)&cliLen)) < 0)
			printf("failed\n");
		req = (Request *)malloc(sizeof(Request));
		thread = (pthread_t *)malloc(sizeof(pthread_t));
		
		req->IP = cli_addr.sin_addr.s_addr;
		req->sockNum = cliFd;
		printf("request from %lu sock %d\n", req->IP, req->sockNum);
		pthread_create(thread, NULL, &threadsN, req);
		addNew(threads, (void *)thread, &mutex);
	}
}

void *threadsN(void *arg)
{
	pthread_detach(pthread_self());
	Request *req = (Request *) arg;
       	int mySock = req->sockNum;
	unsigned long myIP = req->IP;
	
	free(req);
	FILE *fp;
    
	int recvBuffSize;
    char signbuff[MAXCHAR];
	char nameBuff[MAXCHAR];
	char passBuff[MAXCHAR];
	char dataRecv[MAXRECV];
    char message[MSG];
	int nameLen, nameIndex,passRight;
    int sendDataLen = 0;
	int newUser = 1;
	UserData *currUser = NULL;
	
	List *names = (List *)malloc(sizeof(List));
	names->head = NULL;
	
	memset(dataRecv, 0, MAXRECV);
	recvBuffSize = recv(mySock, dataRecv,MAXRECV, 0);
    
    while (recvBuffSize != 0){
        printf("%s\n", dataRecv);
        sscanf(dataRecv, "%s %s %s %[^\n]", signbuff, nameBuff, passBuff, message);

        int y,z;
		for(z = 0; z<LINES; z++) {
			for(y = 0; y < MAXCHAR; y++) { 
				usernames[z][y] = 0;
				passwords[z][y] = 0;
			}
		}
		if(!strcmp(signbuff, "register"))
		{
			fp = fopen(FILENAME, "a");
	    		if (fp == NULL)       				
				printf("fopen() failed");
			fprintf(fp, "%s %s\n", nameBuff,passBuff);
			fclose(fp);
            
		}
		if(!strcmp(signbuff, "login"))
		{
			fp = fopen(FILENAME, "r");
			if (fp == NULL)
				printf("fopen() failed");
			
			int a;
			for(a = 0; a < LINES; a++)
			{
				fscanf(fp, "%s %s\n", usernames[a], passwords[a]);
                printf("%s %s\n", usernames[a], passwords[a]);

				if(usernames[a] == nameBuff && passwords[a]==passBuff)
				{
					currUser = findUser(all, (char *)nameBuff, &mutex);
					if(currUser){
						if(currUser->loggedIn){
                            write(mySock, "use already login\n", 23);			
						continue;
						}
					
						newUser=0;
						break;
					}	
				}
`
			}
			if(newUser){
				currUser = (UserData *)malloc(sizeof(UserData));
				memset(currUser, 0, sizeof(UserData));
				currUser->IP = myIP;
				currUser->sockNum = mySock;
				strcpy(currUser->userName, (char *) nameBuff);
				strcpy(currUser->userPass, (char *) passBuff);
				currUser->loggedIn = 1;
				addNew(all, (void *)currUser, &mutex);

			}
			else
			{
				currUser->IP = myIP;
				currUser->sockNum = mySock;
				currUser->loggedIn = 1;
			}
            recvBuffSize = recv(mySock, dataRecv,MAXRECV, 0);
			while(recvBuffSize!=0)
			{
				if(!strcmp(signbuff, "private")
				{
					char user[MAXCHAR];

					char tmpbuff[MSG];
					memset(user, 0, MAXCHAR);
					memset(message, 0, MSG);
					
					int msglen = strlen(message);
					UserData *toUser = findUser(all, passBuff, &mutex);
					if(toUser && toUser !=currUser)
					{
						if(toUser->loggedIn)
                        {
                            write(toUser->socknum, "\n", 1);
                            write(toUser->socknum, message, msglen);
                            write(toUser->sockNum,"\n\n", 2);
                        }
					}																				
				}
                recvBuffSize = recv(mySock, dataRecv,MAXRECV, 0);
			}
        }
        
        recvBuffSize = recv(mySock, dataRecv,MAXRECV, 0);
	}
}
