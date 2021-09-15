using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        public EmailController(ILogger<EmailController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> SendMail([FromServices] IFluentEmail mailer)
        {
            string recipientName = "Dan Petru";
            var email = mailer
                .To("bilanin.maria@gmail.com", "Maria Melton")
                .Subject("Hello there from DI!")
                //.Body("This is a plain text message using Gmail");
                .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/Template/sample.cshtml",
               new
               {
                   Name = recipientName
               });

            await email.SendAsync();
            return Ok("The email has been sent.");
        }
    }
}