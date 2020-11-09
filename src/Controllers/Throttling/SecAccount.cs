using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using owasp_topten_api.Services;
using AutoMapper;
using owasp_topten_api.Entities;
using owasp_topten_api.Model;
using SharpApiRateLimit;

namespace owasp_topten_api.Controllers.Throttling
{
    [ApiController]
    [Route("thr/[controller]")]
    public class SecAccount : ControllerBase
    {

        private IAppServices appServices;
        private IMapper automapper;

        public SecAccount(IAppServices appServ, IMapper mpper)
        {
            appServices = appServ;
            automapper = mpper;
        }

        [HttpGet("GetBalance/{id}")]
        [RateLimitByHeader("X-UserId", "2s", calls: 5)]
        public ActionResult Get(int id)
        {
             var account = appServices.GetAccount(id);

            var accountData = automapper.Map<Account, AccountInfo>(account);

            if (account != null)
                return Ok(accountData);
            else
                return BadRequest();
        }

    }
}