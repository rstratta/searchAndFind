using System;

namespace SearchAndFind.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
        public string CurrentToken { get; set; }
        public int NumberOfReview { get; set; }
        public double AverageReview { get; set; }
        public UserDTO() { }
        public UserDTO(Guid id, string name, string lastName, string mailAddress, string deviceId)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            MailAddress = mailAddress;
            DeviceId = deviceId;
        }
    }
}
