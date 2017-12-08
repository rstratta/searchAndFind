    using SearchAndFind.DTO;
using System;
using SearchAndFind.Entities;

namespace SearchAndFind.Core
{
    public class ClientManager : AbstractUserManager<ClientRequest>
    {
        protected IUserRepository<Saler> salerRepository;

        private IDTOBuilder<ClientDTO, Client> dtoBuilder;

        public ClientManager(IUserRepository<Client> repository,IUserRepository<Saler> salerRepo, IDTOBuilder<ClientDTO, Client> builder)
        {
            clientRepository = repository;
            salerRepository = salerRepo;
            dtoBuilder = builder;
        }

        protected override User GetUserById(string guid)
        {
            return clientRepository.GetById(BuildGuidFromRequest(guid));
        }
        
        
        protected override void UpdateUser(User user)
        {
            try
            {
                clientRepository.UpdateObject((Client)user);
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al actualizar al Client : " + user.Id);
            }
            
        }

        private  void AddClient(Client client)
        {
            try
            {
                clientRepository.AddObject(client);
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al registrar al Client : " + client.Id);
            }
            
        }

        protected override User BuildAndSaveUserFromRequest(ClientRequest request)
        {
            Client client= new Client(request.DeviceId, request.Name, request.LastName, request.Mail);
            client.Password = EncryptPassword(request.Password);
            client.CurrentToken = GenerateLoginToken();
            AddClient(client);
            return client;
        }

        protected override User BuildAndSaveUserFromGoogleRequest(ClientRequest request)
        {
            Client client = new Client(request.DeviceId, request.Name, request.LastName, request.Mail);
            client.Password = EncryptPassword(request.Password);
            client.CurrentToken = request.CurrentToken;
            AddClient(client);
            return client;
        }

        public override Response BuildSuccessResponse(User user)
        {
            ClientResponse response = new ClientResponse();
            response.ClientDTO = dtoBuilder.BuildDTO((Client)user);
            return response;
        }

        protected override User GetUserByMail(string mail)
        {
            User user;
            try
            {
                user = clientRepository.GetUserByMail(mail);
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al buscar al mail: " + mail);
            }

            return user;
        }

        protected override void ValidateExistOtherProfileUser(string mail)
        {
            try
            {
               User user = salerRepository.GetUserByMail(mail);
               if (user != null)
               {
                   throw new ManagerException("Existe un vendedor con el correo que indica");
               }
            }
            catch (RepositoryException) {
                    
            }
            
        }

        protected override void UpdateConcreteUser(User user, ClientRequest request)
        {
            UpdateConcreteClient((Client)user,  request);
        }

        private void UpdateConcreteClient(Client client,ClientRequest request)
        {
            //TODO
        }

        protected override User GetConcreteUserById(string userId)
        {
            try
            {
                return clientRepository.GetById(Guid.Parse(userId));
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al obtener cliente");
            }
        }

        protected override void ValidateUniqeUserMail(string mail)
        {
            try
            {
                User user = clientRepository.GetUserByMail(mail);
                if (user != null)
                {
                    throw new ManagerException("Existe un cliente con el correo que indica");
                }
            }
            catch (RepositoryException)
            {

            }
        }

        
    }
}
