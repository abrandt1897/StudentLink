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
            SQLConnection.insert(myLogin);
            Students mystu = new Students();
            LoginController loginContoller = new LoginController();
            mystu.StudentID = 2315;
            mystu.Username = "haxor";
            mystu.Password = LoginController.hashPassword("yeet");
            mystu.FullName = "MASTERHAX";
            mystu.Classification = "Junior";
            mystu.Major = "Computer Software Person";
            mystu.Role = "ADMIN";
            SQLConnection.insert(mystu);
            String temp;
            temp = loginContoller.PutLogin(myLogin).Value;
            NUnit.Framework.Assert.AreEqual(temp.Split()[0], "2315");
        }

        [Test]
        public void TestNotifications()
        {
            Notifications noti = new Notifications() { Data = "lmao testdata lmao", Type = "Announcement meme", StudentID = 6969 };
            SQLConnection.insert(noti);
            SQLConnection.insert(noti);
            List<dynamic> notis = ChatsController.GetNotifications(6969).Result;
            NUnit.Framework.Assert.AreEqual(notis.Count, 2);
            NUnit.Framework.Assert.AreEqual(notis[0].Type, "Announcement meme");
            NUnit.Framework.Assert.AreEqual(notis[1].Data, "lmao testdata lmao");
        }


        [Test]
        public void TestSQLConnection()
        {
            Chats chat = new Chats() { Data = "lmao text", ChatID = 69, SenderID = 420 };
            SQLConnection.insert(chat);
            SQLConnection.get(typeof(Chats), $"WHERE ChatID={chat.ChatID}");
            SQLConnection.delete(chat);
            NUnit.Framework.Assert.AreEqual(notis.Count, 2);
            NUnit.Framework.Assert.AreEqual(notis[0].Type, "Announcement meme");
            NUnit.Framework.Assert.AreEqual(notis[1].Data, "lmao testdata lmao");
        }
    }
}