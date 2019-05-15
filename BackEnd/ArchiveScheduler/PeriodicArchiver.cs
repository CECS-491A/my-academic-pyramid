using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ManagerLayer.Archiving;


namespace ArchiveScheduler

{
    
    public class PeriodicArchiver
    {
        public static void ArchiveCall()
        {
            while (true)
            {

                ArchivingManager archiver = new ArchivingManager();
                var response = archiver.ArchiveOldLogFiles();
                if (response == false)
                {
                    archiver.ContactAdmin();
                }
                Thread.Sleep(1000 * 60);
            }
        }
    }

    class Program
    {
        
        static void Main(String[] args)
        {
            
            Thread thr = new Thread(new ThreadStart(PeriodicArchiver.ArchiveCall));
            thr.Start();
            while (true)
            {

            }
        }
    }

}

