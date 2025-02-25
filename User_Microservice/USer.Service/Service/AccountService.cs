﻿using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using User.API.Data;
using User.API.Exceptions;
using User.API.Interfaces;
using User.Data;
using User.Data.Entities;
using User.Service.Models;


namespace User.API.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAccountCryptographyService _cryptographyService;

        public AccountService(IUserRepository userRepository, IMapper mapper, IAccountCryptographyService cryptographyService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cryptographyService = cryptographyService;
        }
        public async Task DeleteAccount(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            await _userRepository.Delete(id);
        }

        public async Task CreateAccount(UserModel user)
        {
            if (_userRepository.GetByEmail(user.Email) != null)
            {
                throw new AccountCreatingException("This email already used");
            }

            IEnumerable<UserEntity> usersList = await _userRepository.GetAll();
            user.Role = !usersList.Any() ? UserRoles.Roles.Admin : UserRoles.Roles.User;

            var userEntity = _mapper.Map<UserModel, UserEntity>(user);
            userEntity.Password = _cryptographyService.HashPassword(userEntity.Password);

            await _userRepository.Add(userEntity);  
        }

        public UserModel Authorize(UserModel user)
        {
            var userEntity = _mapper.Map<UserModel, UserEntity>(user);

            var dbUser = _userRepository.GetByEmail(userEntity.Email);

            if (dbUser == null)
            {
                return null;
            }

            if (_cryptographyService.Authorize(userEntity.Password, dbUser.Password))
            {
                user = _mapper.Map<UserEntity, UserModel>(dbUser);

                return user;
            }

            return null;
        }

        

        public async Task<UserModel> GetAccount(Guid id)
        {
            var userEntity = await _userRepository.GetById(id);
            var userModel = _mapper.Map<UserEntity, UserModel>(userEntity);
            return userModel;
        }

        public async Task EditAccount(UserModel user)
        {
            var userEntity = _mapper.Map<UserModel, UserEntity>(user);
            await _userRepository.Edit(userEntity);
        }

        public async Task<IEnumerable<UserModel>> GetAllAccounts()
        {
            var userEntities = await _userRepository.GetAll();
            var userModels = _mapper.Map<IEnumerable<UserModel>>(userEntities);
            return userModels;
        }
    }
}
