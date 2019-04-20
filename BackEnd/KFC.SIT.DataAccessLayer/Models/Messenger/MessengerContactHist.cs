using System;

namespace DataAccessLayer.Models.Messenger
{
    public class MessengerContactHist : IComparable, IEntity
    {
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
        public DateTime ContactTime { get; set; }

        public int Id { get; set; }

        int IComparable.CompareTo(object obj)
        {
            MessengerContactHist m = (MessengerContactHist)obj;
            if(this.SenderUserName.Equals(m.SenderUserName) && this.ReceiverUserName.Equals(m.ReceiverUserName))
            {
                return 0;
            }
            else
            {
                return -1;
            }
           
        }
    }
}
