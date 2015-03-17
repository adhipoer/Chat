#define MAXUSER 100
#define MAXCHAR 100

typedef struct UserData UserData;
typedef struct Request Request;
struct UserData{
	char IP[MAXCHAR];
	int sockNum;
	char userName[MAXCHAR];
	char userPass[MAXCHAR];
	int loggedIn;

};
struct Request{
	char IP[MAXCHAR];
	int sockNum;
};

//typedef struct wrongLogin wrongLogin;
//struct wrongLogin{
//	char userName[MAXCHAR];
//	int wrongUser;
//};
