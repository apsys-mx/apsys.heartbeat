using apsys.heartbeat.exceptions;
using apsys.heartbeat.services.users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apsys.heartbeat.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize()]
    public class UsersController : WebApiControllerBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public UsersController(ILogger<WebApiControllerBase> logger, IMediator mediator, IWebHostEnvironment webHostEnvironment)
            : base(logger, mediator, webHostEnvironment)
        {
        }

        [HttpGet, Route("current")]
        public IActionResult GetCurrent()
        {
            var requestedBy = this.User.GetUserName();
            var command = new ApplicationUserProfileSearcher.Query(requestedBy, requestedBy);
            var userProfile = this._mediator.Send(command).Result;
            return Ok(userProfile.ToDynamic());
        }

        /// <summary>
        /// Get a user profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{userName}")]
        public IActionResult Profile(string userName)
        {
            var requestedBy = this.User.GetUserName();
            var command = new ApplicationUserProfileSearcher.Query(requestedBy, userName);
            var userProfile = this._mediator.Send(command).Result;
            return Ok(userProfile.ToDynamic());
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = "AdministratorRole")]
        public IActionResult GetAll()
        {
            var requestedBy = this.User.GetUserName();
            var command = new ApplicationUsersGetter.Query(requestedBy);
            var allUsers = this._mediator.Send(command).Result;
            return Ok(allUsers.Select(x => x.ToDynamic()));
        }

        /// <summary>
        /// Get the identity server user profile
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet, Route("idsrv/{userName}")]
        public IActionResult GetIdentityServerProfile(string userName)
        {
            var requestedBy = this.User.GetUserName();
            var command = new IdentityUserProfileSearcher.Query(requestedBy, userName);
            var user = this._mediator.Send(command).Result;
            return Ok(user);
        }

        ///// <summary>
        ///// Grant access to a username
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <returns></returns>
        //[HttpPost, Route("{userName}")]
        //[Authorize(Policy = "AdministratorRole")]
        //public IActionResult GrantAccessToUser(string userName)
        //{
        //    try
        //    {
        //        var requestedBy = this.User.GetUserName();
        //        var command = new GrantAccessToUserService.Command(requestedBy, userName);
        //        var user = this._mediator.Send(command).Result;
        //        return Ok(user.ToDynamic());
        //    }
        //    catch (AggregateException ex)
        //    {
        //        if (ex.IsExceptionType<ResourceNotFoundException>())
        //            return BadRequest(ex.Message);
        //        else
        //            return Problem();
        //    }
        //    catch (ResourceNotFoundException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem();
        //    }
        //}

        ///// <summary>
        ///// Add a role to user
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="roleName"></param>
        ///// <returns></returns>
        //[HttpPost("{userName}/roles/{roleName}")]
        //[Authorize(Policy = "AdministratorRole")]
        //public IActionResult AddRoleToUser(string userName, string roleName)
        //{
        //    try
        //    {
        //        var requestedBy = this.User.GetUserName();
        //        var command = new AddRoleToUserService.Command(requestedBy, userName, roleName);
        //        var user = this._mediator.Send(command).Result;
        //        return Ok(user.ToDynamic());
        //    }
        //    catch (AggregateException ex)
        //    {
        //        if (ex.IsExceptionType<ResourceNotFoundException>())
        //            return BadRequest(ex.Message);
        //        else
        //            return Problem();
        //    }
        //    catch (ResourceNotFoundException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch
        //    {
        //        return Problem();
        //    }
        //}

        ///// <summary>
        ///// Add a role to user
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="roleName"></param>
        ///// <returns></returns>
        //[HttpDelete("{userName}/roles/{roleName}")]
        //[Authorize(Policy = "AdministratorRole")]
        //public IActionResult RemoveRoleFromUser(string userName, string roleName)
        //{
        //    try
        //    {
        //        var requestedBy = this.User.GetUserName();
        //        var command = new RemoveRoleFromUserService.Command(requestedBy, userName, roleName);
        //        var user = this._mediator.Send(command).Result;
        //        return Ok(user.ToDynamic());
        //    }
        //    catch (AggregateException ex)
        //    {
        //        if (ex.IsExceptionType<ResourceNotFoundException>())
        //            return BadRequest(ex.Message);
        //        else
        //            return Problem();
        //    }
        //    catch (ResourceNotFoundException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (OperationNotAllowedException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch
        //    {
        //        return Problem();
        //    }
        //}
    }
}
