﻿using System.ComponentModel.DataAnnotations;

namespace TODOList.API.Models.DTO
{
    public class AddTodoRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name requires a minimum of 3 characters")]
        [MaxLength(30, ErrorMessage = "Name can not have more that 30 characters")]
        public string Name { get; set; }
        [MaxLength(200, ErrorMessage = "Description can not have more that 200 characters")]
        public string? Description { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
