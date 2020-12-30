using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenshinFarm.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(User entity)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9-._@+]+$", RegexOptions.Compiled);
            if (!regex.IsMatch(entity.Username))
            {
                throw new ArgumentOutOfRangeException("Invalid username, characters allowed are: 'a-z', 'A-Z', '0-9', '-._@+'");
            }
            var users = _unitOfWork.UserRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Username == entity.Username) ?? users.FirstOrDefault(u => u.Email == entity.Email);
            if (user != null)
            {
                throw new ArgumentException("Username or email already exists.");
            }
            await _unitOfWork.UserRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddElement(string userId, UserElement userElement)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            if(user == null) { throw new ArgumentException("The user doesn't exists."); }
            var existingElement = user.UserElement.FirstOrDefault(e => e.ElementId == userElement.ElementId);
            if(existingElement != null) { throw new ArgumentException("The user already had the element."); }
            user.UserElement.Add(userElement);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();            
        }

        public Task AddElements(string userId, ICollection<UserElement> userElements)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(string id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            if(user == null) { return false; }
            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public async Task<User> GetById(string id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            if (user == null) { throw new ArgumentException($"There is not User with the id: {id}."); }
            return user;
        }

        public async Task<User> GetLoginByCredentials(UserLogin login)
        {
            return await _unitOfWork.UserRepository.LoginByCredentials(login);
        }

        public async Task<bool> Update(User entity)
        {
            User user = await _unitOfWork.UserRepository.GetById(entity.Id);
            if(user == null) { return false; }
            user.Email = entity.Email;
            user.Username = entity.Username;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
