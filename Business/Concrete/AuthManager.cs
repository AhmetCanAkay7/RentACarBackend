using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        //Normalde bir manager sınıfı başka bir manager sınıfını kullanmaz fakat Auth bir veritabanı nesnesi 
        //olmadığından veriye erişim için başka bir manager sınıfına ihtiyaç duyar.
        IUserService _userService;
        ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService,ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
           var claims =_userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken,Messages<User>.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages<User>.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.Data.PasswordHash,userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages<User>.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck.Data,Messages<User>.SuccessfulLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
            User user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Insert(user);
            return new SuccessDataResult<User>(user,Messages<User>.UserRegistered);

        }

        public IResult UserExists(string email)
        {
            var userToCheck = _userService.GetByMail(email);
            if (userToCheck.Data != null)
            {
                return new ErrorResult(Messages<User>.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        
    }
}
