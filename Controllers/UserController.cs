﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InterceptorExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext database;

        public UserController(DatabaseContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return this.database.Users;
        }

        [HttpPost]
        public User Post([FromBody] User user)
        {
            this.database.Add(user);
            this.database.SaveChanges();
            return user;
        }

    }
}
