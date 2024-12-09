using AgendaBlue.Domain.Entities;
using AgendaBlue.DTO;


namespace AgendaBlue.Manager.Interfaces
{
    public interface IUserManager
    {
        string RegisterUser(UserRegisterDTO userRegister);
        SucessLoginDTO Login(LoginDTO login);

        string RegisterScheduling(SchedulingDTO scheduling); 

        UpdateUserDTO UpdateUser(UpdateUserDTO user);
        string DeleteUser(long IdUser);
        UpdateSchedulingDTO UpdateScheduling(UpdateSchedulingDTO scheduling);
        string DeleteScheduling(long IdScheduling);
        string CompletedScheduling(SchedulingCompletedDTO schedulingCompleted);
        ListResultAllSchedulingDTO GetallScheduling();
        List<User> GetallUsers();
        ListResultSchedulingDTO GetScgedulingPerId(long IdUser);
        string WarningsRoutine();
        string Logout(LogoutDTO logout);
        ListResultAllSchedulingDTO SchedulingsBarbers (string NameBarber);
        User GetUserId(long IdUser);
        List<Horary> GetAllHoraries();
        List<Shavy> GetAllShavy();
        List<Service> GetAllService();
    }
}
