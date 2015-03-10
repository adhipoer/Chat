#define MAXCHAR 50
#define MAXUSER 100
#define MAXCHAR 100
typedef struct userData
{
	unsigned long IP;
	int sockNum;
	char userName[MAXCHAR];
	char userPass[MAXCHAR];
	int loggedIn;

}userData;

typedef struct request
{
	unsigned long IP;
	int sockNum;
}request;

typedef struct wrongLogin
{
	char userName[MAXCHAR];
	int wrongUser;
}wrongLogin;
