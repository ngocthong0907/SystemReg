using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;
using System.Reflection;
namespace NinjaSystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 10;
            string result = "";
            try
            {
                while (i > 0)
                {
                    OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client();
                    client.Connect("outlook.office365.com", 995, true);
                    client.Authenticate("lw00sv17@outlook.com", "der79yy6Pfv",OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);
                    var count = client.GetMessageCount();
                    OpenPop.Mime.Message message = client.GetMessage(count);
                    OpenPop.Mime.MessagePart text = message.FindFirstPlainTextVersion();
                    OpenPop.Mime.Header.MessageHeader header = message.Headers;
                    //  MessageBox.Show(text.());

                    string email = header.Subject.ToString();
                    result = email.Substring(0, 5);
                    client.Disconnect();
                    if (!string.IsNullOrEmpty(result))
                        break;
                   // Delay(3);
                    i--;
                }
            }
            catch 
            {
               // File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + " : " + ex.Message + "\n");
            }

          //  return result;
            //try
            //{
            //  //  Microsoft.Office.Interop.Outlook.Application outlookApplication = null;
            //  //  NameSpace outlookNamespace = null;
            //  //  MAPIFolder inboxFolder = null;
            //  //  Items mailItems = null;

            //  //  outlookApplication = new Microsoft.Office.Interop.Outlook.Application();
            //  //  outlookNamespace = outlookApplication.GetNamespace("MAPI");
            //  //  inboxFolder = outlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
            //  //  mailItems = inboxFolder.Items;

            //  ////  Console.WriteLine(mailItems.Count);

            //  //  foreach (MailItem item in mailItems)
            //  //  {
            //  //      var stringBuilder = new StringBuilder();
            //  //      stringBuilder.AppendLine("From: " + item.SenderEmailAddress);
            //  //      stringBuilder.AppendLine("To: " + item.To);
            //  //      stringBuilder.AppendLine("CC: " + item.CC);
            //  //      stringBuilder.AppendLine("");
            //  //      stringBuilder.AppendLine("Subject: " + item.Subject);
            //  //      stringBuilder.AppendLine(item.Body);

            //  //     // Console.WriteLine(stringBuilder);
            //  //      Marshal.ReleaseComObject(item);
            //  //  }


            //}
            ////Error handler.
            //catch 
            //{
            //   // Console.WriteLine("{0} Exception caught: ", e);
            //}

//            try
//            {
//                // Create the Outlook application.
//                // in-line initialization
//                Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();

//                // Get the MAPI namespace.
//                Microsoft.Office.Interop.Outlook.NameSpace oNS = oApp.GetNamespace("mapi");

//                // Log on by using the default profile or existing session (no dialog box).
//                oNS.Logon(Missing.Value, Missing.Value, false, true);

//                // Alternate logon method that uses a specific profile name.
//                // TODO: If you use this logon method, specify the correct profile name
//                // and comment the previous Logon line.
//                //oNS.Logon("profilename",Missing.Value,false,true);

//                //Get the Inbox folder.
//                Microsoft.Office.Interop.Outlook.MAPIFolder oInbox = oNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);

//                //Get the Items collection in the Inbox folder.
//                Microsoft.Office.Interop.Outlook.Items oItems = oInbox.Items;

//                // Get the first message.
//                // Because the Items folder may contain different item types,
//                // use explicit typecasting with the assignment.
//                Microsoft.Office.Interop.Outlook.MailItem oMsg = (Microsoft.Office.Interop.Outlook.MailItem)oItems.GetFirst();

//                //Output some common properties.
//                Console.WriteLine(oMsg.Subject);
//                Console.WriteLine(oMsg.SenderName);
//                Console.WriteLine(oMsg.ReceivedTime);
//                Console.WriteLine(oMsg.Body);

//                //Check for attachments.
//                int AttachCnt = oMsg.Attachments.Count;
//                Console.WriteLine("Attachments: " + AttachCnt.ToString());

//                //TO DO: If you use the Microsoft Outlook 10.0 Object Library, uncomment the following lines.
//                /*if (AttachCnt > 0) 
//                {
//                for (int i = 1; i <= AttachCnt; i++) 
//                 Console.WriteLine(i.ToString() + "-" + oMsg.Attachments.Item(i).DisplayName);
//                }*/

//                //TO DO: If you use the Microsoft Outlook 11.0 Object Library, uncomment the following lines.
//                /*if (AttachCnt > 0) 
//                {
//                for (int i = 1; i <= AttachCnt; i++) 
//                 Console.WriteLine(i.ToString() + "-" + oMsg.Attachments[i].DisplayName);
//                }*/


//                //Display the message.
//                oMsg.Display(true);  //modal

//                //Log off.
//                oNS.Logoff();

//                //Explicitly release objects.
//                oMsg = null;
//                oItems = null;
//                oInbox = null;
//                oNS = null;
//                oApp = null;
//            }

////Error handler.
//            catch (System.Exception exx)
//            {
//                Console.WriteLine("{0} Exception caught: ", exx);
//            }
        }
    }
}
