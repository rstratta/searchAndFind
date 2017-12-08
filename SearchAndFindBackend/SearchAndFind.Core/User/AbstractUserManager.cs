using SearchAndFind.Core;
using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public abstract class AbstractUserManager<R> where R:UserRequest
    {
        
        protected IUserRepository<Client> clientRepository;

        public virtual Response SignUp(R request)
        {
            ValidateSignUpRequest(request);
            User user = BuildAndSaveUserFromRequest(request);
            return BuildSuccessResponse(user);
        }
        protected abstract void ValidateUniqeUserMail(string mail);
        protected abstract void ValidateExistOtherProfileUser(string mail);

        protected abstract User BuildAndSaveUserFromRequest(R request);

        public virtual Response SignIn(R request)
        {
            User user = GetUserByMail(request.Mail);
            VerifyUserPassword(request.Password, user.Password);
            user.CurrentToken = GenerateLoginToken();
            user.DeviceId = request.DeviceId;
            UpdateUser(user);
            return BuildSuccessResponse(user);
        }


        public virtual Response GetUserById(R request)
        {
            User user = GetConcreteUserById(request.UserId);
            return BuildSuccessResponse(user);
        }

        protected abstract User GetConcreteUserById(string userId);

        public virtual Response UpdateAccount(R request)
        {
            User user = GetUserById(request.UserId);
            UpdateUserFields(user, request);
            UpdateConcreteUser(user, request);
            UpdateUser(user);
            return BuildSuccessResponse(user);
        }


        protected abstract void UpdateConcreteUser(User user, R request);

        public Response SignInByGoogleAuthentication(R request) 
        {
            try { 
                User user = GetUserByMail(request.Mail);
                user.CurrentToken = request.CurrentToken;
                user.DeviceId = request.DeviceId;
                UpdateUser(user);
                return BuildSuccessResponse(user);
            }
            catch(ManagerException)
            {
                return SignUpGoogle(request);
            }
           
        }

        private Response SignUpGoogle(R request)
        {
            ValidateSignUpRequest(request);
            User user = BuildAndSaveUserFromGoogleRequest(request);
            return BuildSuccessResponse(user);
        }

        protected abstract User BuildAndSaveUserFromGoogleRequest(R request);

        private void ValidateSignUpRequest(R request)
        {
            ValidateExistOtherProfileUser(request.Mail);
            ValidateUniqeUserMail(request.Mail);
        }

        public virtual UserDTO GetUserDTOById(string guid)
        {
            User user = GetUserById(guid);
            return BuildUserDTO(user);
        }
        private UserDTO BuildUserDTO(User user)
        {
            IDTOBuilder<UserDTO, User> dtoBuilder = new UserDTOBuilder();
            return dtoBuilder.BuildDTO(user);
        }
        protected abstract User GetUserById(string guid);

        protected abstract User GetUserByMail(string mail);

        protected abstract void UpdateUser(User user);

        public abstract Response BuildSuccessResponse(User user);

        protected virtual void UpdateUserFields(User user, UserRequest request)
        {
            user.Name = request.Name;
            user.LastName = request.LastName;
            user.MailAddress = request.Mail;
            user.Eliminated = false;
            user.DeviceId = request.DeviceId;
        }
        public virtual Response RemoveAccount(R request)
        {
            User user = GetUserById(request.UserId);
            if (user.Eliminated)
            {
                throw new ManagerException("El usuario que intenta eliminar ya está eliminado");
            }
            UpdateUser(user);
            return BuildSuccessResponse(user);
        }

        protected string EncryptPassword(string password)
        {
            try
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
                return System.Convert.ToBase64String(data);
            }
            catch (Exception)
            {
                return password;
            }
        }

        protected string DesEncryptPassword(string password)
        {
            try
            { 
            byte[] data = System.Convert.FromBase64String(password);
            return System.Text.Encoding.UTF8.GetString(data);
            }
            catch (Exception)
            {
                return password;
            }
        }

        protected string GenerateLoginToken()
        {
            return Guid.NewGuid().ToString();
        }

        protected Guid BuildGuidFromRequest(string id)
        {
            return Guid.Parse(id);
        }

        protected void VerifyUserPassword(string requestPassword, string userPassword)
        {
            if (!requestPassword.Equals(DesEncryptPassword(userPassword)))
            {
                throw new ManagerException("Verifique el password ingresado");
            }
        }
    }
}
