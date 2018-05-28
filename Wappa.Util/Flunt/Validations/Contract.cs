using Wappa.Util.Flunt.Notifications;

namespace Wappa.Util.Flunt.Validations
{
    public partial class Contract : Notifiable
    {
        public Contract Requires()
        {
            return this;
        }

        public Contract Join(params Notifiable[] items)
        {
            if (items != null)
            {
                foreach (var notifiable in items)
                {
                    if (notifiable.Invalid)
                        AddNotifications(notifiable.Notifications);
                }
            }

            return this;
        }
    }
}