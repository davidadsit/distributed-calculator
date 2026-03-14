# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build and Test Commands

```bash
# Build entire solution
dotnet build

# Run unit tests (coordinator.tests)
dotnet test coordinator.tests

# Run acceptance tests (coordinator.acceptancetests)
dotnet test coordinator.acceptancetests

# Run all tests
dotnet test

# Run a single test
dotnet test --filter "FullyQualifiedName~JobPoolTests.GetNextJob_gets_a_job_of_the_appropriate_difficulty"

# Run the coordinator (ASP.NET Core web app, defaults to http://localhost:5000)
dotnet run --project coordinator

# Run a worker (ASP.NET Core web app)
dotnet run --project worker

# Build and test Docker image (coordinator only)
docker build -t coordinator-test coordinator/
docker run -d --name coordinator-test-container -p 8080:80 coordinator-test
curl http://localhost:8080/
docker stop coordinator-test-container && docker rm coordinator-test-container
```

## Docker

The coordinator has a Dockerfile at `coordinator/Dockerfile`. When upgrading .NET versions:
1. Update the base images (`mcr.microsoft.com/dotnet/aspnet` and `mcr.microsoft.com/dotnet/sdk`) to match the target framework
2. Build the Docker image and run it to verify it works
3. Test that the web app responds (e.g., `curl http://localhost:8080/`)

## Architecture Overview

This is a distributed calculator system built for a coding exercise. It consists of two ASP.NET Core web applications that communicate via REST APIs.

### Coordinator (coordinator/)
The central server that manages workers and assigns calculation jobs. Key components:
- **WorkerRegistry** (`Application/WorkerRegistry.cs`) - Tracks registered workers in a `ConcurrentDictionary`. Workers become inactive after 4 consecutive failed responses.
- **JobPool** (`Application/JobPool.cs`) - Generates math problems with increasing difficulty based on worker's correct response count. Difficulty levels 0-2 are generated dynamically; higher levels use predefined problems (Roman numerals, complex math, algebra).
- **JobAssignmentService** (`Application/JobAssignmentService.cs`) - Background `IHostedService` that polls every 5 seconds, sending jobs to active workers via their `CreateJobEndpoint` and reporting errors to their `ErrorCheckEndpoint`.
- **RegisterController** - Accepts worker registration with team name and callback endpoints.
- **StatusController** - Displays active/inactive workers and recent job assignments.

### Worker (worker/)
A sample calculator implementation that receives jobs from the coordinator:
- **CreateJobController** - Receives calculation requests, parses simple arithmetic (two operands with +, -, *, /), returns result.
- **RegisterController** - UI for registering with a coordinator instance.

### Communication Flow
1. Worker registers with coordinator (POST /register) providing callback URLs
2. Coordinator's JobAssignmentService periodically sends jobs to workers' CreateJobEndpoint
3. Worker calculates result and returns it
4. Coordinator tracks correct/incorrect/failed responses per worker
5. Coordinator sends error notices to workers' ErrorCheckEndpoint when results are incorrect

### Test Projects
- **coordinator.tests** - NUnit unit tests for JobPool (difficulty progression, problem generation)
- **coordinator.acceptancetests** - NUnit integration tests using `WebApplicationFactory` to test registration endpoint
