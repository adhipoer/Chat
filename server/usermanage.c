#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "usermanage.h"

void initialize(List *list)
{
	list->head = NULL;
}

void addNew(List *list, void* dat, pthread_mutex_t *mutex)
{
	pthread_mutex_lock(mutex);
	Node *head = list->head;
	Node *newUser = (Node *)malloc(sizeof(Node));
	newUser->data = dat;
	newUser->next = head;
	list->head =newUser;
	pthread_mutex_unlock(mutex);
}
void* removeItem(List *list,  void *dat, pthread_mutex_t *mutex)
{
	pthread_mutex_lock(mutex);
	Node *curr, *prev;
	prev=NULL;
	for(curr = list->head; curr!=NULL; prev=curr, curr=curr->next)
	{
		if(curr->data == dat){
			if(prev ==NULL)
				list->head = curr->next;
			else
				prev->next = curr->next;
			void *tmp = curr->data;
			free(curr);
			pthread_mutex_unlock(mutex);
			return 	tmp;
		}
	}
	pthread_mutex_unlock(mutex);
	return NULL;
		
}
void* removeThread(List *list, pthread_t data, pthread_mutex_t *mutex)
{
	Node *curr, *prev;
	prev = NULL;

	pthread_mutex_lock(mutex);
	for(curr = list->head; curr!=NULL;prev=curr, curr = curr->next)
	{
		if(*(pthread_t *)(curr->data) == data)
		{
			if(prev == NULL)
				list->head = curr->next;
			else
				prev->next = curr->next;
			void *tmp = curr->data;
			free(curr);
			pthread_mutex_unlock(mutex);
			return tmp;
		}

	}
	pthread_mutex_unlock(mutex);
	return NULL;
}

//wrongLogin* findWrongUser(List *, char *, pthread_mutex_t *);
UserData* findUser(List *list, char *name, pthread_mutex_t *mutex)
{
	UserData *found = NULL;
	int len = strlen(name);
	
	pthread_mutex_lock(mutex);
	Node *tmp = list->head;
	while(tmp)
	{
		UserData *tmpData = (UserData *)tmp->data;
		if(!strncmp(name, tmpData->userName, len-1))
		{
			found = tmpData;
			break;
		}
		tmp =tmp->next;
	}
	pthread_mutex_unlock(mutex);
	return found;
}
void allUser(List *list, char *userList, void *ptr, pthread_mutex_t *mutex)
{
	pthread_mutex_lock(mutex);
	Node *tmp = list->head;
	while(tmp)
	{
		UserData *data = (UserData *)tmp->data;
		if(data->loggedIn && data!=ptr)
		{
			strcat(userList,data->userName);
			strcat(userList, "\n");
		}
		tmp=tmp->next;
	}	
	pthread_mutex_unlock(mutex);
}

void *popFront(List *list) { 
  if (!list->head) 
    return NULL; 
  struct Node *oldHead = list->head; 
  list->head = oldHead->next; 
  void *data = oldHead->data; 
  free(oldHead); 
  return data; 
}
void deleteList(List *list) {
  while (list->head) {
    void *temp = popFront(list);
    free(temp);
  }
}

void cancelThreads(List *list) {
  Node *temp = list->head;
  while(temp) {
    pthread_t *thread = (pthread_t *)temp->data;
    printf("Cancelling thread %ld\n", *thread);
    pthread_cancel(*thread);
    temp=temp->next;
  }
}

