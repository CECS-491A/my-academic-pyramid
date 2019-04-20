using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Messenger
{
    public class MessengerContactHist : IEntity
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public DateTime ContactTime { get; set; }

        public int Id { get; set; }

        //int IComparable.CompareTo(object obj)
        //{
        //    MessengerContactHist m = (MessengerContactHist)obj;
        //    if(this.SenderUserName.Equals(m.SenderUserName) && this.ReceiverUserName.Equals(m.ReceiverUserName))
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return -1;
        //    }
           
        //}
    }
}
