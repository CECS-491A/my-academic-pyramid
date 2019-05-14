using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ManagerLayer.Archiving;

namespace KFC.SIT.WebAPI
{
    public class PeriodicArchiver
    {
        public void archiveCall()
        {
            while (true)
            {
                ArchivingManager archiver = new ArchivingManager();
                var response = archiver.ArchiveOldLogFiles();
                if (response == false)
                {
                    archiver.contactAdmin();
                }
                Thread.Sleep(1000 * 60);
            }
        }
    }

    public class archiverMain
    {
        PeriodicArchiver archive = new PeriodicArchiver();
        public void Main()
        {
            Thread thr = new Thread(new ThreadStart(archive.archiveCall));
            thr.Start();
            while (true)
            {

            }
        }
    }

}