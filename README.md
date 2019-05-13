# .NET Core DockerVolumeWatcher

Forked from https://github.com/Angelinsky7/Docker-Volume-Watcher

## To use:
```
dotnet tool install --global DockerVolumeWatcher
dotnet-docker-volume-watcher
```

All mounted volumes will be watched automatically

## .dockerignore file
Use a .dockerignore file to mark files that shouldn't be watched.

```
node_modules/*
.npm/*
```
