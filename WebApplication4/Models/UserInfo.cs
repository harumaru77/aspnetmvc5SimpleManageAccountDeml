using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "필수입력사항입니다.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "필수입력사항입니다.")]
        public string UserPassword { get; set; }
        [Required(ErrorMessage = "필수입력사항입니다.")]
        [Compare("UserPassword", ErrorMessage = "비밀번호가 일치하지 않습니다.")]
        public string ConfirmUserPassword { get; set; }
    }
}