﻿using WebTest.Domains.User.Repositories;
using WebTest.Dto.User.Request;
using WebTest.Dto.User.Response;
using WebTest.Transformers.User;

namespace WebTest.Domains.User.Handlers
{
    public class CreateUser(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IHandler<CreateDto, UserDto>
    {
        public UserDto? Handle(CreateDto? dto)
        {
            if (dto == null)
            {
                return null;
            }

            var user = new Models.User.User()
            {
                Login = dto.Login,
                Password = dto.Password,
            };

            userRepository.Save(user);

            return transformer.Transform(user);
        }
    }
}
