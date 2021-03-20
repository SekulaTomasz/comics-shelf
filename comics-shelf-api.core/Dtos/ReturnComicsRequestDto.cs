using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace comics_shelf_api.core.Dtos
{
    public class ReturnComicsRequestDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ComicsId { get; set; }
    }
}
