
using AgendaBlue.Domain.DAO;
using AgendaBlue.Domain.Entities;
using AgendaBlue.Repository;
using AgendaBlue.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgendaBlue.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthAttributes : ActionFilterAttribute
    {
        IUserRepository _userRepository;
        public AuthAttributes(bool saveUserId = false)
        {

            IDAO<User> _userDAO = new BaseDAO<User>();
            IDAO<Scheduling> _schedulingDAO = new BaseDAO<Scheduling>();
            IDAO<Barber> _barberDAO = new BaseDAO<Barber>();
            IDAO<Sessions> _sessionDAO = new BaseDAO<Sessions>();
            IDAO<Service> _ServiceDAO = new BaseDAO<Service>();
            IDAO<Horary> _horaryDAO = new BaseDAO<Horary>();
            IDAO<Shavy> _shavyDAO = new BaseDAO<Shavy>();

            _userRepository = new UserRepository(_userDAO, _schedulingDAO, _barberDAO, _sessionDAO, _ServiceDAO, _shavyDAO, _horaryDAO);

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var path = context.HttpContext.Request.Path;
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token) || token == null)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new JsonResult("Sorry, you are not logged in. Log in and try again!");
                return;
            }    
           
        }
    }
}
