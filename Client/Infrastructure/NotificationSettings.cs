using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Infrastructure
{
	public class NotificationSettings
	{
        private static NotificationSettings _instance;
        public static NotificationSettings GetInstance()
        {
            if (_instance == null)
                _instance = new NotificationSettings();
            return _instance;
        }

        public string Message { get; set; }
		public Enums.MessageType MessageType { get; set; }

        /// <summary>
        /// لیستی از پیغام های اعلان را بر می گرداند
        /// </summary>
        /// <param name="messages">لیستی از پیغام ها</param>
        /// <param name="messageType">نوع پیغام</param>
        /// <returns></returns>
		public IList<NotificationSettings> setNotificationsList(IList<string> messages, Infrastructure.Enums.MessageType messageType) 
		{
            IList<NotificationSettings> lstNotificationSettings = new List<NotificationSettings>();

            foreach (var message in messages)
            {
                lstNotificationSettings.Add(setNotification(message.ToString(), messageType));
            }

            return lstNotificationSettings;
        }

        /// <summary>
        /// یک پیغام اعلان را بر می گرداند
        /// </summary>
        /// <param name="message">پیغام</param>
        /// <param name="messageType">نوع پیغام</param>
        /// <returns></returns>
		public NotificationSettings setNotification(string message, Infrastructure.Enums.MessageType messageType) 
		{
            NotificationSettings notification = new NotificationSettings()
                {
                    Message = message.ToString(),
                    MessageType = messageType,
                };

            return notification;
        }
    }
}
