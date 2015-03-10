
#include <sys/socket.h>
#include <arpa/inet.h>
#include <sys/types.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <pthread.h>
#include "server.h"
#include "usermanage.h"

#define FILENAME "userpass.txt"
#define MAXRECV 1000
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

	int a;
	int cliFd;
	int portNum;
	int cliLen;

	FILE *fp;

	struct sockaddr_in serv_addr;
	struct sockaddr_in cli_addr;
	pthread_t *thread;
	request *req;
	userData *user;
	all= malloc(sizeof(List));
	threads = malloc(sizeof(List));
	
	cliLen = sizeof(cli_addr);



	
}
