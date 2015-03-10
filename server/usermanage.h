#include <stdio.h>
#include <pthread.h>  
#include "server.h"

typedef struct Node Node;
typedef struct List List;

struct Node{
	Node *next;
	char *data;
};
struct List{
	Node *head;
};

void init(List *);
void insert(List *, char *, pthread_mutex_t *);
void *removeItem(List *,  char *, pthread_mutex_t *);
void *removeThread(List *, pthread_t data, pthread_mutex_t *mutex);
//wrongLogin *findWrongUser(List *, char *, pthread_mutex_t *);
userData *findUser(List *, char *,pthread_mutex_t * );
void allUser(List *, char *, void *, pthread_mutex_t *);
