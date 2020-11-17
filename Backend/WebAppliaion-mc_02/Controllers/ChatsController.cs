using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication_mc_02.Models;
using System.Net.Http;
using MySql.Data.MySqlClient;
using WebApplication_mc_02.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections;
using System.Threading;
using Nancy.Json;

namespace WebApplication_mc_02.Controllers
{
    /// <summary>
    /// Interface with the Chats Database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        // GET: api/Students
        /// <summary>
        /// gets all chats that a student is in
        /// </summary>
        /// <returns> list of all chats</returns>
        [HttpGet("Group/{id}")]
        public async Task<ActionResult<IEnumerable<Student2ChatMap>>> GetGroupChat(int id)
        {
            List<Student2ChatMap> chats = await SQLConnection.Get<Student2ChatMap>($"WHERE StudentID = {id}");
            return Ok(chats);
        }

        // GET: api/Chats/{StudentID}/{ChatID}
        /// <summary>
        /// Gets Chat log of given student and Chat
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="ChatID"></param>
        /// <returns></returns>
        [HttpGet("{StudentID}/{ChatID}")]
        public async Task<ActionResult<Chats>> GetChat(int StudentID, int ChatID)
        {
            List<Chats> chat = new List<Chats>();
            List<Student2ChatMap> chatGroups = await SQLConnection.Get<Student2ChatMap>($"WHERE StudentID={StudentID}");
            foreach (Student2ChatMap map in chatGroups)
            {
                if (map.ChatID == ChatID)
                {
                    chat = await SQLConnection.Get<Chats>($"Where ChatID={ChatID}");
                    break;
                }
            }
            if (chat.Count == 0)
                return BadRequest("There's no chat under that id");
            return Ok(chat);
        }

        // PUT: api/Students/5
        /// <summary>
        /// Create a Chat Group based on Courses
        /// </summary>
        /// <param name="studIDs">current studentID</param>
        /// <returns>'ChatID: {chatID}, {groupChat.Keys.ToList()}'</returns>
        [HttpPut]
        public async Task<ActionResult<Chats>> PutGroupChat([Bind] int studIDs)
        {
            var groupChat = groupAlgorithm(studIDs);
            var chatID = makeGroupChat(groupChat.Keys.ToList());
            var data = new JavaScriptSerializer().Serialize(groupChat);
            return Ok($"ChatID: {chatID}, {data}");
        }

        // POST: api/Students/
        /// <summary>
        /// sends a chat to the group
        /// </summary>
        /// <param name="chat">chat object</param>
        /// <returns>the chat that was sent</returns>
        [HttpPost]
        public async Task<ActionResult<Chats>> PostChat([FromBody] Chats chat)
        {
            if (SQLConnection.Get<Chats>($"WHERE ChatID={chat.ChatID} and SenderID={chat.SenderID}").Result.Count < 1)
                return BadRequest("your not allowed to send data in this chat");
            if (await SQLConnection.Insert<Chats>(chat))
                return Ok(chat);
            return BadRequest();
        }

        // DELETE: api/Students/5
        /// <summary>
        /// deletes a chat from the database
        /// </summary>
        /// <param name="id">Chat ID</param>
        /// <returns>Chat that was removed</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Chats>> DeleteChat(int id)
        {
            var chat = SQLConnection.Get<Chats>($"WHERE ChatID = {id}").Result[0];
            if (SQLConnection.delete(chat))
                return Ok(chat);
            return BadRequest("sum went wrong, idk");
        }

        private async Task<int> makeGroupChat(List<int> studIDs)
        {
            string query = "SELECT MAX(ChatID) FROM StudentLink.Student2ChatMap";
            object result = SQLConnection.get(query);
            if (result.ToString() == "")
                result = "0";
            foreach (int stuID in studIDs)
            {
                Student2ChatMap s2cm = new Student2ChatMap();
                s2cm.ChatID = Convert.ToInt32(result) + 1;
                s2cm.StudentID = stuID;
                await SQLConnection.Insert<Student2ChatMap>(s2cm);
            }
            return Convert.ToInt32(result) + 1;
        }

        private Dictionary<int, double> groupAlgorithm(int StudentID)
        {
            Dictionary<int, int> Student2Course = new Dictionary<int, int>();
            int GroupSize = 7;
            var courseList = SQLConnection.Get<Student2CourseMap>($"WHERE StudentID={StudentID} and currentlyEnrolled=1").Result;
            foreach (var course in courseList)
            {
                var studentList2 = SQLConnection.Get<Student2CourseMap>($"WHERE CourseID={course.CourseID}").Result;
                foreach (var student in studentList2)
                {
                    if (Student2Course.ContainsKey(student.StudentID))
                        Student2Course[student.StudentID]++;
                    else
                        Student2Course.Add(student.StudentID, 1);
                }
            }
            Student2Course.OrderBy(entry => entry.Value).ToList();
            Dictionary<int, double> simStudents = new Dictionary<int, double>();
            for (int i = 0; i < ((Student2Course.Count < GroupSize) ? Student2Course.Count : GroupSize); i++)
            {
                double percentMatch = Student2Course.ElementAt(i).Value / courseList.Count();
                simStudents.Add(Student2Course.ElementAt(i).Key, percentMatch);
            }

            return simStudents;
        }
    }
}
