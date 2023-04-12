#define _GNU_SOURCE
#include <dlfcn.h>
#define _FCNTL_H
#include <sys/types.h>
#include <bits/fcntl.h>
#include <stddef.h>
#include <time.h>
#include <stdio.h>

long times_called = 0L;
int initial_sec = 0;

// int (*orig_clock_gettime)(clockid_t clockid, struct timespec *tp);
// int (*orig___clock_gettime)(clockid_t clockid, struct timespec *tp);
// time_t (*orig_time)(time_t *__timer);
int (*orig_gettimeofday)(struct timeval *tv, void * /*tzv*/);

// int clock_gettime(clockid_t clockid, struct timespec *tp) {

//     if(!orig_clock_gettime) orig_clock_gettime = dlsym(RTLD_NEXT, "clock_gettime");
//     int retval = orig_clock_gettime(clockid, tp);
//     if (!initial_sec) initial_sec = tp->tv_sec;
    
//     // printf("times_called: %ld, clock: %d\n", times_called, clockid);


//     if (clockid == 0) {
//         times_called++;
//         tp->tv_nsec = 621355967;
//         tp->tv_sec = 1621355967;
//         return 0;
//     } else {
//         return orig_clock_gettime(clockid, tp);
//     }
// }

// int __clock_gettime(clockid_t clockid, struct timespec *tp) {

//     if(!orig_clock_gettime) orig___clock_gettime = dlsym(RTLD_NEXT, "__clock_gettime");
//     int retval = orig___clock_gettime(clockid, tp);
//     if (!initial_sec) initial_sec = tp->tv_sec;
    
//     printf("__times_called: %ld, clock: %d\n", times_called, clockid);


//     if (clockid == 0) {
//         times_called++;
//         tp->tv_nsec = 621355967;
//         tp->tv_sec = 1621355967;
//         return 0;
//     } else {
//         return orig___clock_gettime(clockid, tp);
//     }
// }

// time_t time(time_t *__timer) {
//     if(!orig_time) orig_time = dlsym(RTLD_NEXT, "time");
//     // printf("time_call!\n");
//     return orig_time(__timer);
// }

int gettimeofday(struct timeval *tv, void * tzv) {
    if(!orig_gettimeofday) orig_gettimeofday = dlsym(RTLD_NEXT, "gettimeofday");
    // printf("gettimeofday\n");
    tv->tv_sec = -11644473600LL;
    //tv->tv_usec = 59748;
    tv->tv_usec = 1002333;
    return 0;
    
}

// seeds:
// 1002333 -> 0 att
// 59748 -> 8 att

// int __thread (*_open)(const char * pathname, int flags, ...) = NULL;
// int __thread (*_open64)(const char * pathname, int flags, ...) = NULL;

// int open(const char * pathname, int flags, mode_t mode)
// {
//     if (NULL == _open) {
//         _open = (int (*)(const char * pathname, int flags, ...)) dlsym(RTLD_NEXT, "open");
//     }
//     if(flags & O_CREAT)
//         return _open(pathname, flags | O_NOATIME, mode);
//     else
//         return _open(pathname, flags | O_NOATIME, 0);
// }

// int open64(const char * pathname, int flags, mode_t mode)
// {
//     if (NULL == _open64) {
//         _open64 = (int (*)(const char * pathname, int flags, ...)) dlsym(RTLD_NEXT, "open64");
//     }
//     if(flags & O_CREAT)
//         return _open64(pathname, flags | O_NOATIME, mode);
//     else
//         return _open64(pathname, flags | O_NOATIME, 0);
// }
