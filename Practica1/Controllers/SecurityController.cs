using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica1.Responses;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entidades;
using SocialMediaCore.Enumerations;
using SocialMediaCore.Interfaces;

namespace Practica1.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrador))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService securityService;
        private readonly IMapper mapper;

        public SecurityController(ISecurityService _securityService, IMapper _mapper)
        {
            securityService = _securityService;
            mapper = _mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SecurityDTO _securityDTO)
        {
            var security = mapper.Map<Security>(_securityDTO);

            await securityService.RegisterUser(security);
            var securityDTO = mapper.Map<SecurityDTO>(security);

            var response = new ApiResponse<SecurityDTO>(securityDTO);
            return Ok(response);
        }
    }
}
