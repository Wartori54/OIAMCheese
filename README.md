# The code for "hacking" One in a million
Link to the map in question: https://gamebanana.com/mods/396432

Explanation: [DETAILS.md](https://github.com/Wartori54/OIAMCheese/blob/master/DETAILS.md)

This repo contains the code that was used to beat this celeste map twice in a row.

## Running and compiling

Compile lib with: `./build_ld.sh ldintercept.c`

Run the c# programs with: `dotnet run`

Run Celeste with: `TZ="" LD_PRELOAD="$¨{PATH_TO_THE_LIB}/ldintercept.so" ./Celeste`
