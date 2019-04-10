using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OperationContracts;

namespace Kurs_adonet
{
    public class ChatViewModel : INotifyPropertyChanged,ServiceFF.IChatServiceCallback
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public ObservableCollection<MessageViewModel> Messages { set; get; }
        public string MessageText { set; get; }

        DelegateCommand _sendMessage;
        public ICommand SendMessage
        {
            get
            {
                if (_sendMessage==null)
                {
                    _sendMessage = new DelegateCommand(param => SendMyMessage(), null);
                }
                return _sendMessage;
            }
        }
        ServiceFF.ChatServiceClient _chatServiceClient;
        ILoginRegisterUser _loginRegisterUser;

        public ChatViewModel(ILoginRegisterUser user)
        {
            _loginRegisterUser = user;
            InstanceContext instanceContext = new InstanceContext(this);
            _chatServiceClient = new ServiceFF.ChatServiceClient(instanceContext);
            _chatServiceClient.AddNewUserChatService();
            Messages = new ObservableCollection<MessageViewModel>();

        }
        void SendMyMessage()
        {
            _chatServiceClient.SendMessage(new MessageData() { NickName = _loginRegisterUser.GetCurrentUser(), Message = MessageText, MessageTime = DateTime.Now.ToString() });

        }

        public void SetMessage(MessageData msg)
        {
            
            Messages.Add(new MessageViewModel() { NickName = msg.NickName.Login,UserImage = new ImageConverter().ByteToBitmapImage(msg.NickName.UserImage), Message = msg.Message });
        }
    }
}
