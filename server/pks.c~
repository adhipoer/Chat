#include <sys/socket.h>
#include <arpa/inet.h>
#include <sys/types.h>
#include <unistd.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <pthread.h>

#include "usermanage.h"

#define FILENAME "pk.txt"
#define LINES 200
#define MAXRECV 1000

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
char usernames[LINES][MAXCHAR];
char pubkey[LINES][MAXCHAR];
List *threads;
int servFd;

void *loginThread(void *);
void Die(char *message);
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
	
	threads =(List *) malloc(sizeof(List ));
	threads->head = NULL;

	cliLen = sizeof(cli_addr);
	portNum = atoi(argv[1]);
	
	servFd = socket(AF_INET, SOCK_STREAM, 0);
	memset(&serv_addr, 0, sizeof(serv_addr));

	serv_addr.sin_family = AF_INET;
	serv_addr.sin_addr.s_addr= inet_addr("127.0.0.1");
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
	char signbuff[MAXCHAR];
	char nameBuff[MAXCHAR];
	char pkBuff[MAXCHAR];
	char dataRecv[MAXRECV];
	int recvBuffSize;
    
    memset(dataRecv, 0, MAXRECV);
	recvBuffSize = recv(mySock, dataRecv,MAXRECV, 0);
	while (recvBuffSize != 0)
	{
        sscanf(dataRecv, "%s %s %s", signbuff, nameBuff, pkBuff);
        printf("%s\n", signbuff);
        if(!strcmp(signbuff, "store"))
        {
        	int flag = 0;
        	int y,z;
			for(z = 0; z<LINES; z++) {
				for(y = 0; y < MAXCHAR; y++) { 
					usernames[z][y] = 0;
					pubkey[z][y] = 0;
				}
			}
			fp = fopen(FILENAME, "r");
			if (fp == NULL)
				printf("fopen() failed");
			
			int a = 0;
			
			while(fscanf(fp, "%s %s\n", usernames[a], pubkey[a])!=EOF){
				a++;
			}
			printf("%d\n", a);
			int i;
			int c =0;
			for(i = a; i >=0; i--)
			{	
				if(!strcmp(usernames[i], nameBuff))
				{
					flag = 1;
				}
				
			}
			fclose(fp);
			if(flag == 1){
				write(mySock, "maaf, pk sudah ada di server\n", 28);
            }
            else
            {
            	fp = fopen(FILENAME, "a");
	    		if (fp == NULL)       				
					printf("fopen() failed");
            	fprintf(fp, "%s %s\n", nameBuff, pkBuff);
            	fclose(fp);
            	write(mySock, "pk sudah di server\n", 20);
            }
            
        }
     	else if (!strcmp(signbuff, "get"))
     	{
     		
        	int y,z;
			for(z = 0; z<LINES; z++) {
				for(y = 0; y < MAXCHAR; y++) { 
					usernames[z][y] = 0;
					pubkey[z][y] = 0;
				}
			}
			fp = fopen(FILENAME, "r");
			if (fp == NULL)
				printf("fopen() failed");
			
			int a = 0;
			
			while(fscanf(fp, "%s %s\n", usernames[a], pubkey[a])!=EOF){
				a++;
			}
			printf("%d\n", a);
			int i;
			int c =0;
			for(i = a; i >=0; i--)
			{	
				if(!strcmp(usernames[i], nameBuff))
				{	
					int n;
					n = strlen(pubkey[i]);
					write(mySock, pubkey[i], n);
				}
				
			}
			
     	}
     	else
     	{
     		shutdown(mySock, 2);
     		removeThread(threads, pthread_self(), &mutex);
     		free(thread);
     	}
     	recvBuffSize = recv(mySock, dataRecv,MAXRECV, 0);   
    }

}
void Die(char *message) 
{
  cancelThreads(threads);
  deleteList(threads);
  free(threads);
  printf("%s\n", message);
  exit(1);
}
