﻿using Logistics.Application.Tools.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Application.v1.Controllers.Base
{
    [ApiController]
    [ValidateModelStateAttribute]
    public class MainController : ControllerBase
    {
    }
}
