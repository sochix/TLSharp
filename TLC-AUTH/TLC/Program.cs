using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLSharp;
using TeleSharp.TL;
using TLSharp.Core;
namespace TLC
{
    class Program
    {

        static string phoneCodeHash;
        static void Main(string[] args)
        {

            Run();



        }
      static  async void Run()
        {
            var store = new FakeSessionStore();
            string Alert = "ciao";
           TLUser user = null;
            var client = new TelegramClient(117, "b42", store);
            // if i call client with await it doesn't work
            client.ConnectAsync();
            if (client.IsUserAuthorized())
            {
                //get available contacts
                var result = await client.GetContactsAsync();

                //find recipient in contacts
             //   var userr = result.users.lists.Where(x => x.GetType() == typeof(TLUser)).Cast<TLUser>().FirstOrDefault(x => x.phone == "98" + TextBox1.Text);

                //send message
                await client.SendMessageAsync(new TLInputPeerUser() { user_id = user.id }, Alert);
            }
            else
            {
                //excption here
                phoneCodeHash = await client.SendCodeRequestAsync("39");
                Task.WaitAll();

            }

        }
    }
}
