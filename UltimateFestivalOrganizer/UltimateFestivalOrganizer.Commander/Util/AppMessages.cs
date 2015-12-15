using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.Commander.Util
{
    

    public static class AppMessages
    {
        public enum MessageTypes
        {
            ArtistChangedMessage,
            CatagoryChangedMessage,
            VenueChanged,
            SuccessMessage,
            ErrorMessage
        }
        public enum ChangeType
        {
            Add,
            Remove,
            Change
        }

        public static class ArtistChanged
        {
            
            public static void Send(ChangeType type)
            {
                Messenger.Default.Send(type, MessageTypes.ArtistChangedMessage);
            }
            public static void Register(object recipient, Action<ChangeType> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.ArtistChangedMessage, action);
            }
        }

        public static class CatagoryChanged
        {
            public static void Send(ChangeType type)
            {
                Messenger.Default.Send(type, MessageTypes.CatagoryChangedMessage);
            }
            public static void Register(object recipient, Action<ChangeType> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.CatagoryChangedMessage, action);
            }
        }

        public static class VenueChanged
        {
            public static void Send(ChangeType type)
            {
                Messenger.Default.Send(type, MessageTypes.VenueChanged);
            }
            public static void Register(object recipient, Action<ChangeType> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.VenueChanged, action);
            }
        }
        public static class ShowSuccessMessage
        {
            public static void Send(string message)
            {
                Messenger.Default.Send(message, MessageTypes.SuccessMessage);
            }
            public static void Register(object recipien, Action<string> action)
            {
                Messenger.Default.Register(recipien, MessageTypes.SuccessMessage, action);
            }
        }

        public static class ShowErrorMessage
        {
            public static void Send(string message)
            {
                Messenger.Default.Send(message, MessageTypes.ErrorMessage);
            }
            public static void Register(object recipien, Action<string> action)
            {
                Messenger.Default.Register(recipien, MessageTypes.ErrorMessage, action);
            }
        }
    }

}