# CpuBurner
Simple .NET 4.x console application that will eat CPU time on all available cores.

## Usage

1. Clone this repo to any workstation (even non-Windows since exe is already included) with CF CLI installed.
2. `cf push` from the root repo dir.

By default the console application will consume 25% of each CPU core for 5 minutes. On an environment with 4 cores you should see close to 100% CPU usage. 25% + 25% + 25% + 25% = 100%.

## Configuration

Edit the manifest.yml to adjust the percentage of CPU usage and run length:
```
    CPU_USAGE_PERCENTAGE: 25
    RUN_TIME_IN_SECONDS: 360
```
