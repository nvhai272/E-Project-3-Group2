﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Mobile_Recharge.DTO.Request;
using Online_Mobile_Recharge.DTO.Response;
using Online_Mobile_Recharge.Interfaces;
using Online_Mobile_Recharge.Models;

namespace Online_Mobile_Recharge.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserServiceController : ControllerBase
	{
		private readonly ICrud<UserService> _crud;
		private readonly IMapper _mapper;

		public UserServiceController(ICrud<UserService> crud, IMapper mapper)
		{
			_crud = crud;
			_mapper = mapper;
		}

		[HttpGet]
		[Route("list")]
		public IActionResult GetAllUserService()
		{
			try
			{
				//var UserServiceList = _crud.GetListItems();
				var UserServiceList = _mapper.Map<List<UserServiceResponse>>(_crud.GetListItems());
				return Ok(UserServiceList);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("detail/{id}")]
		public IActionResult GetUserService(int id)
		{
			try
			{
				var UserService = _mapper.Map<UserServiceResponse>(_crud.GetItemById(id));
				return Ok(UserService);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("create")]
		public IActionResult CreateUserService([FromBody] UserServiceRequest newUserService)
		{
			try
			{
				_crud.Create(_mapper.Map<UserService>(newUserService));
				return Ok("Thanh cong");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		[Route("edit/{id}")]
		public IActionResult UpdateUserService(int id, UserServiceRequest e)
		{
			try
			{
				var ex = _mapper.Map<UserService>(e);
				_crud.Update(id, ex);
				return Ok("Thanh cong");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		[Route("delete/{id}")]
		public IActionResult DeleteUserService(int id)
		{
			try
			{
				_crud.Delete(id);
				return Ok("Thanh cong");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}