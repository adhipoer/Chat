
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
List *names;
int servFd;

void *loginThread(void *);
void *threadsN(void *);
int reg(char *,char *);
void Die(char *message);
void threadCleanup(void *arg);

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
	
	servFd = socket(AF_INET, SOCK_STREAM, 0);
	memset(&serv_addr, 0, sizeof(serv_addr));

	serv_addr.sin_family = AF_INET;
	serv_addr.sin_addr.s_addr= inet_addr("10.151.36.206");
	serv_addr.sin_port = htons(portNum);

	if(bind(servFd,(struct sockaddr *)&serv_addr, sizeof(serv_addr))<0)
		Die("Bind failed\n");
    else
        printf("Bind success\n");
	if(listen(servFd,MAXUSER)< 0)
		Die("Listen failed\n");
	else
        printf("listen success\n");
	
	printf("server started\n");

	while(1)
	{
		if((cliFd = accept(servFd, (struct sockaddr *)&cli_addr, (socklen_t *)&cliLen)) < 0)
			Die("accept failed\n");
		req = (Request *)malloc(sizeof(Request));
		thread = (pthread_t *)malloc(sizeof(pthread_t));
		
		strcpy(req->IP,inet_ntoa(cli_addr.sin_addr));
		req->sockNum = cliFd;
		printf("request from %s sock %d\n", req->IP, req->sockNum);
		pthread_create(thread, NULL, &loginThread, req);
		addNew(threads, (void *)thread, &mutex);
	}
}
void *loginThread(void *arg)
{
	pthread_t *thread;
	Request *req = (Request *) arg;
    int mySock = req->sockNum;
	char myIP[MAXCHAR];
	strcpy(myIP, req->IP);
	
	free(req);
    FILE *fp;
	int recvBuffSize;
    char signbuff[MAXCHAR];
	char nameBuff[MAXCHAR];
	char passBuff[MAXCHAR];
	char message[MAXCHAR];
	char dataRecv[MAXRECV];
	int sendDataLen = 0;
	int newUser = 1;
	UserData *currUser = NULL;
	
	List *names = (List *)malloc(sizeof(List));
	names->head = NULL;
	
	memset(dataRecv, 0, MAXRECV);
	recvBuffSize = recv(mySock, dataRecv,MAXRECV, 0);
    
    while (recvBuffSize != 0)
	{
        sscanf(dataRecv, "%s %s %s", signbuff, nameBuff, passBuff);
        printf("%s\n", signbuff);

        
		if(!strcmp(signbuff, "register"))
		{
			fp = fopen(FILENAME, "a");
	    		if (fp == NULL)       				
				printf("fopen() failed");
			if(!strcmp(nameBuff,"\0") || !strcmp(nameBuff," ") || !strcmp(passBuff,"\0") || !strcmp(passBuff," "))
			{
				write(mySock, "username/password harap diisi dengan benar\n", 43);				
			}
			else{
				fprintf(fp, "%s %s\n", nameBuff,passBuff);
            	fclose(fp);
            	write(mySock, "Selamat, anda sudah terdaftar\n", 30);
			}
		}

		else if(!strcmp(signbuff, "login"))
		{
			int flag1 = 0;
			int flag2 = 0;
			int y,z;
			for(z = 0; z<LINES; z++) {
				for(y = 0; y < MAXCHAR; y++) { 
					usernames[z][y] = 0;
					passwords[z][y] = 0;
				}
			}
			fp = fopen(FILENAME, "r");
			if (fp == NULL)
				printf("fopen() failed");
			
			int a = 0;
			
			while(fscanf(fp, "%s %s\n", usernames[a], passwords[a])!=EOF){
				a++;
			}
			//printf("%d\n", a);
			int i;
			int c =0;
			for(i = a; i >=0; i--)
			{	
				if(!strcmp(usernames[i], nameBuff))
				{
					flag1 = 1;
					if(!strcmp(passwords[i],passBuff))
					{
						flag2 = 1;
						break;
					}
				}
				
			}
            if(flag1 == 1 && flag2 == 1)
			{
				currUser = findUser(all, nameBuff, &mutex);
			  	if(currUser) 
			  	{
					//printf("User exists\n");
					if(currUser->loggedIn) {
				  		write(mySock, "failed", 7);
				  		continue;
					}
					
					newUser=0;
					break;
			  	}
			  	if(newUser){
		            currUser = (UserData *)malloc(sizeof(UserData));
		            memset(currUser, 0, sizeof(UserData));
		            strcpy(currUser->IP,myIP);
		            currUser->sockNum = mySock;
		            strcpy(currUser->userName, (char *) nameBuff);
		            strcpy(currUser->userPass, (char *) passBuff);
		            currUser->loggedIn = 1;
		            addNew(all, (void *)currUser, &mutex);
		            write(currUser->sockNum, "success", 8);
				}
				else 
				{  
					strcpy(currUser->IP,myIP);
					currUser->sockNum = mySock;
					currUser->loggedIn = 1;
				}
				memset(dataRecv, 0, MAXRECV);
				recvBuffSize = recv(currUser->sockNum, dataRecv,MAXRECV, 0);
				while( recvBuffSize!=0 )
				{                      
					sscanf(dataRecv, "%s %s %[^\n]", signbuff, nameBuff, message);         
					if(!strcmp(signbuff, "private"))
					{
						//printf("haloo\n");
						char user[MAXCHAR];
						char tmpbuff[MSG];
						memset(user, 0, MAXCHAR);
						memset(tmpbuff, 0, MSG);
						strcpy(tmpbuff, message);
						int msglen = strlen(message);
						UserData *toUser = findUser(all, nameBuff, &mutex);
						printf("%d", toUser->sockNum);
						if(toUser && toUser !=currUser)
						{
							if(toUser->loggedIn)
							{
								write(toUser->sockNum, "\n", 1);
								write(toUser->sockNum, message, msglen);
								write(toUser->sockNum,"\n\n", 2);
							}
						}																				
				   	}

					else if(!strcmp(signbuff, "alluser"))
					{
						write(currUser->sockNum, "\n",1);
						char listAllUser[MAXCHAR*MAXCHAR+MAXCHAR];
						memset(listAllUser,0 , MAXCHAR*MAXCHAR+MAXCHAR);
						allUser(all, listAllUser,(void *)currUser, &mutex);
						sendDataLen = strlen(listAllUser);
						write(currUser->sockNum, listAllUser, sendDataLen);
						write(currUser->sockNum, "\n", 1);
						//continue;
					}
					else if(!strcmp(signbuff, "logout"))
					{
						write(currUser->sockNum, "Logging off.\n", 13);
						shutdown(currUser->sockNum, 2);
					 
						removeItem(all, (void *)currUser, &mutex);
						removeThread(threads, pthread_self(), &mutex);
						free(thread);
					}	
					recvBuffSize = recv(currUser->sockNum, dataRecv,MAXRECV, 0);
				}
            }
            else if(flag1 ==1 && flag2 ==0)
           		write(mySock, "password anda salah\n", 20);
           	else
           		write(mySock, "anda belum terdaftar\n", 21);
        }
         
		else 
			write(mySock, "404\n\n", 5);
        recvBuffSize = recv(mySock, dataRecv,MAXRECV, 0);
	}
}

void Die(char *message) 
{
  cancelThreads(threads);
  free(all);
  deleteList(threads);
  free(threads);
  printf("%s\n", message);
  exit(1);
}

