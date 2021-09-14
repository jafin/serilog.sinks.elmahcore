using System;
using ElmahCore;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AspNetCoreExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("TriggerUnhandledException")]
        public IActionResult TriggerException()
        {
            throw new Exception("Exception");
        }

        [HttpGet("TriggerHandledException")]
        public IActionResult TriggerHandledException()
        {
            try
            {
                throw new Exception("Exception");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error thrown via Serilog");
            }

            return Ok();
        }

        [HttpGet("ManualException")]
        public IActionResult TriggerManualException()
        {
            this.HttpContext.RiseError(new Exception("Test")); //fails due to https://github.com/ElmahCore/ElmahCore/issues/103
            ElmahExtensions.RiseError(new Exception("Test2"));
            return Ok();
        }
    }
}