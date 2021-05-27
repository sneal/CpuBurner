FROM mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2019

WORKDIR /burn
COPY ./CpuBurner/bin/Release/net4.5/win81-x64/publish/CpuBurner.exe .
ENTRYPOINT ["CpuBurner.exe"]
