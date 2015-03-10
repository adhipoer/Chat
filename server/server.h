#define MAXUSER 100
#define MAXCHAR 100

typedef struct userData userData;

struct userData{
	unsigned long IP;
	int sockNum;
	char userName[MAXCHAR];
	char userPass[MAXCHAR];
	int loggedIn;

};

typedef struct request request;

struct request{
	unsigned long IP;
	int sockNum;
};

typedef struct wrongLogin wrongLogin;
struct wrongLogin{
	char userName[MAXCHAR];
	int wrongUser;
};
