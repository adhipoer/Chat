#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>  
#include "server.h"

typedef struct Node Node;
typedef struct List List;

struct Node{
	Node *next;
	void *data;
};
struct List{
	Node *head;
};

void initialize(List *);
void addNew(List *, void *, pthread_mutex_t *);
void *removeItem(List *,  void *, pthread_mutex_t *);
void *removeThread(List *, pthread_t data, pthread_mutex_t *mutex);
//wrongLogin *findWrongUser(List *, char *, pthread_mutex_t *);
UserData *findUser(List *, char *, pthread_mutex_t * );
void allUser(List *, char *, void *, pthread_mutex_t *);
void deleteList(List *);
void cancelThreads(List *);
