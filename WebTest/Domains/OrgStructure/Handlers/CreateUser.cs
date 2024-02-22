﻿using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.User.Request;
using WebTest.Dto.User.Response;
using WebTest.Models.OrgStructure;
using WebTest.Services;
using WebTest.Transformers.User;

namespace WebTest.Domains.OrgStructure.Handlers
{
    public class CreateUser(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IRequestResponseHandler<CreateDto, UserDto>
    {
        public UserDto Handle(CreateDto dto)
        {
            var user = new User()
            {
                Login = dto.Login,
            };
            user.Password = AuthService.HashPassword(user, dto.Password);

            userRepository.Save(user);

            return transformer.Transform(user);
        }
    }
}
