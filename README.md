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
- Monitor Clipboard for BTC Addresses and Replace with Attacker's defined Address  


## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Security Considerations](#security-considerations)
- [C2 Components](#C2-Components)
- [Contributing](#contributing)

## Youtube Demo
[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/EsMQl13e8bs/0.jpg)](https://www.youtube.com/watch?v=EsMQl13e8bs&ab_channel=MohammedHattab)  


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

The executable file produced by the project will enable the activation of a reverse shell. Simply execute the exe file to initiate the reverse shell, which will establish a connection with the command and control server, ready to receive and execute commands.

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
[Malware Distribution Phishing Page](https://github.com/The-Hustler-Hattab/obs-project-phishing)  

## Contributing
Contributions are welcome! 
Please make a pull request for new features
