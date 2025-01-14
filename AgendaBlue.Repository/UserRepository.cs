﻿
using AgendaBlue.Repository.Interfaces;
using System.Net.Mail;
using System.Net;

using System.Text;

using AgendaBlue.DTO;
using AgendaBlue.Domain.DAO;
using AgendaBlue.Domain.Entities;
using AgendaBlue.Domain.Enums;

namespace AgendaBlue.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDAO<User> _userDAO;
        private readonly IDAO<Scheduling> _schedulingDAO;
        private readonly IDAO<Barber> _barberDAO;
        private readonly IDAO<Sessions> _sessionDAO;
        private readonly IDAO<Service> _service;
        private readonly IDAO<Shavy> _shavy;
        private readonly IDAO<Horary> _horaries;

        public UserRepository(IDAO<User> userDAO, IDAO<Scheduling> schedulingDAO, IDAO<Barber> barberDAO, IDAO<Sessions> sessionDAO, IDAO<Service> servicesDAO, IDAO<Shavy> shavyDAO, IDAO<Horary> horaryDAO)
        {
            _horaries = horaryDAO;
            _service = servicesDAO;
            _shavy = shavyDAO;
            _userDAO = userDAO;
            _schedulingDAO = schedulingDAO;
            _barberDAO = barberDAO;
            _sessionDAO = sessionDAO;
        }

        public string DeleteUser(long IdUser)
        {
            var ThereIsUser = _userDAO.GetAll().Where(x => x.IdUser == IdUser);
            if (!ThereIsUser.Any())
            {
                throw new OperationCanceledException($"Could not find any with the given Id {IdUser}");
            }
            var getUser = _userDAO.Get(IdUser);
            _userDAO.Delete(IdUser);
            return $"IdUser:{IdUser} was Deleted";
        }

        public User GetEmail(long IdUser)
        {
           var getuser = _userDAO.GetAll().FirstOrDefault(x=> x.IdUser == IdUser);
           return getuser;
        }

        public Scheduling GetScheduling(long IdUser)
        {
            var getscheduling = _schedulingDAO.GetAll().FirstOrDefault(x => x.IdUser == IdUser);
            return getscheduling;
        }

        public User RegisterUser(string Name, string Email, string CPF, string Password, string Phone, bool IsAdminBarber)
        {
            try
            {
                if (Email.Count() < 2 || CPF.Count() < 3 || Name.Count() < 3 || Phone.Count() < 3 || Password.Count() < 3)
                {
                    throw new OperationCanceledException("Could not create a new user, some fields may be invalid");
                }

                var ThereiIsCPF = _userDAO.GetAll().Where(x => x.CPF == CPF);
                if (ThereiIsCPF.Any())
                {
                    throw new OperationCanceledException("User already existis with CPF");
                }
                var ThereIsEmail = _userDAO.GetAll().Where(x => x.Email == Email);
                if (ThereIsEmail.Any())
                {
                    throw new OperationCanceledException("User already existis with Email");
                }
                var ThereIsPhone = _userDAO.GetAll().Where(x => x.Phone == Phone);
                if (ThereIsPhone.Any())
                {
                    throw new OperationCanceledException("User already existis with Phone");
                }
                
                var newUser = new User()
                {
                    Email = Email,
                    CPF = CPF,
                    UserName = Name,
                    BarberAdmin = false,
                    Password = Password,
                    Phone = Phone,
                };
                _userDAO.Create(newUser);

                
                return newUser;
            }
            
            catch (Exception)
            {
                throw;
            }
        }

        public Scheduling scheduling(long IdUser, DateTime HairCurtDate, string DesiredService, string Time, BarberEnum barberEnum)
        {
            try
            {
                if (DesiredService.Count() < 2 || Time.Count() < 2) 
                {
                    throw new OperationCanceledException("Could not create a new user, some fields may be invalid");
                }
                var UserId = _userDAO.GetAll().Where(x => x.IdUser == IdUser);
                if (!UserId.Any())
                {
                    throw new OperationCanceledException("User Don´t exist");
                }

                //Realizar validação para saber se horario está disponivel
                var getHoraries = _horaries.GetAll().FirstOrDefault(x => x.horary == Time);
                if (getHoraries.AvailableTime == false)
                {
                    throw new Exception("It was not possible to make an appointment because the time has already been chosen.");
                }

                try
                {
                    //realizamos o agendamento do horario
                    getHoraries.AvailableTime = false;
                    _horaries.Update(getHoraries);
                }
                catch
                {
                    throw;
                }
                
                var GetUser = _userDAO.GetAll().FirstOrDefault(x => x.IdUser == IdUser);

                //Procurar pelo o usuario e verifica se ele é admin, caso seja retornar um erro.
                if (GetUser.BarberAdmin == true) throw new Exception("It was not possible to make an appointment.");
                
                var DesiredServiceUnic = _schedulingDAO.GetAll().Where(x => x.IdUser == IdUser).ToList();

                // Procurar agendamentos concluidos, caso haja ele permite um novo cadastro na mesma data e horario.
                foreach (Scheduling desiredService in DesiredServiceUnic)
                {
                    if(desiredService.SchedulingCompleted == true)
                    {
                        //Valida se o usuario já não tem esse serviço agendado
                        if (desiredService.HairCurtDate == HairCurtDate && desiredService.SchedulingCompleted == true)
                        {
                            throw new Exception("service already scheduled for today");
                        }
                    }

                    var ThereIstimes = _schedulingDAO.GetAll().Where(x => x.Time == Time && x.HairCurtDate == HairCurtDate);

                    if (ThereIstimes.Any())
                    {
                        throw new OperationCanceledException("Date and time already scheduled, please choose another again");
                    }
                    var Salveds = new Scheduling()
                        {
                            IdUser = IdUser,
                            Time = Time,
                            DesiredService = DesiredService,
                            HairCurtDate = HairCurtDate,
                            Barber = barberEnum,
                            NameUser = GetUser.UserName

                        };
                        _schedulingDAO.Create(Salveds);

                        var newSalveds = new Barber()
                        {
                            IdSchedulling = Salveds.IdSchedulling,
                            Name = Salveds.Barber.ToString()
                        };
                        _barberDAO.Create(newSalveds);

                    


                    return Salveds;
                    }

                //Caso o cliente não tenha concluido o serviço ele faz novamente a validação para poder salvar.    
                var ThereIstime = _schedulingDAO.GetAll().Where(x => x.Time == Time && x.HairCurtDate == HairCurtDate);

                if (ThereIstime.Any())
                {
                    throw new OperationCanceledException("Date and time already scheduled, please choose another again");
                }

                var Salved = new Scheduling()
                {
                    IdUser = IdUser,
                    Time = Time,
                    DesiredService = DesiredService,
                    HairCurtDate = HairCurtDate,
                    Barber = barberEnum,
                    NameUser = GetUser.UserName

                };
                _schedulingDAO.Create(Salved);

                var newSalved = new Barber()
                {
                    IdSchedulling = Salved.IdSchedulling,
                    Name = Salved.Barber.ToString()
                };
                _barberDAO.Create(newSalved);

                return Salved;
            }

            catch
            {
                throw;
            }
        }

        public User UpdateUser(long IdUser, string Name, string Email, string CPF, string Password, string Phone, bool IsAdminBarber)
        {
            try
            {
                if (Email.Count() < 2 || CPF.Count() < 3 || Name.Count() < 3 || Phone.Count() < 3 || Password.Count() < 3)
                {
                    throw new OperationCanceledException("Could not create a new user, some fields may be invalid");
                }

                var GetUser = _userDAO.Get(IdUser);
                if (GetUser == null)
                {
                    throw new OperationCanceledException("We couldn't find user with that id");
                }

                GetUser.UserName = Name;
                GetUser.Email = Email;
                GetUser.CPF = CPF;
                GetUser.Password = Password;
                GetUser.Phone = Phone;
                GetUser.BarberAdmin = IsAdminBarber;

                var SalvedUser = _userDAO.Update(GetUser);
                return SalvedUser;
               
            }

            catch (Exception ex)
            {
                throw new Exception("Error editing user" + ex);
            }
        }

        public Scheduling UpdateScheduling(long IdScheduling, long IdUser, DateTime HairCurtDate, string DesiredService, string Time, BarberEnum barberEnum)
        {
            try
            {
                if (DesiredService.Count() < 2 || Time.Count() < 2)
                {
                    throw new OperationCanceledException("Could not create a new user, some fields may be invalid");
                }

                var GetAgendate = _schedulingDAO.Get(IdScheduling);
                if (GetAgendate.SchedulingCompleted == true) throw new ArgumentException("This appointment has already been completed.");

                var Scheduling = _schedulingDAO.GetAll().Where(x => x.IdSchedulling == IdScheduling);

                if (!Scheduling.Any())
                {
                    throw new OperationCanceledException("Scheduling Don´t exist");
                }

                var SeeUser = _schedulingDAO.GetAll().Where(x => x.IdUser == IdUser);

                if (!SeeUser.Any())
                {
                    throw new OperationCanceledException("User Don´t exist");
                }

                var GetUser = _userDAO.GetAll().Where(x => x.IdUser == IdUser).ToList();
                foreach (User user in GetUser)
                {
                    if (user.BarberAdmin == true)
                    {
                        throw new Exception("It was not possible to make an appointment.");
                    }
                }
                var DesiredServiceUnic = _schedulingDAO.GetAll().Where(x => x.IdUser == IdUser).ToList();

                //foreach (Scheduling desiredService in DesiredServiceUnic)
                //{
                //    if (desiredService.DesiredService == DesiredService)
                //    {
                //        throw new Exception("service already scheduled for today");
                //    }
                //}

                var ThereIstime = _schedulingDAO.GetAll().Where(x => x.Time == Time && x.HairCurtDate == HairCurtDate);

                if (ThereIstime.Any())
                {
                    throw new OperationCanceledException("Date and time already scheduled, please choose another again");
                }

                var GetScheduling = _schedulingDAO.Get(IdScheduling);

                if (GetScheduling.IdUser != IdUser) throw new OperationCanceledException("You cannot edit someone else's schedule.");


                    GetScheduling.HairCurtDate = HairCurtDate;
                    GetScheduling.DesiredService = DesiredService;
                    GetScheduling.Time = Time;
                    GetScheduling.Barber = barberEnum;

                    var salvedUpdated = _schedulingDAO.Update(GetScheduling);

                    var GetBarber = _barberDAO.GetAll().FirstOrDefault(x => x.IdSchedulling == IdScheduling);
                    GetBarber.Name = barberEnum.ToString();
                    _barberDAO.Update(GetBarber);


                return GetScheduling;
                
                
            }

            catch 
            {
                throw;
            }


        }

        public User UserIsValid(string Email, string Password)
        {
            var User = _userDAO.GetAll().FirstOrDefault(x => x.Email == Email && x.Password == Password);
            if (User == null)
            {
                throw new OperationCanceledException("Incorrect username and/or password. Try again.");
            }
            return User;

        }

        public string DeleteScheduling(long IdScheduling)
        {
            var ThereIsUser = _schedulingDAO.GetAll().Where(x => x.IdSchedulling == IdScheduling);
            if (!ThereIsUser.Any())
            {
                throw new OperationCanceledException($"Could not find any with the given Id {IdScheduling}");
            }
            _schedulingDAO.Delete(IdScheduling);
           
            return $"Schedullig:{IdScheduling} was Deleted";
        }

        public string SchedulingCompleted(long IdScheduling, bool SchedulingCompleted)
        {
            try
            {
                var Scheduling = _schedulingDAO.GetAll().Where(x => x.IdSchedulling == IdScheduling);

                if (!Scheduling.Any())
                {
                    throw new OperationCanceledException("Scheduling Don´t exist");
                }

                var GetScheduling = _schedulingDAO.GetAll().FirstOrDefault(x => x.IdSchedulling == IdScheduling);
                if (GetScheduling != null)
                {
                    if (GetScheduling.SchedulingCompleted == true) throw new ArgumentException("appointment has already been completed");
                     GetScheduling.SchedulingCompleted = SchedulingCompleted;
                     var salved = _schedulingDAO.Update(GetScheduling);
                    return "Scheduling completed successfully";
                }
                return "unable to finalize appointment";


            }

            catch
            {
                throw;
            }
        }

        public ListResultAllSchedulingDTO GetAllScheduling()
        {
            var GetAllScheduling = _schedulingDAO.GetAll().Where(x => x.SchedulingCompleted == false).ToList();
            var result = new ListResultAllSchedulingDTO()
            {
                schedulings = GetAllScheduling,
                Count = GetAllScheduling.Count()
            };
            return result;
        }

        public List<User> GetallUsers()
        {
            var  GetAllUsers = _userDAO.GetAll().ToList();
            return GetAllUsers;
        }

        public ListResultSchedulingDTO GetSchedulingPerId(long IdUser)
        {
            var UserId = _userDAO.GetAll().Where(x => x.IdUser == IdUser);
            if (!UserId.Any())
            {
                throw new OperationCanceledException("User Don´t exist");
            }
            var getAll = _schedulingDAO.GetAll().Where(x => x.IdUser == IdUser && x.SchedulingCompleted == false).ToList();

            var count = getAll.Count();
            var Result = new ListResultSchedulingDTO()
            {
                schedulings = getAll,
                Count = count
            };
            return Result;
        }
        

        public string WarnigsRoutine()
        {
            try
            {

                var date = DateTime.Now;
                var IdUser = 0 ;
                var VeryfyDates = _schedulingDAO.GetAll().Where(x => x.HairCurtDate.Date == date.Date);
                if (!VeryfyDates.Any())
                {
                    throw new OperationCanceledException("We do not have appointments for this date.");
                }
                var getScheduling = _schedulingDAO.GetAll().Where(x => x.HairCurtDate.Date == date.Date && x.SchedulingCompleted == false).ToList();
                if (getScheduling != null)
                {
                foreach(Scheduling scheduling in getScheduling)
                {
                    IdUser = (int)scheduling.IdUser;
                    var GetUsers = _userDAO.GetAll().FirstOrDefault(x => x.IdUser == IdUser);
                        MailMessage mailMessage = new MailMessage("barbeariadesign628@gmail.com", GetUsers.Email);

                        mailMessage.Subject = $"Comunicado de Aviso!";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = $"<h1> Olá, {GetUsers.UserName}!  </h1> <br> <p> Vinhemos informa que, seu agendamento é hoje dia <b> {scheduling.HairCurtDate.Date.Day}</b> as <b> {scheduling.Time}</b> horas, para realizar o serviço desejado: <b>{scheduling.DesiredService}.</b> </p> <br> <button> Confirmar Presença </button> <br> <hr> <br> Te Aguardamos ansiosamente.  ";
                        mailMessage.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                        mailMessage.BodyEncoding = Encoding.GetEncoding("UTF-8");

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential("barbeariadesign628@gmail.com", "xlbgbmrhrhjkfnzt");

                        smtpClient.EnableSsl = true;

                        smtpClient.Send(mailMessage);

                    }
            }
                return "sucess";
                
            }
            catch
            {
                throw;
            }
            
          

        }

        public Sessions CreateSession(long IdUser, string Token)
        {
            var UserId = _userDAO.GetAll().Where(x => x.IdUser == IdUser);
            if (!UserId.Any())
            {
                throw new OperationCanceledException("User Don´t exist");
            }

            var salvedSessio = new Sessions()
            {
                IdUser = IdUser,
                Token = Token,
                LoginDate = DateTime.Now,
                SessionFinalized = false
            };
            var SessionCreate = _sessionDAO.Create(salvedSessio);
            return SessionCreate;
        }

        public Sessions SeeTokenValid(string Token)
        {
            try
            {

            var GetSession = _sessionDAO.GetAll().Where(x => x.Token == Token).ToList();

                foreach(var Session in GetSession)
                {
                    if(Session.SessionFinalized == false)
                    {
                        var subtract = DateTime.Now.Hour - Session.LoginDate.Hour;
                        var result = Math.Abs(subtract);
                        if (result > 5)

                        {
                            Session.LogoutDate = DateTime.Now;
                            Session.SessionFinalized = true;
                            var salved = _sessionDAO.Update(Session);
                            return salved;
                        }
                    };
                }
                    var finalized = new Sessions();
                    finalized = null;
                    return finalized;
            }
            catch
            {
                throw;
            }
            
        }

        public string Logout(long IdUser, string token)
        {
            try
            {
                var VerifySession = _sessionDAO.GetAll().Where(x => x.IdUser == IdUser && x.SessionFinalized == false);
                if (!VerifySession.Any())
                {
                    throw new ArgumentException("Sessios has finalized");
                }
                var UserId = _sessionDAO.GetAll().Where(x => x.IdUser == IdUser && x.Token == token && x.SessionFinalized == false);
                if (UserId.Any())
                {
                    var GetSession = _sessionDAO.GetAll().Where(x => x.IdUser == IdUser).ToList();
                    foreach (Sessions sessions in GetSession)
                    {
                        sessions.SessionFinalized = true;
                        sessions.LogoutDate = DateTime.Now;
                        var salved = _sessionDAO.Update(sessions);
                    }

                }
                    return "Session Finalized sucessufuly";
               
            }
            catch
            {
                throw;
            }


        }

        public ListResultAllSchedulingDTO GetSchedulingsBarbers(string NameBarber)
        {
            try
            {
            var getSchedulingBarber = _schedulingDAO.GetAll().Where(x => x.Barber.ToString() == NameBarber && x.SchedulingCompleted == false).ToList();
            
                var result = new ListResultAllSchedulingDTO()
                {
                    schedulings = getSchedulingBarber,
                    Count = getSchedulingBarber.Count()

                };
                return result;
         

            }
            catch
            {
                throw;
            }
        }

        public User GetUserId(long IdUser)
        {
            try
            {
                var getUser = _userDAO.Get(IdUser);
                if (getUser == null) throw new ArgumentException("could not find this IdUser");
                return getUser;
            }
            catch
            {
                throw;
            }
        }

        public List<Shavy> GetAllShavies()
        {
            var getAllShavs = _shavy.GetAll().ToList();

            try
            {
                if (getAllShavs == null)
                {
                    throw new ArgumentException("Não foi possivel retornar a lista");
                }

                return getAllShavs;

            }
            catch
            {
                throw;
            }

            
        }

        public List<Horary> GetAllHoraries()
        {
            var getAllHoraries = _horaries.GetAll().Where(x=> x.AvailableTime == true).ToList();

            try
            {
                if (getAllHoraries == null)
                {
                    throw new ArgumentException("Não foi possivel retornar a lista");
                }

                return getAllHoraries;
            }
            catch
            {
                throw;
            }
        }

        public List<Service> GetAllService()
        {
            var getAllServices = _service.GetAll().ToList();

            try
            {
                if (getAllServices == null)
                {
                    throw new ArgumentException("Não foi possivel retornar a lista");
                }

                return getAllServices;
            }
            catch
            {
                throw;
            }
        }
    }
}
