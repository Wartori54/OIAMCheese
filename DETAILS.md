# One in a million twice in a row

*I spent way too much time on this.*

This document is divided into two sections, an overview explaining in an undetailed way how this was done, and then a more in depth section.

## Before starting
Even though it may seem I was claiming I got this challenge by luck, I did not, all references to that were jokingly made.

## What's one in a million?
One in a million is a map that has a 1 in 1000000 million chance of being beaten, no skill involved whatsoever, this is achieved by some custom triggers that when the player collides with them they have a predetermined chance of killing the player.
Those triggers use a default random function, whose seed is determined by the system clock.

## Overview: How did i “cheese” One in a million
By the fact that the randomness is purely determined by the system's clock time, all i had to do was mess with that, and once i could choose any seed i just brute forced two seeds, one that gets it after 8 attempts, and another one that does it first try. If you watch the video you may have seen that between attempts I reboot the game. That's where in the background I swapped the seed.

### Technical explanation
Ingredients to make this work:
- Have a lot of free time
- A linux machine
- Perseverance
- Get really mad at MS for the proprietary code

__Prior knowledge:__
Celeste uses the dotnet framework 4.5.2, which under linux has to be run with mono. After digging through the code of Aurora’s helper, I saw it used Datetime.Now.Ticks. That uses the gettimeofday function from libc.

Knowing that i just had to find a way to intercept those function calls and alter its output in some way so that i can arbitrarily choose any seed.

#### Issue 1:
The first problem was that this function returns microseconds but dotnet wants 100s of nanoseconds, so any value that I sent would be multiplied by 10. That meant that you cannot get any seed as all the seed would end 010 in its binary form.

#### The “hack”:
When you code something it is not a good idea to do everything from scratch, so libraries were invented.
A library is a piece of software that defines a couple of functions that then any program can use.
Then you can link those libraries to your program and use their functionality and consequently speed up your development time a lot.
Libraries have become so fundamental to development that literally every binary out there at least uses one, libc is an example. That library is the most important one, because it gives you a way to interact with the OS in an easy way.

So it turns out that the function gettimeofday resides inside the current libc implementation, so all i had to do was “overwrite” that function.

#### Actually doing the “hack”:
From now on I’ll only describe how to do it in Linux, other kernels may have other ways of achieving this.

In Linux there’s something known as the LD_PRELOAD trick. Essentially is a way to load a library before all of the others get loaded, you use that to define some functions, later when the other libraries get loaded, if a function has already been defined, it will keep the one that was loaded first.
Using this I can write a small library with an implementation of the gettimeofday function, and return any value I would like.

__Finding seeds:__
Before you can get One in a million many times in a row, you have to find a seed that will get you to your victory, this can be easily done by bruteforcing with a simple script. I had to use two seeds because my (not too extensive) testing did not find any seed that would get one in a million twice in a row with 50 prior attempts. One may exist, but I didn't have the patience to actually find it. Those seeds are in the code.

#### Issue 2:
Looking at the code you may have seen that the seconds field is negative, that is to undo a conversion so that you can more easily modify the microseconds field to get any seed. This has some side effects, like logs not being created, but a workaround is to run celeste from the terminal and looking at the standard output.

#### The final note:
Something I haven't mentioned is that for this to work you’ll have to set the TZ env variable to "" so that your time zone doesn’t mess with the results.

#### The code
The code is in this repo if you want to check it out. There’s a lot of commented code from my previous tests. I kept it for if anyone wants to see what other tests i did.

__Why does `clock_gettime` appear in the code:__
At first I set up a dotnet console app with the latest version, that is 7.0. I did my research with that version and found it used clock_gettime, but later I realised that celeste uses 4.5.2 and consequently that first attempt did not succeed.

Note:

The mono code where `gettimeofday` gets used is here.

## Appendix
But what about the bounty?
I thought that this may be possible a while ago, but never got around to doing it. But after I saw the bounty I decided to attempt it and (surprisingly) succeeded.

### But does it count for the bounty?
Since it has been proven by Aurora (the map’s creator) that there’s no seed such that you can complete the bounty without restarting the game, I think it should count.
This technique doesn’t modify the games code in any way nor any mod’s code, and i think that this is the least cheaty possible way of completing this bounty.
But it's up to the bounty leader to decide, so that's just a suggestion.

\- Wartori
