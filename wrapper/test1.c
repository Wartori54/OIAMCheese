#include <time.h>
#include <sys/time.h>
#include <stdio.h>
#include <unistd.h>

int main() {
    struct timespec currtime;
    clock_gettime(298, &currtime);
    time(NULL);
    struct timeval current_time;
    gettimeofday(&current_time, NULL);
    printf("%ld\n", currtime.tv_nsec);
    printf("%ld\n", currtime.tv_sec);
    __uint64_t target = 0;
    __int64_t magnum = -62135596738 + target; // 1355967
    __uint64_t key1 = 621355968000000000L;
    __uint64_t key2 = 0x4000000000000000;
    printf("%ld\n", magnum);
    printf("%ld\n", magnum + key1);
    printf("%ld\n", (magnum + key1) | key2);
    printf("%ld\n", ((magnum + key1) | key2) & 0x7FFFFFFF);
    __int64_t magnum_modif = 10000000 * magnum + magnum / 100;
    printf("%ld\n", magnum_modif);
    printf("%ld\n", magnum_modif + key1);
    printf("%ld\n", (magnum_modif + key1) | key2);
    printf("%ld\n", ((magnum_modif + key1) | key2) & 0x7FFFFFFF);
}
