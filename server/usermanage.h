#include <stdio.h>
#include <stdlib.h>

typedef struct user{
	char name[20];
	char pass[20];
	struct user *next;
}user_n;


