#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "usermanage.h"

void init(list *list)
{
	list->head = NULL;
}

void adduser(list *list, char *name, char *pass, pthread_mutex_t *mutex)
{
	pthread_mutex_lock(mutex);
	user *head = list->head;
	user *newUser = (user *)malloc(sizeof(user));
	newUser->username = name;
	newUser->userpass = pass;
	newUser->next = head;
	list->head =newUser;
	pthread_mutex_unlock(mutex);
}
void* removeItem(list *,  char *username, char *userpass, pthread_mutex_t *)
{
	
}
void* removeThread(list *list, pthread_t data, pthread_mutex_t *mutex);
int find(list *, char *username, char *userpass, pthread_mutex_t *);





