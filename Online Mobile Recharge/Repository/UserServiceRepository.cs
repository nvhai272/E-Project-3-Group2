﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Online_Mobile_Recharge.DTO.Response;
using Online_Mobile_Recharge.Interfaces;
using Online_Mobile_Recharge.Models;

namespace Online_Mobile_Recharge.Repository
{
	public class UserServiceRepository : ICrud<UserService, UserServiceResponse>
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public UserServiceRepository(DataContext dataContext, IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public UserServiceResponse Convert(UserService e)
		{
			var getUser = _dataContext.Users.Find(e.UserId).Name;
			var getService = _dataContext.Services.Find(e.ServiceId).Name;
			var res = new UserServiceResponse()
			{
				Id = e.Id,
				UserName = getUser,
				ServiceName = getService
			};
			return res;
		}

		public bool Create([FromBody] UserService entity)
		{
			var findUser = _dataContext.Users.Find(entity.UserId);
			var findService = _dataContext.Services.Find(entity.ServiceId);
			UserService userService = new UserService()
			{
				UserId = entity.UserId,
				ServiceId = entity.ServiceId,
				Service = findService,
				User = findUser,
				Status = entity.Status
			};
			_dataContext.User_Service.Add(userService);
			return Save();
		}

		public bool Delete(int id, UserService entity)
		{
			var updateDelete = _dataContext.User_Service.Find(id);
			updateDelete.IsDeleted = entity.IsDeleted;
			_dataContext.User_Service.Update(updateDelete);
			return Save();
		}

		public UserService GetItem(int id)
		{
			if (IsExisted(id))
			{
				return _dataContext.Set<UserService>().FirstOrDefault(x => x.Id == id);
			}
			throw new InvalidOperationException("UserService does not existed");
		}

		public UserServiceResponse GetItemById(int id)
		{
			if (IsExisted(id))
			{
				return _mapper.Map<UserServiceResponse>(_dataContext.Set<UserService>().FirstOrDefault(x => x.Id == id));
			}
			throw new InvalidOperationException("UserService does not existed");
		}

		public ICollection<UserServiceResponse> GetListItems()
		{
			return _mapper.Map<List<UserServiceResponse>>(_dataContext.Set<UserService>().Where(p => p.IsDeleted == false).OrderBy(p => p.Id).ToList());
		}

		public bool IsExisted(int id)
		{
			return _dataContext.User_Service.Any(e => e.Id == id && e.IsDeleted == false);
		}

		public bool Save()
		{
			var save = _dataContext.SaveChanges();
			return save > 0 ? true : false;
		}

		public bool Update(int id, UserService entity)
		{
			try
			{
				var existingUserService = GetItem(id);
				var findUser = _dataContext.Users.Find(entity.UserId);
				var findServicee = _dataContext.Services.Find(entity.ServiceId);
				existingUserService.Service = findServicee;
				existingUserService.ServiceId = entity.ServiceId;
				existingUserService.User = findUser;
				existingUserService.UserId = entity.UserId;
				existingUserService.Status = entity.Status;
				existingUserService.ModifiedAt = DateTime.Now;
				_dataContext.User_Service.Update(existingUserService);
				return Save();
			}
			catch (InvalidOperationException ex)
			{
				throw ex;
			}
		}
	}
}
