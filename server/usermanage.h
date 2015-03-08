#include <stdio.h>
#include <pthread.h>  

typedef struct user user;
typedef struct list list;

struct user{
	user *next;
	char *username;
	char *userpass;
};
struct list{
	user *head;
};

void init(list *);
void adduser(list *, char *, char *, pthread_mutex_t *);
void* removeItem(list *,  char *, char *, pthread_mutex_t *);
void* removeThread(list *list, pthread_t data, pthread_mutex_t *mutex);
int find(list *, char *, char *, pthread_mutex_t *);
