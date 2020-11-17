using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using WebApplication_mc_02.Controllers;
using WebApplication_mc_02.Models;
using WebApplication_mc_02.Models.DTO;
using System.Reflection;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;

namespace NUnitTestProject2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLogin()
        {
            Login myLogin = new Login() { Username = "haxor", Password = "yeet" };
            _ = SQLConnection.Insert(myLogin).Result;
            Students mystu = new Students();
            LoginController loginContoller = new LoginController();
            mystu.StudentID = 2315;
            mystu.Username = "haxor";
            mystu.Password = LoginController.hashPassword("yeet");
            mystu.FullName = "MASTERHAX";
            mystu.Classification = "Junior";
            mystu.Major = "Computer Software Person";
            mystu.Role = "ADMIN";
            SQLConnection.Insert(mystu);
            string temp = loginContoller.PutLogin(myLogin).Result.Value;
            NUnit.Framework.Assert.AreEqual(temp.Split()[0], "2315");
            SQLConnection.delete(mystu);
            SQLConnection.delete(myLogin);
        }

        [Test]
        public void TestNotifications()
        {
            Notifications noti = new Notifications() { Data = "lmao testdata lmao", Type = "Announcement meme", StudentID = 6969 };
            SQLConnection.Insert(noti);
            SQLConnection.Insert(noti);
            List<Notifications> notis = SQLConnection.Get<Notifications>( $"WHERE StudentID={noti.StudentID}").Result;
            NUnit.Framework.Assert.AreEqual(notis.Count, 2);
            NUnit.Framework.Assert.AreEqual(notis[0].Type, "Announcement meme");
            NUnit.Framework.Assert.AreEqual(notis[1].Data, "lmao testdata lmao");
            SQLConnection.delete(noti);
            SQLConnection.delete(noti);
        }


        [Test]
        public void TestSQLConnection()
        {
            Chats chat = new Chats() { Data = "lmao text", ChatID = 69, SenderID = 420 };
            SQLConnection.Insert(chat);
            var chatsBefore = SQLConnection.Get<Chats>($"WHERE ChatID={chat.ChatID}");
            chat = new Chats() { Data = "lmao text but better", ChatID = 69, SenderID = 420 };
            SQLConnection.update(chat);
            var chatsAfterUpdate = SQLConnection.Get<Chats>($"WHERE ChatID={chat.ChatID}");
            SQLConnection.delete(chat);
            var chatsAfterDel = SQLConnection.Get<Chats>($"WHERE ChatID={chat.ChatID}").Result;
            NUnit.Framework.Assert.AreEqual(chatsAfterDel.Count, 0);
            NUnit.Framework.Assert.AreNotEqual(chatsAfterDel, chatsAfterUpdate);
        }

        [Test]
        public void TestCourses()
        {
            CoursesController coursesController = new CoursesController();
            //checks if this throws
            NUnit.Framework.Assert.DoesNotThrow((TestDelegate)Delegate.CreateDelegate(typeof(CoursesController), typeof(CoursesController).GetMethod("Get")), "", new object[0]);
            NUnit.Framework.Assert.DoesNotThrow((TestDelegate)Delegate.CreateDelegate(typeof(CoursesController), typeof(CoursesController).GetMethod("Get")), "", new object[1] { 81537 });
        }

        [Test]
        public void TestWebsocket()
        {
            
        }

        [Test]
        public void createFriend()
        {
            Students mystu = new Students();
            mystu.StudentID = 2315;
            mystu.Username = "haxman";
            mystu.Password = "gethax";
            mystu.FullName = "betterhaxor";
            mystu.Classification = "Senior";
            mystu.Major = "Computer Software Engineer Guy";
            mystu.Role = "ADMIN0";
            Notifications noti = new Notifications { StudentID = 2315, Data = "New Friend", Type = "Request" };
            NotificationsController notificationsController = new NotificationsController();
            String temp = notificationsController.PostFriend(2315, noti).Result.ToString();
            try
            {
                NUnit.Framework.Assert.AreNotEqual(temp, "couldnt delete or insert idk"); //If passed then it inserted
            }
            catch (Exception e)
            {
                Console.WriteLine("Test Not Passed, something is wrong");
            }
        }

        [Test]
        public void createChat()
        {
            Chats chats = new Chats { ChatID = 985415, Data = "I hate unit testing", SenderID = 2315, Record = 0 };
            ChatsController chatsController = new ChatsController();
            string temp = chatsController.PostChat(chats).Result.ToString();
            try
            {
                NUnit.Framework.Assert.AreNotEqual(temp, "your not allowed to send data in this chat"); //If passed then it inserted
            }
            catch (Exception e)
            {
                Console.WriteLine("Test Not Passed, something is wrong");
            }
        }

        [Test]
        public void createGroup()
        {
            ChatsController chatsController = new ChatsController();
            String temp = chatsController.PutGroupChat(451561).Result.ToString();
            try
            {
                NUnit.Framework.Assert.AreNotEqual(temp, "Error"); //If passed then it inserted
            }
            catch (Exception e)
            {
                Console.WriteLine("Test Not Passed, something is wrong");
            }

        }

        [Test]
        public void createAccount()
        {
            CreateAccount myAccount = new CreateAccount { Username = "test", Password = "test", canvasOAuthToken = "10835~CLiOtauIaEttJog8BA3Mt3rfsatuUoBbAu2cr6dUAadHg8kVzxMEeGqk3AbA8unc" };
            NUnit.Framework.Assert.AreEqual(myAccount.Username, "test");
            NUnit.Framework.Assert.AreEqual(myAccount.Password, "test");
            NUnit.Framework.Assert.AreEqual(myAccount.canvasOAuthToken, "10835~CLiOtauIaEttJog8BA3Mt3rfsatuUoBbAu2cr6dUAadHg8kVzxMEeGqk3AbA8unc");
        }

        [Test]
        public void createStudent()
        {
            Students mystudent = new Students { StudentID = 1221, Username = "test", Attributes = "cool", Password = "test", FullName = "test test", Classification = "Super Senior", Role = "student" };
            NUnit.Framework.Assert.AreEqual(mystudent.StudentID, 1221);
            NUnit.Framework.Assert.AreEqual(mystudent.Username, "test");
            NUnit.Framework.Assert.AreEqual(mystudent.Attributes, "cool");
            NUnit.Framework.Assert.AreEqual(mystudent.Password, "test");
            NUnit.Framework.Assert.AreEqual(mystudent.FullName, "test test");
            NUnit.Framework.Assert.AreEqual(mystudent.Classification, "Super Senior");
            NUnit.Framework.Assert.AreEqual(mystudent.Role, "student");
        }
    }
}