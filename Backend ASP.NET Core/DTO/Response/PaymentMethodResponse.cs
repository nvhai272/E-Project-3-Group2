﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Online_Mobile_Recharge.Models;

namespace Online_Mobile_Recharge.DTO.Response
{
    public class PaymentMethodResponse
    {
		public int Id { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }

		//public ICollection<Transaction> Transactions { get; set; }

		//public ICollection<UserPaymentInfo> User_Payment_Infos { get; set; }
	}
}
