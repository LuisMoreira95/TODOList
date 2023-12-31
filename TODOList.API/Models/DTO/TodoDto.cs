﻿using System;
using TODOList.API.Models.Domain;

namespace TODOList.API.Models.DTO
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Done { get; set; }
        public IList<CategoryDto> Categories { get; set; }
    }
}
