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
using Xunit;

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
            List<Notifications> notis = SQLConnection.Get<Notifications>(typeof(Notifications), $"WHERE StudentID={noti.StudentID}").Result;
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
            var chatsBefore = SQLConnection.Get<Chats>(typeof(Chats), $"WHERE ChatID={chat.ChatID}");
            chat = new Chats() { Data = "lmao text but better", ChatID = 69, SenderID = 420 };
            SQLConnection.update(chat);
            var chatsAfterUpdate = SQLConnection.Get<Chats>(typeof(Chats), $"WHERE ChatID={chat.ChatID}");
            SQLConnection.delete(chat);
            var chatsAfterDel = SQLConnection.Get<Chats>(typeof(Chats), $"WHERE ChatID={chat.ChatID}").Result;
            NUnit.Framework.Assert.AreEqual(chatsAfterDel.Count, 0);
            NUnit.Framework.Assert.AreNotEqual(chatsAfterDel, chatsAfterUpdate);
        }

        [Test]
        public void TestCourse()
        {
            CoursesController coursesController = new CoursesController();
            var allCourses = coursesController.Get().Result;

        }
    }
}